using ExamonimyWeb.Attributes;
using ExamonimyWeb.DTOs.CourseModuleDto;
using ExamonimyWeb.Entities;
using ExamonimyWeb.Managers.UserManager;
using ExamonimyWeb.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ExamonimyWeb.Controllers;

[Route("")]
public class CourseModuleController : BaseController
{
    private readonly IGenericRepository<Course> _courseRepository;
    private readonly IGenericRepository<CourseModule> _courseModuleRepository;

    public CourseModuleController(IUserManager userManager, IGenericRepository<Course> courseRepository, IGenericRepository<CourseModule> courseModuleRepository) : base(userManager)
    {
        _courseRepository = courseRepository;
        _courseModuleRepository = courseModuleRepository;
    }

    

    [CustomAuthorize(Roles = "Admin,Teacher")]
    [HttpGet("api/course-module")]
    public async Task<IActionResult> GetByCourse([FromQuery] int courseId)
    {
        var courseModules = await _courseModuleRepository.GetRangeAsync(m => m.CourseId == courseId, new List<string> { "Course" });
        var courseModulesToReturn = courseModules.Select(m => new CourseModuleGetDto
        {
            Id = m.Id,
            Name = m.Name,
            Course = m.Course!.Name
        });
        return Ok(courseModulesToReturn);
    }
}
