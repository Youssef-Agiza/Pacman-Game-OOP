using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebProject1.Models;
using EASendMail;
using EASendMail.Oauth;

namespace WebProject1.Controllers
{
    public class OauthController : Controller
    {
        public ActionResult Index()
        {
            var mailTask = _buildDefaultTask();
            return _mailView(mailTask);
        }

        ActionResult _mailView(MailTask mailTask)
        {
            ViewBag.Port = DropDownListData.PortList(mailTask.Port);
            ViewBag.OauthProvider = DropDownListData.OauthList(mailTask.OauthProvider);
            return View("Index", mailTask);
        }

        void _setDefaultViewBagValue()
        {
            ViewBag.IsSyncSendSucceeded = false;
            ViewBag.SyncSendStatus = string.Empty;

            ViewBag.AutoAsyncSend = false;
            ViewBag.TokenIsExisted = false;
        }

        MailTask _buildDefaultTask()
        {
            var mailTask = new MailTask();
            mailTask.Subject = "Test email from ASP.NET MVC with XOAUTH2";
            mailTask.IsAuthenticationRequired = true;
            mailTask.AuthType = SmtpAuthType.XOAUTH2;
            mailTask.Server = "smtp.gmail.com";
            mailTask.Port = 587;
            mailTask.OauthProvider = OauthProvider.GoogleSmtpProvider;
            mailTask.IsSslConnection = true;
            mailTask.Sender = "unspecified"; // will be replaced after oauth is completed.

            var body = new StringBuilder();
            body.Append("This sample demonstrates how to send email from ASP.NET MVC with XOAUTH2.\r\n\r\n");
            body.Append("Please apply for your Google/MS client_id and client_secret as introduced in OauthProvider.cs.\r\n");
            body.Append("If you got \"This app isn't verified\" information, please click \"advanced\" -> Go to ... for test.\r\n");

            mailTask.TextBody = body.ToString();

            _setDefaultViewBagValue();

            var OauthWrapper = _initOauthWrapper(mailTask.OauthProvider);
            ViewBag.TokenIsExisted = !string.IsNullOrEmpty(OauthWrapper.Provider.AccessToken);

            return mailTask;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(MailTask mailTask)
        {
            _setDefaultViewBagValue();

            if (ModelState.IsValid)
            {
                try
                {
                    return _doOauth(mailTask);
                }
                catch (Exception ep)
                {
                    ViewBag.SyncSendStatus = ep.Message;
                }

                return _mailView(mailTask);
            }

            var OauthWrapper = _initOauthWrapper(mailTask.OauthProvider);
            ViewBag.TokenIsExisted = !string.IsNullOrEmpty(OauthWrapper.Provider.AccessToken);

            return _mailView(mailTask);
        }

        // Please add http://localhost:54098/oauth/token to Authorized redirect URIs in your Google/MS Azure project.
        // to learn more detail, please refer to GoogleOauthProvider.cs
        public ActionResult Token(string code, string state)
        {
            _setDefaultViewBagValue();

            MailTask mailTask = Services.TempMailTaskStore.GetTask(state);
            if (mailTask == null)
            {
                mailTask = _buildDefaultTask();

                ViewBag.SyncSendStatus = "Specified mail task is not found in temporal storage, please try it again.";
                return _mailView(mailTask);
            }

            var OauthWrapper = _initOauthWrapper(mailTask.OauthProvider);

            try
            {
                OauthWrapper.AuthorizationCode = code;
                OauthWrapper.RequestAccessTokenAndUserEmail();
            }
            catch (Exception ep)
            {
                ViewBag.SyncSendStatus = ep.Message;
                return _mailView(mailTask);
            }

            if (mailTask.IsAsyncTask)
            {
                ViewBag.IsSyncSendSucceeded = true;
                ViewBag.SyncSendStatus = "Oauth is completed, ready to send email.";

                // oauth is completed. back to view and invoke AutoAsyncSend.
                ViewBag.AutoAsyncSend = true;
                ViewBag.TokenIsExisted = !string.IsNullOrEmpty(OauthWrapper.Provider.AccessToken);
                return _mailView(mailTask);
            }

            return _syncSendMail(mailTask);
        }

        private ActionResult _doOauth(MailTask mailTask)
        {
            _setDefaultViewBagValue();

            var OauthWrapper = _initOauthWrapper(mailTask.OauthProvider);
            if (!string.IsNullOrEmpty(OauthWrapper.Provider.AccessToken))
            {
                if (!OauthWrapper.IsAccessTokenExpired)
                {
                    return _syncSendMail(mailTask);
                }

                try
                {
                    OauthWrapper.RefreshAccessToken();
                    return _syncSendMail(mailTask);
                }
                catch
                {
                    // "Failed to refresh access token, try to get a new access token ...";
                }
            }

            Services.TempMailTaskStore.PutTask(mailTask);
            return Redirect(OauthWrapper.Provider.GetFullAuthUri() + "&state=" + mailTask.TaskId);
        }

        private ActionResult _syncSendMail(MailTask mailTask)
        {
            var OauthWrapper = _initOauthWrapper(mailTask.OauthProvider);
            ViewBag.TokenIsExisted = !string.IsNullOrEmpty(OauthWrapper.Provider.AccessToken);

            try
            {
                mailTask.AuthType = SmtpAuthType.XOAUTH2;
                mailTask.User = OauthWrapper.Provider.UserEmail;
                mailTask.Password = OauthWrapper.Provider.AccessToken;
                mailTask.IsAuthenticationRequired = true;

                // always set From to authenticated user.
                mailTask.Sender = OauthWrapper.Provider.UserEmail;

                var smtp = new SmtpClient();
                var server = mailTask.BuildServer();
                var mail = mailTask.BuildMail();

                smtp.SendMail(server, mail);

                ViewBag.IsSyncSendSucceeded = true;
                ViewBag.SyncSendStatus = "Message has been submitted to server successfully.";
            }
            catch (Exception ep)
            {
                ViewBag.IsSyncSendSucceeded = false;
                ViewBag.SyncSendStatus = ep.Message;
            }

            mailTask.TaskId = Guid.NewGuid().ToString();
            return _mailView(mailTask);
        }

        // access token is existed, submit email directly from ajax send.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AsyncSend(MailTask mailTask)
        {
            var OauthWrapper = _initOauthWrapper(mailTask.OauthProvider);
            if (OauthWrapper.IsAccessTokenExpired)
            {

                try
                {
                    OauthWrapper.RefreshAccessToken();
                }
                catch
                {
                    Services.AsyncSendMailService.PutErrorStatus(mailTask.TaskId, "Failed to refresh access token.");
                    return Content(mailTask.TaskId);
                }
            }

            mailTask.AuthType = SmtpAuthType.XOAUTH2;
            mailTask.User = OauthWrapper.Provider.UserEmail;
            mailTask.Password = OauthWrapper.Provider.AccessToken;
            mailTask.IsAuthenticationRequired = true;

            // always set From to authenticated user.
            mailTask.Sender = OauthWrapper.Provider.UserEmail;

            Services.AsyncSendMailService.CreateAsyncTask(mailTask);
            return Content(mailTask.TaskId);
        }

        public ActionResult QueryAsyncTask(string id)
        {
            Services.AsyncSendMailStatus status = Services.AsyncSendMailService.QueryStatus(id);
            if (status == null)
            {
                status = new Services.AsyncSendMailStatus();
                status.TaskId = id;
                status.Completed = true;
                status.HasError = true;
                status.Status = "Invalid Task ID";
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ClearToken()
        {
            var OauthWrapper = _initOauthWrapper(OauthProvider.GoogleSmtpProvider);
            OauthWrapper.Provider.ClearToken();
            OauthWrapper.AuthorizationCode = "";
            return Content("OK");
        }

        private OauthHttpWrapper _initOauthWrapper(int providerType)
        {
            var OauthWrapper = Session["OauthWrapper"] as OauthHttpWrapper;
            if (OauthWrapper != null && OauthWrapper.Provider.ProviderType == providerType)
            {
                return OauthWrapper;
            }

            OauthProvider provider = null;
            switch (providerType)
            {
                case OauthProvider.MsLiveProvider:
                    provider = OauthProvider.CreateMsLiveProvider();
                    break;
                case OauthProvider.MsOffice365Provider:
                    provider = OauthProvider.CreateMsOffice365Provider();
                    break;
                case OauthProvider.GoogleGmailApiProvider:
                    provider = OauthProvider.CreateGoogleGmailApiProvider();
                    break;
                default:
                    provider = OauthProvider.CreateGoogleSmtpProvider();
                    break;
            }
            OauthWrapper = new OauthHttpWrapper(provider);
            Session["OauthWrapper"] = OauthWrapper;
            return OauthWrapper;
        }
    }
}