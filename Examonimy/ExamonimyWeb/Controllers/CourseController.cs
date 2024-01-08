using ExamonimyWeb.Attributes;
using ExamonimyWeb.DTOs.CourseDTO;
using ExamonimyWeb.Entities;
using ExamonimyWeb.Managers.ExamPaperManager;
using ExamonimyWeb.Managers.QuestionManager;
using ExamonimyWeb.Managers.UserManager;
using ExamonimyWeb.Repositories;
using ExamonimyWeb.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ExamonimyWeb.Controllers;

[Route("")]
public class CourseController : BaseController
{
    private readonly IGenericRepository<Course> _courseRepository;
    private readonly IExamPaperManager _examPaperManager;   
    private readonly IQuestionManager _questionManager;

    public CourseController(IGenericRepository<Course> courseRepository, IUserManager userManager, IExamPaperManager examPaperManager, IQuestionManager questionManager) : base(userManager)
    {
        _courseRepository = courseRepository;
        _examPaperManager = examPaperManager;      
        _questionManager = questionManager;
    }

    [CustomAuthorize(Roles = "Admin,Teacher")]
    [RequestHeaderMatchesMediaType("Accept", "application/vnd.examonimy.course.numbersofexampapers+json")]
    [Produces("application/vnd.examonimy.course.numbersofexampapers+json")]
    [HttpGet("api/course")]
    public async Task<ActionResult> GetCoursesWithNumbersOfExamPapers([FromQuery] RequestParams? requestParams)
    {
        var courses = await _courseRepository.GetPagedListAsync(requestParams, null, null, q => q.OrderBy(c => c.Name));
        var dict = await _examPaperManager.CountGroupByCourseIdAsync();
        var coursesToReturn = courses.Select(c => new CourseWithNumbersOfExamPapersGetDto
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
        
        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

        return Ok(coursesToReturn);
    }

    [CustomAuthorize(Roles = "Admin,Teacher")]
    [RequestHeaderMatchesMediaType("Accept", "application/vnd.examonimy.course.numbersofquestions+json")]
    [Produces("application/vnd.examonimy.course.numbersofquestions+json")]
    [HttpGet("api/course")]
    public async Task<IActionResult> GetCoursesWithNumbersOfQuestions([FromQuery] RequestParams? requestParams)
    {
        var courses = await _courseRepository.GetPagedListAsync(requestParams, null, null, q => q.OrderBy(c => c.Name));
        var dict = await _questionManager.CountByCourseAsync();
        var coursesToReturn = courses.Select(c => new CourseWithNumbersOfQuestionsGetDto
        {
            Id = c.Id,
            NumbersOfQuestions = dict.TryGetValue(c.Id, out var numbers) ? numbers : 0,
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

        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

        return Ok(coursesToReturn);
    }
}
