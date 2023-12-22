using AutoMapper;
using ExamonimyWeb.DTOs.UserDTO;
using ExamonimyWeb.Entities;
using ExamonimyWeb.Managers.UserManager;
using ExamonimyWeb.Repositories.GenericRepository;
using ExamonimyWeb.Services.AuthService;
using ExamonimyWeb.Services.TokenService;
using ExamonimyWeb.Utilities;
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
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _jwtConfigurations;
        private readonly string _tokenName;
        private readonly string _refreshTokenName;
        private readonly int _accessTokenLifetimeInMinutes;
        private readonly int _refreshTokenLifetimeInDays;
        private readonly CookieOptions _cookieOptionForAccessTokenWithMaxAge;

        private readonly CookieOptions _cookieOptionsForAccessTokenWithoutMaxAge = new()
        {          
            Path = "/",
            HttpOnly = true
        };

        private readonly CookieOptions _cookieOptionsForRefreshTokenWithMaxAge;

        private readonly CookieOptions _cookieOptionsForRefreshTokenWithoutMaxAge = new()
        {          
            Path = "/api/auth/refresh",
            HttpOnly = true
        };


        public AuthController(IAuthService authService, IMapper mapper, IUserManager userManager, IConfiguration configuration, ITokenService tokenService, IGenericRepository<User> userRepository) : base(mapper, userRepository, userManager)
        {
            _authService = authService;
            _mapper = mapper;
            _userManager = userManager;
            _tokenService = tokenService;
            _jwtConfigurations = configuration.GetSection("JwtConfigurations");
            _tokenName = _jwtConfigurations["AccessTokenName"]!;
            _refreshTokenName = _jwtConfigurations["RefreshTokenName"]!;
            _accessTokenLifetimeInMinutes = int.Parse(_jwtConfigurations["AccessTokenLifetimeInMinutes"]!);
            _refreshTokenLifetimeInDays = int.Parse(_jwtConfigurations["RefreshTokenLifetimeInDays"]!);
            _cookieOptionForAccessTokenWithMaxAge = new()
            {
                MaxAge = new TimeSpan(0, _accessTokenLifetimeInMinutes, 0),
                Path = "/",
                HttpOnly = true
            };
            _cookieOptionsForRefreshTokenWithMaxAge = new()
            {
                MaxAge = new TimeSpan(_refreshTokenLifetimeInDays, 0, 0, 0),
                Path = "/api/auth/refresh",
                HttpOnly = true
            };
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
                user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(Convert.ToDouble(_jwtConfigurations["RefreshTokenLifetimeInDays"]));

                await _userManager.UpdateAsync(user);

                Response.Cookies.Append(_tokenName, jwt, _cookieOptionForAccessTokenWithMaxAge);
                Response.Cookies.Append(_refreshTokenName, refreshToken, _cookieOptionsForRefreshTokenWithMaxAge);
            }
            else
            {
                Response.Cookies.Append(_tokenName, jwt, _cookieOptionsForAccessTokenWithoutMaxAge);
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
