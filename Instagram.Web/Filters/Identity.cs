using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
namespace Instagram.Web.Filters
{
    public abstract class Identity : ActionFilterAttribute
    {
        public string ControllerName { get; set; }
        public string Namespace { get; set; }

        protected DbContext GetDbContext(ActionExecutingContext context)
        {
            var type = Type.GetType(Namespace + "." + ControllerName);
            var thisController = Convert.ChangeType(context.Controller, type);

            var dbContext =
                thisController.GetType().GetField("_context", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(context.Controller) as DbContext;

            return dbContext;
        }
    }
}
