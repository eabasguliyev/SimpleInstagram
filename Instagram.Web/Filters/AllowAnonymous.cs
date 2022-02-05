using Instagram.Web.Entities;
using Instagram.Web.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace Instagram.Web.Filters
{
    public class AllowAnonymous : Identity
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var dbContext = GetDbContext(context);


            string token = context.HttpContext.Request.Cookies["token"];

            var url = "";


            if (!String.IsNullOrEmpty(token))
            {
                var userFromDb = dbContext.Set<User>().FirstOrDefault(u => u.Token == token);

                if (userFromDb is Admin)
                {
                    url = SD.URL_AdminDefaultPage;
                }
                else if (userFromDb is User)
                {
                    url = SD.URL_UserDefaultPage;
                }

                if (!string.IsNullOrEmpty(url))
                {
                    context.Result = new RedirectResult(url);
                }
            }

            base.OnActionExecuting(context);
        }
    }
}
