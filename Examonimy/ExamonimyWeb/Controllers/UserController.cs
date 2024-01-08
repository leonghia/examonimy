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

        
    }
}
