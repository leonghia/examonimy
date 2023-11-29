using AutoMapper;
using ExamonimyWeb.Attributes;
using ExamonimyWeb.DTOs.UserDTO;
using ExamonimyWeb.Managers.UserManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExamonimyWeb.Controllers
{
    public class QuestionController : Controller
    {
        private readonly IUserManager _userManager;
        private readonly IMapper _mapper;

        public QuestionController(IUserManager userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        [CustomAuthorize(Roles = "Administrator")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var username = HttpContext.User.Identity!.Name;
            var user = await _userManager.FindByUsernameAsync(username!);
            var role = _userManager.GetRole(user!);
            var userGetDto = _mapper.Map<UserGetDto>(user);
            return View(role, userGetDto);
        }

        [CustomAuthorize(Roles = "Administrator")]
        [HttpGet]
        public IActionResult Create()
        {

        }
    }
}
