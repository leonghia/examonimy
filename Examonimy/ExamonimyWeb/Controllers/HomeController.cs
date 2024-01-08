using ExamonimyWeb.Attributes;
using ExamonimyWeb.DTOs.UserDTO;
using ExamonimyWeb.Managers.UserManager;
using ExamonimyWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ExamonimyWeb.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserManager _userManager;       
        

        public HomeController(ILogger<HomeController> logger, IUserManager userManager) : base(userManager)
        {
            _logger = logger;
            _userManager = userManager;           
            
        }

        [CustomAuthorize]
        [HttpGet]
        public async Task<IActionResult> Index()
        {          
            var userToReturn = (await base.GetContextUser()).Item2;                     
            var authorizedViewModel = new AuthorizedViewModel { User = userToReturn };
            return View(userToReturn.Role.ToString(), authorizedViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}