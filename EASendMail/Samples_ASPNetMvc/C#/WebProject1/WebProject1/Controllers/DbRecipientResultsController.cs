using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebProject1.Models;

namespace WebProject1.Controllers
{
    public class DbRecipientResultsController : Controller
    {
        private WebProject1Context db = new WebProject1Context();

        // GET: DbMailResults
        public ActionResult Index(int? id)
        {
            var results = (id == null) ?
                db.DbRecipientResults.ToList() :
                db.DbRecipientResults.Where(item => item.TaskId == id).ToList();

            return View(results);
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
