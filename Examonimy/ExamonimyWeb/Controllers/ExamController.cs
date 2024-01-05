﻿using AutoMapper;
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
using ExamonimyWeb.Services.NotificationService;
using ExamonimyWeb.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace ExamonimyWeb.Controllers;

[Route("")]
public class ExamController : BaseController
{
    private readonly IMapper _mapper;
    private readonly IUserManager _userManager;
    private readonly IExamManager _examManager;
    private readonly IExamPaperManager _examPaperManager;
    private readonly IGenericRepository<Course> _courseRepository;
    private readonly IGenericRepository<MainClass> _mainClassRepository;
    private readonly INotificationService _notificationService;
    private const int _timeAllowedInMinutes = 40;

    public ExamController(IMapper mapper, IUserManager userManager, IExamManager examManager, IExamPaperManager examPaperManager, IGenericRepository<Course> courseRepository, IGenericRepository<MainClass> mainClassRepository, INotificationService notificationService) : base(userManager)
    {
        _mapper = mapper;
        _userManager = userManager;
        _examManager = examManager;
        _examPaperManager = examPaperManager;
        _courseRepository = courseRepository;
        _mainClassRepository = mainClassRepository;
        _notificationService = notificationService;
    }

    [CustomAuthorize(Roles = "Admin,Student")]
    [HttpGet("exam")]
    public async Task<IActionResult> RenderIndexView()
    {
        var contextUser = await base.GetContextUser();
        var role = _userManager.GetRole(contextUser);
        return View(role, new AuthorizedViewModel { User = _mapper.Map<UserGetDto>(contextUser) });
    }

    [CustomAuthorize(Roles = "Admin")]
    [HttpGet("api/exam")]
    public async Task<IActionResult> Get([FromQuery] RequestParams? requestParams)
    {
        var contextUser = await base.GetContextUser();
        var exams = await _examManager.GetPagedListAsync(requestParams);
        var examsToReturn = exams.Select(e => new ExamGetDto
        {
            From = e.From,
            To = e.To,
            TimeAllowedInMinutes = _timeAllowedInMinutes,
            MainClasses = e.MainClasses!.Select(mc => mc.Name).ToList(),
            Id = e.Id,
            ExamPaperCode = e.ExamPaper!.ExamPaperCode,
            CourseName = e.ExamPaper!.Course!.Name
        });
        return Ok(examsToReturn);
    }

    [CustomAuthorize(Roles = "Admin")]
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
        var classes = await _mainClassRepository.GetRangeAsync();
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

    [CustomAuthorize(Roles = "Admin")]
    [HttpPost("api/exam")]
    [Consumes("application/json")]
    public async Task<IActionResult> Create([FromBody] ExamCreateDto examCreateDto)
    {      
        if (!ModelState.IsValid) return base.ValidationProblem(ModelState);
        var examPaper = await _examPaperManager.GetAsync(ep => ep.Id == examCreateDto.ExamPaperId, new List<string> { "Course" });
        if (examPaper is null) return NotFound();
        foreach (var id in examCreateDto.MainClassIds)
        {
            var mainClass = await _mainClassRepository.GetByIdAsync(id);
            if (mainClass is null) return NotFound();
        }
        var examToCreate = new Exam
        {
            ExamPaperId = examCreateDto.ExamPaperId,
            From = examCreateDto.From,
            To = examCreateDto.To
        };
        await _examManager.CreateExamAsync(examToCreate, examCreateDto.MainClassIds.ToList());

        var teacherId = (await base.GetContextUser()).Id;
        await _notificationService.NotifyAboutUpcomingExamAsync(teacherId, examToCreate.Id, examCreateDto.MainClassIds.ToList());
        var mainClasses = (await _mainClassRepository.GetRangeAsync(c => examCreateDto.MainClassIds.Contains(c.Id))).Select(c => c.Name).ToList();       
        var examToReturn = new ExamGetDto
        {
            Id = examToCreate.Id,
            MainClasses = mainClasses,
            ExamPaperCode = examPaper.ExamPaperCode,
            CourseName = examPaper.Course!.Name,
            From = examToCreate.From,
            To = examToCreate.To,
            TimeAllowedInMinutes = _timeAllowedInMinutes
        };
        return Created("", examToReturn);
    }
}
