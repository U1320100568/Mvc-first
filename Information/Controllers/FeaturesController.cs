using Information.Infrastructure;
using Information.Models;
using Information.Service;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Information.Controllers
{
    [UserAuthorize]
    public class FeaturesController : Controller
    {
        
        private CrudRepository<Feature> featRepo = new CrudRepository<Feature>();
        private AccountService accountService = new AccountService();

        [UserAuthorize]
        public ActionResult Index()
        {
            ExportService<Feature> exportService = new ExportService<Feature>();
            ViewBag.ExportColumns = exportService.GetSelectList();
            return View(featRepo.GetAll().ToList());
        }

        // GET: Features
        [UserAuthorize]
        public ActionResult Edit(int? id)
        {
            if(id == null)
            {
                throw new ArgumentNullException("id");
            }
            Feature feature = featRepo.Get(f => f.ID == id);
            if (feature == null)
            {
                return HttpNotFound();
            }
            return View(feature);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthorize]
        public ActionResult Edit([Bind (Include = "ID, MemberId, FeatInfor, FeatLogRec")]Feature feature)
        {
            if(ModelState.IsValid)
            {
                featRepo.Update(feature);
                return View("Index", featRepo.GetAll().ToList());
            }
            
            return View(feature);
        }

        [UserAuthorize]
        public void Create(int id)
        {
            accountService.CreateAccount(id);
        }

        [UserAuthorize]
        public void Delete(int id)
        {
            Feature feature = featRepo.Get(f => f.MemberId == id);
            featRepo.Delete(feature);
        }
        protected override void Dispose(bool disposing)
        {
            /**/
            if (disposing)
            {
                featRepo.Dispose();
            }
            base.Dispose(disposing);
            

        }
    }
}