using AutoMapper;
using ExamonimyWeb.DTOs.CourseDTO;
using ExamonimyWeb.Entities;
using ExamonimyWeb.Managers.UserManager;
using ExamonimyWeb.Repositories.GenericRepository;
using ExamonimyWeb.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExamonimyWeb.Controllers
{
    [Route("")]
    public class CourseController : GenericController<Course>
    {
        public CourseController(IMapper mapper, IGenericRepository<Course> genericRepository, IUserManager userManager) : base(mapper, genericRepository, userManager)
        {
        }

        [Authorize]
        [Produces("application/json")]
        [HttpGet("api/course")]
        public async Task<ActionResult> Get([FromQuery] RequestParams? requestParams)
        {
            return await base.Get<CourseGetDto>(requestParams, null, null);
        }
    }
}
