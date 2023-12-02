using AutoMapper;
using ExamonimyWeb.Entities;
using ExamonimyWeb.Managers.UserManager;
using ExamonimyWeb.Models;
using ExamonimyWeb.Models.DTOs.UserDTO;
using ExamonimyWeb.Services.AuthService;
using ExamonimyWeb.Services.TokenService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _jwtConfigurations;
        private readonly string _tokenName;
        private readonly string _refreshTokenName;
        private readonly CookieOptions _cookieOptionsForTokenWithMaxAge = new()
        {         
            MaxAge = new TimeSpan(0, 15, 0),
            Path = "/",
            HttpOnly = true
        };

        private readonly CookieOptions _cookieOptionsForTokenWithoutMaxAge = new()
        {          
            Path = "/",
            HttpOnly = true
        };

        private readonly CookieOptions _cookieOptionsForRefreshTokenWithMaxAge = new()
        {
            MaxAge = new TimeSpan(7, 0, 0, 0),
            Path = "/api/auth/refresh",
            HttpOnly = true
        };

        private readonly CookieOptions _cookieOptionsForRefreshTokenWithoutMaxAge = new()
        {          
            Path = "/api/auth/refresh",
            HttpOnly = true
        };


        public AuthController(IAuthService authService, IMapper mapper, IUserManager userManager, IConfiguration configuration, ITokenService tokenService)
        {
            _authService = authService;
            _mapper = mapper;
            _userManager = userManager;
            _tokenService = tokenService;
            _jwtConfigurations = configuration.GetSection("JwtConfigurations");
            _tokenName = _jwtConfigurations["TokenName"]!;
            _refreshTokenName = _jwtConfigurations["RefreshTokenName"]!;
        }

        [HttpGet("login", Name = "GetLoginView")]
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

                problemDetails.Extensions.TryAdd("errors", new { credentials = "Email hoặc mật khẩu không chính xác." });

                return Unauthorized(problemDetails);
            }

            var jwt = _tokenService.CreateToken(user);
            var refreshToken = _tokenService.CreateRefreshToken();

            if (userLoginDto.RememberMe)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(Convert.ToDouble(_jwtConfigurations["RefreshTokenLifetime"]));

                await _userManager.UpdateAsync(user);

                Response.Cookies.Append(_tokenName, jwt, _cookieOptionsForTokenWithMaxAge);
                Response.Cookies.Append(_refreshTokenName, refreshToken, _cookieOptionsForRefreshTokenWithMaxAge);
            }
            else
            {
                Response.Cookies.Append(_tokenName, jwt, _cookieOptionsForTokenWithoutMaxAge);
                Response.Cookies.Append(_refreshTokenName, refreshToken, _cookieOptionsForRefreshTokenWithoutMaxAge);
            }

            
            return RedirectToAction("Index", "Home");
        }

        [HttpPost("api/auth/refresh")]
        [Consumes("application/json")]
        [Produces("application/json", "application/problem+json")]
        public async Task<IActionResult> Refresh([FromBody] RefreshRequest refreshRequest)
        {
            throw new NotImplementedException();
            //if (!ModelState.IsValid)
            //    return ValidationProblem(ModelState);

            //var claimsIdentity = await _tokenService.GetClaimsIdentityFromTokenAsync(refreshRequest.AccessToken, false);
            //var username = claimsIdentity.Name;

            //if (username is null)
            //    return Unauthorized();

            //var user = await _userManager.FindByUsernameAsync(username);

            //if (user is null)
            //{
            //    return Unauthorized();
            //}

            //if (!user.RefreshToken!.Equals(refreshRequest.RefreshToken) || user.RefreshTokenExpiryTime <= DateTime.UtcNow) 
            //{
            //    return Unauthorized();
            //}

            //var newAccessToken = _tokenService.CreateToken(user);
            //var newRefreshToken = _tokenService.CreateRefreshToken();

            //user.RefreshToken = newRefreshToken;
            //await _userManager.UpdateAsync(user);

            //// Update cookies for client
            //Response.Cookies.Append(_tokenName, newAccessToken, _cookieOptionsForToken);
            //Response.Cookies.Append(_tokenName, newRefreshToken, _cookieOptionsForRefreshToken);

            //return Ok();
        }

        [Authorize]
        [HttpPost("api/auth/revoke")]
        public async Task<IActionResult> Revoke()
        {
            var username = HttpContext.User.Identity!.Name;

            var user = await _userManager.FindByUsernameAsync(username!);

            if (user is null)
                return NotFound();

            user.RefreshToken = null;

            await _userManager.UpdateAsync(user);           

            return NoContent();
        }
    }
}
