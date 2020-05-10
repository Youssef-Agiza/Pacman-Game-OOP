using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebProject1.Controllers;
using WebProject1.Models;

namespace WebProject1
{
    public class DbMailTasksController : Controller
    {
        private WebProject1Context db = new WebProject1Context();

        // GET: DbMailTasks
        public ActionResult Index()
        {
            return TaskList();
        }

        public ActionResult TaskList()
        {
            return PartialView(db.DbMailTasks.ToList());
        }

        // GET: DbMailTasks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            DbMailTask dbMailTask = db.DbMailTasks.Find(id);
            if (dbMailTask == null)
            {
                return HttpNotFound();
            }
            return View(dbMailTask);
        }


        // GET: DbMailTasks/Create
        public ActionResult Create()
        {
            ViewBag.RecipientCount = db.DbRecipients.Count();

            var mailTask = new DbMailTask();
            mailTask.Subject = "Test email from ASP.NET MVC";
            mailTask.IsAuthenticationRequired = false;

            var body = new StringBuilder();
            body.Append("This sample demonstrates how to pickup recipient from database and send emails from ASP.NET MVC with thread pool.\r\n");
            body.Append("Curren task id is: [$taskid]\r\n\r\n");
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

        // POST: DbMailTasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DbMailTask dbMailTask)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrWhiteSpace(dbMailTask.TaskName))
                {
                    dbMailTask.TaskName = "Unnamed task";
                }

                dbMailTask.CreationTime = DateTime.Now;
                dbMailTask.LastWriteTime = DateTime.Now;
                db.DbMailTasks.Add(dbMailTask);
                db.SaveChanges();
                return RedirectToAction("Index", "DbRecipients");
            }

            return View(dbMailTask);
        }

        // GET: DbMailTasks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DbMailTask dbMailTask = db.DbMailTasks.Find(id);
            if (dbMailTask == null)
            {
                return HttpNotFound();
            }
            return View(dbMailTask);
        }

        // POST: DbMailTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DbMailTask dbMailTask = db.DbMailTasks.Find(id);
            if (dbMailTask.Status == DbMailTaskStatus.Completed || dbMailTask.Status == DbMailTaskStatus.Terminated)
            {
                db.DbMailTasks.Remove(dbMailTask);
                db.Database.ExecuteSqlCommand("DELETE FROM DbRecipientResults WHERE TaskId = {0}", id);

                db.SaveChanges();
            }
            return RedirectToAction("Index", "DbRecipients");
        }

        public ActionResult QueryTasks()
        {
            var tasks = db.DbMailTasks
                    .Select(p => new {
                        p.TaskId,
                        p.TaskName,
                        p.TotalCount,
                        Status = p.Status.ToString(),
                        p.Failed,
                        p.Succeeded,
                        p.CreationTime }).ToList();
            return Json(tasks, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
