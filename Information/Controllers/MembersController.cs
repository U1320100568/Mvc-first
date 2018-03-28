using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Information.Infrastructure;
using Information.Models;
using Information.Service;


namespace Information.Controllers
{
    [UserAuthorize]
    public class MembersController : Controller
    {
        private CrudRepository<Member> memberRepo= new CrudRepository<Member>();
        private AccountService accountService = new AccountService();
        

        // GET: Members
        [UserAuthorize]
        public ActionResult Index()
        {
            ExportService<Member> exportService = new ExportService<Member>();
            ViewBag.ExportColumns = exportService.GetSelectList();
            return View(memberRepo.GetAll().ToList());
            
        }

        // GET: Members/Details/5
        [UserAuthorize]
        public ActionResult Details(int? id)
        {
            
            Member member = memberRepo.Get(m => m.ID == id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // GET: Members/Create
        [UserAuthorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Members/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthorize]
        public ActionResult Create([Bind(Include = "ID,PowerID,Password,Name")] Member member)
        {
            if (ModelState.IsValid)
            {
                memberRepo.Create(member);
                accountService.CreateAccount(member.ID);
                return RedirectToAction("Index");
            }

            return View(member);
        }

        // GET: Members/Edit/5
        [UserAuthorize]
        public ActionResult Edit(int? id)
        {
            Member member = memberRepo.Get(m => m.ID == id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: Members/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthorize]
        public ActionResult Edit([Bind(Include = "ID,PowerID,Password,Name")] Member member)
        {
            if (ModelState.IsValid)
            {
                memberRepo.Update(member);
                return RedirectToAction("Index");
            }
            return View(member);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthorize]
        public ActionResult ResetPassword(int? id)
        {
            Member member = memberRepo.Get(m => m.ID == id);
            if (member == null)
            {
                return HttpNotFound();
            }
            string queryString = "ResetPassword" + " " + id;
            memberRepo.SqlQuery(queryString);
            return RedirectToAction("Index");
        }

        // GET: Members/Delete/5
        [UserAuthorize]
        public ActionResult Delete(int? id)
        {
            Member member = memberRepo.Get(m => m.ID == id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [UserAuthorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Member member = memberRepo.Get(m => m.ID == id);
            FeaturesController featuresController = new FeaturesController();
            featuresController.Delete(id);
            memberRepo.Delete(member);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            /**/
            if (disposing)
            {
                //db.Dispose();
                memberRepo.Dispose();

            }
            base.Dispose(disposing);
            
           
        }
    }
}
