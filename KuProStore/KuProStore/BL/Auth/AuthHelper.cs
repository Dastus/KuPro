using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KuProStore.BL.Service;
using KuProStore.Models;

namespace KuProStore.BL.Auth
{
    public class AuthHelper
    {
        private IUserManager userManager;

        public AuthHelper(IUserManager userManager)
        {
            this.userManager = userManager;
        }

        public void UserSetCookie(HttpContextBase context, User user, string value)
        {
            if (value == null)
                throw new Exception("Cookie is empty");

            HttpCookie cookie = new HttpCookie(Constants.NameCookie)
            {
                Value = value,
                Expires = DateTime.Now.AddDays(10)
            };

            context.Response.Cookies.Add(cookie);
            UpdateCookies(user, context.Response.Cookies[Constants.NameCookie].Value);
        }


        public void LogOutUser(HttpContextBase context)
        {
            if (context.Request.Cookies[Constants.NameCookie] != null)
            {
                HttpCookie cookie = new HttpCookie(Constants.NameCookie)
                {
                    Expires = DateTime.Now.AddDays(-1)
                };

                context.Response.Cookies.Add(cookie);
            }
        }

        public bool IsAuthenticated(HttpContextBase context)
        {
            HttpCookie cookie = context.Request.Cookies[Constants.NameCookie];

            if (cookie != null)
            {
                User user = userManager.GetUserByCookies(cookie.Value);

                return user != null;
            }
            return false;
        }

        private void UpdateCookies(User user, string cookie)
        {
            userManager.UpdateCookies(user, cookie);
        }
    }
}