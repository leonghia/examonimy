using AutoMapper;
using ExamonimyWeb.Attributes;
using ExamonimyWeb.DTOs.UserDTO;
using ExamonimyWeb.Managers.UserManager;
using Microsoft.AspNetCore.Mvc;

namespace ExamonimyWeb.Controllers
{
    [Route("")]
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
        [HttpGet("question")]
        public async Task<IActionResult> Index()
        {
            var username = HttpContext.User.Identity!.Name;
            var user = await _userManager.FindByUsernameAsync(username!);
            var role = _userManager.GetRole(user!);
            var userGetDto = _mapper.Map<UserGetDto>(user);
            return View("Bank", userGetDto);
        }

        [CustomAuthorize(Roles = "Administrator")]
        [HttpGet("question/add")]
        public async Task<IActionResult> Create()
        {
            var username = HttpContext.User.Identity!.Name;
            var user = await _userManager.FindByUsernameAsync(username!);
            var role = _userManager.GetRole(user!);
            var userGetDto = _mapper.Map<UserGetDto>(user);
            return View(userGetDto);
        }
    }
}
