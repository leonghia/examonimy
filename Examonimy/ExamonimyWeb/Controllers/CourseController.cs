using AutoMapper;
using ExamonimyWeb.DTOs.CourseDTO;
using ExamonimyWeb.Entities;
using ExamonimyWeb.Models;
using ExamonimyWeb.Repositories.GenericRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExamonimyWeb.Controllers
{
    [Route("")]
    public class CourseController : GenericController<Course>
    {
        public CourseController(IMapper mapper, IGenericRepository<Course> genericRepository) : base(mapper, genericRepository)
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
