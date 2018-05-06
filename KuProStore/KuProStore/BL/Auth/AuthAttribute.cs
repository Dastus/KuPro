using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using KuProStore.BL.Auth;
using KuProStore.BL.Service;

namespace KuProStore.BL.Auth
{
    public class AuthAttribute : FilterAttribute, IAuthenticationFilter
    {
        private IUserManager userManager;
        private AuthHelper authHelper;
        public AuthAttribute(IUserManager userManager)
        {
            this.userManager = userManager;
            this.authHelper = new AuthHelper(userManager);
        }

        public void OnAuthentication(AuthenticationContext filterContext)
        {
            if (!authHelper.IsAuthenticated(filterContext.HttpContext))
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            
            if (authHelper.IsAuthenticated(filterContext.HttpContext))
            {
                filterContext.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary {
                    { "controller", "Home" }, { "action", "Index" }
                   });
            }
        }
    }
}