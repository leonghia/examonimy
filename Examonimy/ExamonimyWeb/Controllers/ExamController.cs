using AutoMapper;
using ExamonimyWeb.Attributes;
using ExamonimyWeb.DTOs.ClassDTO;
using ExamonimyWeb.DTOs.CourseDTO;
using ExamonimyWeb.DTOs.ExamDTO;
using ExamonimyWeb.DTOs.UserDTO;
using ExamonimyWeb.Entities;
using ExamonimyWeb.Managers.ExamManager;
using ExamonimyWeb.Managers.ExamPaperManager;
using ExamonimyWeb.Managers.UserManager;
using ExamonimyWeb.Models;
using ExamonimyWeb.Repositories;
using ExamonimyWeb.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace ExamonimyWeb.Controllers;

[Route("")]
public class ExamController : GenericController<Exam>
{
    private readonly IMapper _mapper;
    private readonly IExamManager _examManager;
    private readonly IExamPaperManager _examPaperManager;
    private readonly IGenericRepository<Course> _courseRepository;
    private readonly IGenericRepository<MainClass> _mainClassRepository;
    private const int _timeAllowedInMinutes = 40;

    public ExamController(IMapper mapper, IGenericRepository<Exam> genericRepository, IUserManager userManager, IExamManager examManager, IExamPaperManager examPaperManager, IGenericRepository<Course> courseRepository, IGenericRepository<MainClass> mainClassRepository) : base(mapper, genericRepository, userManager)
    {
        _mapper = mapper;
        _examManager = examManager;
        _examPaperManager = examPaperManager;
        _courseRepository = courseRepository;
        _mainClassRepository = mainClassRepository;
    }

    [CustomAuthorize(Roles = "Teacher")]
    [HttpGet("exam")]
    public async Task<IActionResult> RenderIndexView()
    {
        var contextUser = await base.GetContextUser();
        return View("Index", new AuthorizedViewModel { User = _mapper.Map<UserGetDto>(contextUser) });
    }

    [CustomAuthorize(Roles = "Teacher")]
    [HttpGet("api/exam")]
    public async Task<IActionResult> Get([FromQuery] RequestParams? requestParams)
    {
        //var contextUser = await base.GetContextUser();
        //var exams = await _examManager.GetPagedListAsync(requestParams, e => e.MainClass!.TeacherId == contextUser.Id);
        //var examsToReturn = exams.Select(e => new ExamGetDto
        //{
        //    Id = e.Id,
        //    MainClassName = e.MainClass!.Name,
        //    ExamPaperCode = e.ExamPaper!.ExamPaperCode,
        //    CourseName = e.ExamPaper.Course!.Name,
        //    From = e.From,
        //    To = e.To,
        //    TimeAllowedInMinutes = _timeAllowedInMinutes
        //});

        //return Ok(examsToReturn);
        throw new NotImplementedException();
    }

    [CustomAuthorize(Roles = "Teacher")]
    [HttpGet("exam/create")]
    public async Task<IActionResult> RenderCreateView()
    {
        var contextUser = await base.GetContextUser();
        var dict = await _examPaperManager.CountGroupByCourseIdAsync();
        var courses = await _courseRepository.GetRangeAsync(null, null, q => q.OrderBy(c => c.Name));
        var coursesToReturn = courses.Select(c => new CourseGetDto
        {
            Id = c.Id,
            NumbersOfExamPapers = dict.TryGetValue(c.Id, out var numbers) ? numbers : 0,
            Name = c.Name,
            CourseCode = c.CourseCode
        }).ToList();
        var classes = await _mainClassRepository.GetRangeAsync(c => c.TeacherId == contextUser.Id);
        var classesToReturn = classes.Select(c => new MainClassGetDto
        {
            Id = c.Id,
            Name = c.Name
        }).ToList();
        var viewModel = new ExamCreateViewModel
        {
            User = _mapper.Map<UserGetDto>(contextUser),
            Courses = coursesToReturn,
            MainClasses = classesToReturn
        };
        return View("Create", viewModel);
    }
}
