using AutoMapper;
using ExamonimyWeb.DTOs.UserDTO;
using ExamonimyWeb.Managers.UserManager;
using ExamonimyWeb.Models;
using ExamonimyWeb.Services.AuthService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

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

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var username = HttpContext.User.Identity!.Name;           
            var user = await _userManager.FindByUsernameAsync(username!);
            if (user is null)
                return Forbid();
            var role = _userManager.GetRole(user);
            var userGetDto = _mapper.Map<UserGetDto>(user);
            return View(role, userGetDto);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}