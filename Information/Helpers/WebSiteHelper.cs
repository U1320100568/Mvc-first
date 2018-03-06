using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Information.Helpers
{
    public static class WebSiteHelper
    {
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
    }
}