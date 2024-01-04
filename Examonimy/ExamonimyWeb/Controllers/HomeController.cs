using AutoMapper;
using ExamonimyWeb.Attributes;
using ExamonimyWeb.DTOs.UserDTO;
using ExamonimyWeb.Managers.UserManager;
using ExamonimyWeb.Models;
using ExamonimyWeb.Services.AuthService;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ExamonimyWeb.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserManager _userManager;       
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, IUserManager userManager, IAuthService authService, IMapper mapper) : base(userManager)
        {
            _logger = logger;
            _userManager = userManager;           
            _mapper = mapper;
        }

        [CustomAuthorize]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await base.GetContextUser();
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