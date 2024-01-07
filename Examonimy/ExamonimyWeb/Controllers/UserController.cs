using ExamonimyWeb.Attributes;
using ExamonimyWeb.DTOs.UserDTO;
using ExamonimyWeb.Entities;
using ExamonimyWeb.Managers.UserManager;
using ExamonimyWeb.Repositories;
using ExamonimyWeb.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace ExamonimyWeb.Controllers
{
    [Route("")]
    public class UserController : BaseController
    {
        
        private readonly IGenericRepository<User> _userRepository;

        public UserController(IGenericRepository<User> userRepository, IUserManager userManager) : base(userManager)
        {
            
            _userRepository = userRepository;
        }

        [CustomAuthorize(Roles = "Administrator,Teacher")]
        [HttpGet("api/user")]
        [Produces("application/json")]
        public async Task<IActionResult> Get([FromQuery] RequestParamsForUser? requestParamsForUser)
        {
            Expression<Func<User, bool>>? predicate = null;
            if (requestParamsForUser?.RoleId is not null && requestParamsForUser.RoleId > 0)
                predicate = u => u.RoleId == requestParamsForUser.RoleId;
            var users = (await _userRepository.GetRangeAsync(predicate, null, q => q.OrderBy(u => u.FullName))).ToList();
            var contextUser = await base.GetContextUser();
            var index = users.FindIndex(u => u.Id == contextUser.Id);
            users.RemoveAt(index);
            var usersToReturn = users.Select(u => new UserGetDto { FullName = u.FullName, Id = u.Id, ProfilePicture = u.ProfilePicture });
            return Ok(usersToReturn);
        }
    }
}
