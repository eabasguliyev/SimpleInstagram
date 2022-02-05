using Instagram.Web.Data;
using Instagram.Web.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Reflection;
using System.Linq;
using Instagram.Web.Utilities;

namespace Instagram.Web.Filters
{
    public class RequireAuthorization:Identity
    {
        public UserType UserType { get; set; }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var dbContext = GetDbContext(context);

            string token = context.HttpContext.Request.Cookies["token"];

            var url = "";

            if (string.IsNullOrEmpty(token))
            {
                url = SD.URL_UserLoginPage;
            }
            else
            {
                var userFromDb = dbContext.Set<User>().FirstOrDefault(u => u.Token == token);

                if (UserType == UserType.User && userFromDb is Admin)
                {
                    url = SD.URL_AdminDefaultPage;
                }
                else if (UserType == UserType.Admin && userFromDb is User)
                {
                    url = SD.URL_UserDefaultPage;
                }
            }

            if (!string.IsNullOrEmpty(url))
            {
                context.Result = new RedirectResult(url);
            }

            base.OnActionExecuting(context);
        }
    }

    public enum UserType
    {
        User, Admin
    }
}
