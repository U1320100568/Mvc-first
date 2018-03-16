using Information.Helpers;
using Information.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;

namespace Information.Infrastructure
{
    public class UserAuthorizeAttribute : AuthorizeAttribute
    {
        public string AuthorizationFailView { get; set; }
        private bool featureAccess ;
        //請求授權時執行
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //獲得url請求裡的controller和action
            string controllerName =
                filterContext.RouteData.Values["controller"].ToString().ToString();
            Feature feature = WebSiteHelper.GetFeature();
            featureAccess = false;
            switch (controllerName)
            {
                case "Infors":
                    if(feature.FeatInfor)
                    {
                        featureAccess = true;
                    }
                    break;
                case "LogRecords":
                    if(feature.FeatLogRec)
                    {
                        featureAccess = true;
                    }
                    break;
                default:
                    break;
            }


            base.OnAuthorization(filterContext);//進入AuthorizeCore
        }

        //自定義授權檢查 (return false 則失敗)
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if(httpContext.User.Identity.IsAuthenticated)
            {
                string userName = httpContext.User.Identity.Name;
                if (userName == "admin")
                {
                    featureAccess = true;
                }
                
                
            }
            //return base.AuthorizeCore(httpContext);
            return featureAccess;
            //return true;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            Feature feature = WebSiteHelper.GetFeature();
            string controllerName = feature.GetFirstAccessFeature();
            if(controllerName != null)
            {
                //導到別頁
                filterContext.HttpContext.Response.RedirectToRoute(new { controller = controllerName, action = "Index"});
            }
            else
            {
                //用URL
                filterContext.HttpContext.Response.Redirect("~/Home/Logout");
            }
            
           
            //filterContext.Result = new ViewResult { ViewName = AuthorizationFailView };
        }
    }
}