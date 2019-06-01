using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

namespace DrinksNPicsAdmin.Filters
{
    public class GlobalFilter : IActionFilter{

        public void OnActionExecuting(ActionExecutingContext context)
        {                        
            if (context.RouteData.Values["controller"].ToString().Equals("Account", StringComparison.CurrentCultureIgnoreCase))       
                return;                           
            if (context.HttpContext.Request.Cookies["User"] == null)                
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(new { controller = "Account", action = "Login" })
                );
        }

        public void OnActionExecuted(ActionExecutedContext context) {
            
        }
    }
}