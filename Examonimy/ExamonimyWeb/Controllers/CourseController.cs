using AutoMapper;
using ExamonimyWeb.Attributes;
using ExamonimyWeb.DTOs.CourseDTO;
using ExamonimyWeb.Entities;
using ExamonimyWeb.Managers.ExamPaperManager;
using ExamonimyWeb.Managers.UserManager;
using ExamonimyWeb.Repositories;
using ExamonimyWeb.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ExamonimyWeb.Controllers
{
    [Route("")]
    public class CourseController : BaseController
    {
        private readonly IGenericRepository<Course> _courseRepository;
        private readonly IExamPaperManager _examPaperManager;
        private readonly IConfiguration _configuration;

        public CourseController(IGenericRepository<Course> courseRepository, IUserManager userManager, IExamPaperManager examPaperManager, IConfiguration configuration) : base(userManager)
        {
            _courseRepository = courseRepository;
            _examPaperManager = examPaperManager;
            _configuration = configuration;
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

            var paginationMetadata = new PaginationMetadata
            {
                TotalCount = courses.TotalCount,
                PageSize = courses.PageSize,
                CurrentPage = courses.PageNumber,
                TotalPages = courses.TotalPages
            };
            var header = _configuration.GetSection("Header").GetSection("PaginationMetadata").Value ?? throw new NullReferenceException();
            Response.Headers.Add(header, JsonSerializer.Serialize(paginationMetadata));

            return Ok(coursesToReturn);
        }
    }
}
