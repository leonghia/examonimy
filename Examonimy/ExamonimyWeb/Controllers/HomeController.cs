using AutoMapper;
using ExamonimyWeb.Attributes;
using ExamonimyWeb.DTOs.UserDTO;
using ExamonimyWeb.Managers.UserManager;
using ExamonimyWeb.Models;
using ExamonimyWeb.Services.AuthService;
using ExamonimyWeb.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ExamonimyWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserManager _userManager;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, IUserManager userManager, IAuthService authService, IMapper mapper)
        {
            _logger = logger;
            _userManager = userManager;
            _authService = authService;
            _mapper = mapper;
        }

        [CustomAuthorize]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var username = HttpContext.User.Identity!.Name;           
            var user = await _userManager.FindByUsernameAsync(username!);
            if (user is null)
                return Forbid();
            var role = _userManager.GetRole(user);          
            var authorizedViewModel = new AuthorizedViewModel { User = _mapper.Map<UserGetDto>(user) };
            return View(role, authorizedViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}