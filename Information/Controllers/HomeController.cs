using Information.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Information.Controllers
{
    public class HomeController : Controller
    {
        MemberDbContext db = new MemberDbContext(); 
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return RedirectToAction("Index","Infors");
        }
        
        public ActionResult Login()
        {
            ViewBag.Message = "Your Login page.";
            
            /*
            switch (GlobalVariable.UserID)//
            {
                case 1:
                    return RedirectToAction("Index","Members");
                case 0:
                    return View();
                
            }
            */
            switch (User.Identity.Name)
            {
                case "admin":
                    return RedirectToAction("Index", "Members");
                case "":
                    return View();
                default:
                    return RedirectToAction("Index", "Infors");
            }

            
        }
        [HttpPost]
        public ActionResult Login(Member m)
        {
            if ((!String.IsNullOrEmpty(m.Name)) && (!String.IsNullOrEmpty(m.Password)))
            {

                if (db.Members.FirstOrDefault(s => s.Name == m.Name)==null)
                {
                    ViewBag.errorMsg = "account not found"; //判斷有無此帳號
                    
                    return View(); 
                }
                var member = db.Members.FirstOrDefault(s => s.Name == m.Name );
                if (!member.Password.Equals(m.Password))
                {
                    ViewBag.errorMsg = "wrong password";  //判斷密碼
                    return View();
                }
                //Authenticaion
                var now = DateTime.Now;
                string userdata=member.Name;
                var ticket = new FormsAuthenticationTicket(
                        version: 1,
                        name: m.Name,
                        issueDate: now,
                        expiration: now.AddMinutes(30),
                        isPersistent:false,
                        userData:userdata,
                        cookiePath:FormsAuthentication.FormsCookiePath
                    );
                var encryptedTicket = FormsAuthentication.Encrypt(ticket);
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                Response.Cookies.Add(cookie);
                //Authenticaion

                //GlobalVariable.UserID = member.ID;
                if ( !member.Name.Equals("admin")) {    //member.ID !=1  
                   
                    return  RedirectToAction("Index", "Infors");  //不是admit
                }

            }
            
            
            return RedirectToAction("Index","Members");

        }

        public ActionResult Logout()
        {
            //Authentitcation
            FormsAuthentication.SignOut();
            Session.RemoveAll();
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie);
            //Authentication
            GlobalVariable.UserID = 0;
            return RedirectToAction("Login", "Home");
        }

    }
}