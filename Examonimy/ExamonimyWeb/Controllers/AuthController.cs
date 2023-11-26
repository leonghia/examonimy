using AutoMapper;
using ExamonimyWeb.DTOs.UserDTO;
using ExamonimyWeb.Entities;
using ExamonimyWeb.Managers.UserManager;
using ExamonimyWeb.Models;
using ExamonimyWeb.Services.AuthService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

namespace ExamonimyWeb.Controllers
{
    [Route("")]
    public class AuthController : GenericController<User>
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        private readonly IUserManager _userManager;
        private readonly IConfiguration _jwtConfigurations;

        public AuthController(IAuthService authService, IMapper mapper, IUserManager userManager, IConfiguration configuration)
        {
            _authService = authService;
            _mapper = mapper;
            _userManager = userManager;
            _jwtConfigurations = configuration.GetSection("JwtConfigurations");
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
            var operationResult = await _userManager.CreateAsync(user, userRegisterDto.Password);

            if (!operationResult.Succeeded)
            {
                var errors = new ExpandoObject();
                foreach (var error in operationResult.Errors!)
                {
                    errors.TryAdd(error.Code, new string[] { error.Description });
                }

                var problemDetails = new CustomProblemDetails(HttpContext.Request.Path, "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.8", StatusCodes.Status409Conflict, "registration");

                problemDetails.Extensions.TryAdd("errors", errors);

                return Conflict(problemDetails);
            }

            return Accepted();
        }

        [HttpPost("api/auth/login")]
        [Consumes("application/json")]
        [Produces("application/json", "application/problem+json")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var user = await _authService.ValidateUserAsync(userLoginDto);

            if (user is null)
            {
                var problemDetails = new CustomProblemDetails(HttpContext.Request.Path, "https://datatracker.ietf.org/doc/html/rfc7235#section-3.1", StatusCodes.Status401Unauthorized, "login");

                problemDetails.Extensions.TryAdd("errors", new { credentials = "Invalid username or password." });

                return Unauthorized(problemDetails);
            }

            var jwt = _authService.CreateJwt();
            var refreshToken = _authService.CreateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(Convert.ToDouble(_jwtConfigurations["RefreshTokenLifetime"]));

            await _userManager.UpdateAsync(user);

            return Ok(new AuthenticatedResponse { Token = jwt, RefreshToken = refreshToken });
        }

        [HttpPost("api/auth/refresh")]
        [Consumes("application/json")]
        [Produces("application/json", "application/problem+json")]
        public async Task<IActionResult> Refresh([FromBody] RefreshRequest refreshRequest)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var claimsIdentity = await _authService.GetClaimsIdentityFromTokenAsync(refreshRequest.AccessToken);
            var username = claimsIdentity.Name;

            if (username is null)
                return BadRequest();

            var user = await _userManager.FindByUsernameAsync(username);

            if (user is null)
            {
                return NotFound();
            }

            if (!user.RefreshToken!.Equals(refreshRequest.RefreshToken) || user.RefreshTokenExpiryTime <= DateTime.UtcNow) 
            {
                return Conflict();
            }

            var newAccessToken = _authService.CreateJwt();
            var newRefreshToken = _authService.CreateRefreshToken();

            user.RefreshToken = newRefreshToken;
            await _userManager.UpdateAsync(user);

            return Ok(new AuthenticatedResponse { RefreshToken = newRefreshToken, Token = newAccessToken });
        }

        [Authorize]
        [HttpPost("api/auth/revoke")]
        public async Task<IActionResult> Revoke()
        {
            var username = User.Identity!.Name;

            var user = await _userManager.FindByUsernameAsync(username!);

            if (user is null)
                return BadRequest();

            user.RefreshToken = null;

            await _userManager.UpdateAsync(user);

            return NoContent();
        }
    }
}
