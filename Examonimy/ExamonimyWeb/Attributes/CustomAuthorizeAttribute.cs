using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace ExamonimyWeb.Attributes
{
    public class CustomAuthorizeAttribute : ActionFilterAttribute
    {

        public string? Roles { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.User.Identity is null || !context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new RedirectToRouteResult("GetLoginView", null);
            }

            var user = context.HttpContext.User;

            if (Roles is not null && !user.IsInRole(Roles))
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
