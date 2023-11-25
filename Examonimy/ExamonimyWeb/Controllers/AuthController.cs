using AutoMapper;
using ExamonimyWeb.DTOs.UserDTO;
using ExamonimyWeb.Entities;
using ExamonimyWeb.Repositories.UserRepository;
using ExamonimyWeb.Services.AuthService;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using System.Text.Json;

namespace ExamonimyWeb.Controllers
{
    [Route("")]
    public class AuthController : GenericController<User>
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public AuthController(IAuthService authService, IMapper mapper, IUserRepository userRepository)
        {
            _authService = authService;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet("register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("api/auth/register")]
        [Consumes("application/json")]
        [Produces("application/problem+json")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegisterDto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);
            var user = _mapper.Map<User>(userRegisterDto);
            var operationResult = await _userRepository.CreateAsync(user, userRegisterDto.Password);

            if (!operationResult.Succeeded)
            {
                var errors = new ExpandoObject();
                foreach (var error in operationResult.Errors!)
                {
                    errors.TryAdd(error.Code, new string[] { error.Description });
                }

                var problemDetails = new ProblemDetails()
                {
                    Detail = "See the errors field for details.",
                    Instance = HttpContext.Request.Path,
                    Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.8",
                    Status = StatusCodes.Status409Conflict,
                    Title = "One or more registration errors occured."                 
                };

                problemDetails.Extensions.Add("errors", errors);

                return Conflict(problemDetails);
            }

            return Accepted();
        }
    }
}
