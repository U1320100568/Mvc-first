using Information.Helpers;
using Information.Models;
using Information.Service;
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
        
        private AccountService accountService = new AccountService();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return RedirectToAction("Index","Infors");
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            ViewBag.Message = "Your Login page.";
            
            
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
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Login([Bind (Include = "Name,Password")]Member m)
        {
            //驗證帳密
            var member = accountService.Login(m);
            if (member != null)
            {
                //Authenticaion
                WebSiteHelper.Authentication(member);
                //Authenticaion

               
               
                if (!member.Name.Equals("admin"))
                {   
                  
                    string controllerName = WebSiteHelper.GetFeature(member.Name).GetFirstAccessFeature();
                    if(controllerName != null)
                    {
                        
                        return RedirectToAction("Index", controllerName);
                    }
                    else
                    {
                        return RedirectToAction("Logout", "Home");
                    }
                    
                }
                return RedirectToAction("Index", "Members");
            }
            else {
                ViewBag.errorMsg = "wrong name or password";
               
            }
            return View();


        }
        
        
        public ActionResult Logout()
        {
            //登出
            accountService.Logout(User.Identity.Name.ToString());
            //Authentitcation
            FormsAuthentication.SignOut();
            Session.RemoveAll();
            // 建立一個同名的 Cookie 來覆蓋原本的 Cookie
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie);

            // 建立 ASP.NET 的 Session Cookie 同樣是為了覆蓋
            HttpCookie cookie2 = new HttpCookie("ASP.NET_SessionId", "");
            cookie2.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie2);
            //Authentication
            
            
            return RedirectToAction("Login", "Home");
        }

    }
}