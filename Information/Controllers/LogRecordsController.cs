using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Information.Helpers;
using Information.Infrastructure;
using Information.Models;
using Information.Service;

namespace Information.Controllers
{
    [UserAuthorize]
    public class LogRecordsController : Controller
    {
        //private AppDbContext db = new AppDbContext();
        private CrudRepository<LogRecord> logRecRepo = new  CrudRepository<LogRecord>();
        private CrudRepository<Member> memberRepo = new CrudRepository<Member>();
        private AccountService accountService = new AccountService();

        // GET: LogRecords
        [UserAuthorize]
        public ActionResult Index()
        {
            string user = WebSiteHelper.UserName;
            ViewBag.Title = user;
            if(user == "admin")
            {
                ViewBag.Title = "會員" ;
                return View(logRecRepo.GetAll());
            }

            
            int memberId = memberRepo.Get(m => m.Name == user).ID;
            return View(logRecRepo.GetAll().Where(r => r.MemberId == memberId).ToList());
        }

        // GET: LogRecords/Details/5
        [UserAuthorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LogRecord logRecord = logRecRepo.Get(r => r.ID == id);
            if (logRecord == null)
            {
                return HttpNotFound();
            }
            return View(logRecord);
        }



        /*
        public void Create([Bind(Include = "ID") ]int ID)
        {
            accountService.Login(ID);
        }
        */

        /*
        public void Edit(int id)
        {
            accountService.Logout(id);
        }
        */

        // GET: LogRecords/Delete/5
        [UserAuthorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LogRecord logRecord = logRecRepo.Get(r => r.ID == id);
            
            if (logRecord == null)
            {
                return HttpNotFound();
            }
            return View(logRecord);
        }

        // POST: LogRecords/Delete/5
        [UserAuthorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LogRecord logRecord = logRecRepo.Get(r => r.ID == id);
            logRecRepo.Delete(logRecord);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                logRecRepo.Dispose();
                memberRepo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
