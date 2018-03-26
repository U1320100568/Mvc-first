using Information.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Information.Helpers
{
    public static class WebSiteHelper
    {
        private static AppDbContext db = new AppDbContext();
        public static string UserName
        {
            get
            {
                var httpContext = HttpContext.Current;
                var identity = httpContext.User.Identity as FormsIdentity;
                    
                if (identity == null)
                {
                    return string.Empty;
                }
                else
                {
                    string username = identity.Name;
                    return username;
                }
            }   

        }
        


        public static string GetUserNameById(int id)//
        {
            return db.Members.Find(id).Name;
        }

        public static Feature GetFeature()
        {
            int userId = 0;

            Member member = db.Members.Where(m => m.Name == UserName).FirstOrDefault();
            if (member == null)
            {
                Feature feat = new Feature()
                {
                    FeatInfor = false,
                    FeatLogRec = false
                };
                return feat;
            }
            userId = member.ID;
            return db.Features.Where(f => f.MemberId == userId).FirstOrDefault();
        }

        public static Feature GetFeature(string name)
        {
            int userId = 0;

            Member member = db.Members.Where(m => m.Name == name).FirstOrDefault();
            if (member == null)
            {
                Feature feat = new Feature()
                {
                    FeatInfor = false,
                    FeatLogRec = false
                };
                return feat;
            }
            userId = member.ID;
            return db.Features.Where(f => f.MemberId == userId).FirstOrDefault();
        }

        public static HttpCookie Authentication(Member member)
        {
            var now = DateTime.Now;
            string userdata = member.Name;
            var ticket = new FormsAuthenticationTicket(
                    version: 1,
                    name: member.Name,
                    issueDate: now,
                    expiration: now.AddMinutes(30),
                    isPersistent: false,
                    userData: userdata,
                    cookiePath: FormsAuthentication.FormsCookiePath
                );
            var encryptedTicket = FormsAuthentication.Encrypt(ticket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            HttpContext.Current.Response.Cookies.Add(cookie);

            return cookie;
        }
    }
}