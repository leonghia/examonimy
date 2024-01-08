using ExamonimyWeb.DTOs.UserDTO;
using ExamonimyWeb.Entities;
using ExamonimyWeb.Enums;
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

    protected async Task<(User, UserGetDto)> GetContextUser()
    {
        var roleIdMappings = new Dictionary<int, string>
        {
            { 1, Enums.Role.Admin.ToString() },
            { 2, Enums.Role.Teacher.ToString() },
            { 3, Enums.Role.Student.ToString() }
        };
        var username = HttpContext.User?.Identity?.Name ?? throw new Exception();
        var user = await _userManager.FindByUsernameAsync(username) ?? throw new Exception();
        return (user, new UserGetDto { FullName = user.FullName, Id = user.Id, ProfilePicture = user.ProfilePicture, Role = roleIdMappings[user.RoleId] });
    }
}
