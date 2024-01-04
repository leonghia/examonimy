using ExamonimyWeb.Entities;
using ExamonimyWeb.Managers.UserManager;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;

namespace ExamonimyWeb.Controllers;

public class BaseController : Controller
{
         
    private readonly IUserManager _userManager;

    public BaseController(IUserManager userManager)
    {
             
        _userManager = userManager;
    }       

    public override ActionResult ValidationProblem([ActionResultObjectValue] ModelStateDictionary modelStateDictionary)
    {
        var options = HttpContext.RequestServices.GetRequiredService<IOptions<ApiBehaviorOptions>>();

        return (ActionResult)options.Value.InvalidModelStateResponseFactory(ControllerContext);
    }

    protected async Task<User> GetContextUser()
    {
        var username = HttpContext.User.Identity!.Name;
        var user = await _userManager.FindByUsernameAsync(username!);
        return user!;
    }
}
