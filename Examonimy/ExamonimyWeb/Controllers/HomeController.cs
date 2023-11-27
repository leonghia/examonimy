using ExamonimyWeb.Helpers;
using ExamonimyWeb.Managers.UserManager;
using ExamonimyWeb.Models;
using ExamonimyWeb.Services.AuthService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ExamonimyWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserManager _userManager;
        private readonly IAuthService _authService;

        public HomeController(ILogger<HomeController> logger, IUserManager userManager, IAuthService authService)
        {
            _logger = logger;
            _userManager = userManager;
            _authService = authService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Request.Cookies["token"];
            if (token is null)
                return RedirectToRoute("GetLoginView");
            var claimsIdentity = await _authService.GetClaimsIdentityFromTokenAsync(token);
            if (claimsIdentity is null)
                return RedirectToRoute("GetLoginView");
            var username = claimsIdentity.Name;
            var user = await _userManager.FindByUsernameAsync(username!);
            if (user is null)
                return RedirectToRoute("GetLoginView");
            var role = _userManager.GetRole(user);
            return View(role);
        }     

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}