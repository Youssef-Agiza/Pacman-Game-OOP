using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Mvc;
using WebProject1.Models;

namespace WebProject1.Controllers
{
    public class MassController : Controller
    {
        // GET: Mass
        public ActionResult Index()
        {
            var mailTask = new MailTask();
            mailTask.Subject = "Test email from ASP.NET MVC";
            mailTask.IsAuthenticationRequired = false;

            var body = new StringBuilder();
            body.Append("This sample demonstrates how to send multiple emails from ASP.NET MVC with thread pool.\r\n\r\n");
            body.Append("From: [$sender]\r\n");
            body.Append("To: [$rcpt]\r\n");
            body.Append("Subject: [$subject]\r\n\r\n");

            body.Append("Above sender, rcpt, subject values will be replaced by actual value based on each recipient.\r\n\r\n");
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
        public ActionResult AsyncSend(MailTask mailTask)
        {
            Services.MassSendMailService.CreateAsyncTask(mailTask);
            return Content(mailTask.TaskId);
        }

        public ActionResult QueryAsyncTask(string id)
        {
            var status = Services.MassSendMailService.QueryStatus(id);
            if (status == null)
            {
                status = new Services.MassSendMailStatus();
                status.TaskId = id;
                status.Completed = true;
                status.HasError = true;
                status.Status = "Invalid Task ID";
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }

    }
}