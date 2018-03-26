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
    public class InforsController : Controller
    {
        
        private CrudRepository<Infor> inforRepo = new CrudRepository<Infor>();

        // GET: Infors
        [UserAuthorize]
        public ActionResult Index()
        {
            ExportService<Infor> exportService = new ExportService<Infor>();
            ViewBag.ExportColumns = exportService.GetSelectList();
            return View(inforRepo.GetAll().ToList());
        }

        // GET: Infors/Details/5
        [UserAuthorize]
        public ActionResult Details(int? id)
        {
            
            Infor infor = inforRepo.Get(i => i.ID == id);
            if (infor == null)
            {
                return HttpNotFound();
            }
            return View(infor);
        }

        // GET: Infors/Create
        [UserAuthorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Infors/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthorize]
        public ActionResult Create([Bind(Include = "ID,Title,Publisher,ReleaseTime,Content")] Infor infor)
        {
            if (ModelState.IsValid)
            {

                inforRepo.Create(infor);
                return RedirectToAction("Index");
            }

            return View(infor);
        }

        // GET: Infors/Edit/5
        [UserAuthorize]
        public ActionResult Edit(int? id)
        {
            
            Infor infor = inforRepo.Get(i => i.ID == id);
            if (infor == null)
            {
                return HttpNotFound();
            }
            return View(infor);
        }

        // POST: Infors/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthorize]
        public ActionResult Edit([Bind(Include = "ID,Title,Publisher,ReleaseTime,Content")] Infor infor)
        {
            if (ModelState.IsValid)
            {

                inforRepo.Update(infor);
                return RedirectToAction("Index");
            }
            return View(infor);
        }

        // GET: Infors/Delete/5
        [UserAuthorize]
        public ActionResult Delete(int? id)
        {
            
            Infor infor = inforRepo.Get(i => i.ID == id);
            if (infor == null)
            {
                return HttpNotFound();
            }
            return View(infor);
        }

        // POST: Infors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [UserAuthorize]
        public ActionResult DeleteConfirmed(int id)
        {
            
            Infor infor = inforRepo.Get(i => i.ID == id);
            inforRepo.Delete(infor);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
                inforRepo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
