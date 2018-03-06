using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Information.Models;

namespace Information.Controllers
{
    public class InforsController : Controller
    {
        private InforDbContext db = new InforDbContext();

        // GET: Infors
        public ActionResult Index()
        {
            //if (GlobalVariable.UserID == 1) { ViewBag.admin = "1";  }
            //if (GlobalVariable.UserID != 0) { ViewBag.guest = "1"; }

            return View(db.Informations.ToList());
        }

        // GET: Infors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Infor infor = db.Informations.Find(id);
            if (infor == null)
            {
                return HttpNotFound();
            }
            return View(infor);
        }

        // GET: Infors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Infors/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,publisher,ReleaseTime,content")] Infor infor)
        {
            if (ModelState.IsValid)
            {
                db.Informations.Add(infor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(infor);
        }

        // GET: Infors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Infor infor = db.Informations.Find(id);
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
        public ActionResult Edit([Bind(Include = "ID,Title,publisher,ReleaseTime,content")] Infor infor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(infor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(infor);
        }

        // GET: Infors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Infor infor = db.Informations.Find(id);
            if (infor == null)
            {
                return HttpNotFound();
            }
            return View(infor);
        }

        // POST: Infors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Infor infor = db.Informations.Find(id);
            db.Informations.Remove(infor);
            db.SaveChanges();
            return RedirectToAction("Index");
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
