using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ExamonimyWeb.Attributes
{
    public class CustomAuthorizeAttribute : ActionFilterAttribute
    {

        public string? Roles { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.User.Identity is null || !context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new RedirectToRouteResult("RenderLoginView", null);
                return;
            }

            var user = context.HttpContext.User;
            if (Roles is not null)
            {
                var isInRole = false;
                var rolesArr = Roles.Split(",");
                foreach (var role in rolesArr)
                {
                    if (user.IsInRole(role))
                    {
                        isInRole = true;
                        break;
                    }
                }
                if (!isInRole)
                {
                    context.Result = new ForbidResult();
                    return;
                }
            }
        }
    }
}
