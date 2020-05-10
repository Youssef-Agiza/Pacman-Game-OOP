using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Mvc;
using WebProject1.Models;
using EASendMail;

namespace WebProject1.Controllers
{
    public class SimpleMailController : Controller
    {
        // GET: SimpleMail
        public ActionResult Index()
        {
            var mailTask = new MailTask();
            mailTask.Subject = "Test email from ASP.NET MVC";
            mailTask.IsAuthenticationRequired = false;

            var body = new StringBuilder();
            body.Append("This sample demonstrates how to send simple email from ASP.NET MVC.\r\n\r\n");
            body.Append("If no sever address was specified, the email will be delivered to the recipient's server directly.\r\n");
            body.Append("However, it is not recommended, because most email providers would reject your message due to anti-spam policy.\r\n");

            mailTask.TextBody = body.ToString();

            ViewBag.Port = DropDownListData.PortList(mailTask.Port);
            ViewBag.Protocol = DropDownListData.ProtocolList(mailTask.Protocol);

            ViewBag.IsSyncSendSucceeded = false;
            ViewBag.SyncSendStatus = string.Empty;

            return View(mailTask);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(MailTask mailTask)
        {
            ViewBag.Port = DropDownListData.PortList(mailTask.Port);
            ViewBag.Protocol = DropDownListData.ProtocolList(mailTask.Protocol);

            if (ModelState.IsValid)
            {
                _syncSendMail(mailTask);
            }

            return View(mailTask);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AsyncSend(MailTask mailTask)
        {
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

        void _syncSendMail(MailTask mailTask)
        {
            try
            {
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
        }
        
    }
}