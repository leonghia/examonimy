using AutoMapper;
using ExamonimyWeb.Attributes;
using ExamonimyWeb.DTOs.CourseDTO;
using ExamonimyWeb.Entities;
using ExamonimyWeb.Managers.ExamPaperManager;
using ExamonimyWeb.Managers.UserManager;
using ExamonimyWeb.Repositories;
using ExamonimyWeb.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace ExamonimyWeb.Controllers
{
    [Route("")]
    public class CourseController : GenericController<Course>
    {
        private readonly IGenericRepository<Course> _courseRepository;
        private readonly IExamPaperManager _examPaperManager;

        public CourseController(IMapper mapper, IGenericRepository<Course> courseRepository, IUserManager userManager, IExamPaperManager examPaperManager) : base(mapper, courseRepository, userManager)
        {
            _courseRepository = courseRepository;
            _examPaperManager = examPaperManager;
        }

        [CustomAuthorize]
        [Produces("application/json")]
        [HttpGet("api/course")]
        public async Task<ActionResult> Get([FromQuery] RequestParams? requestParams)
        {
            var courses = await _courseRepository.GetPagedListAsync(requestParams, null, null, q => q.OrderBy(c => c.Name));
            var dict = await _examPaperManager.CountGroupByCourseIdAsync();
            var coursesToReturn = courses.Select(c => new CourseGetDto
            {
                Id = c.Id,
                NumbersOfExamPapers = dict.TryGetValue(c.Id, out var numbers) ? numbers : 0,
                Name = c.Name,
                CourseCode = c.CourseCode
            });
            return Ok(coursesToReturn);
        }
    }
}
