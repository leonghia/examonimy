using AutoMapper;
using ExamonimyWeb.Attributes;
using ExamonimyWeb.DTOs.ExamDTO;
using ExamonimyWeb.DTOs.UserDTO;
using ExamonimyWeb.Entities;
using ExamonimyWeb.Managers.ExamManager;
using ExamonimyWeb.Managers.UserManager;
using ExamonimyWeb.Models;
using ExamonimyWeb.Repositories.GenericRepository;
using ExamonimyWeb.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace ExamonimyWeb.Controllers;

[Route("")]
public class ExamController : GenericController<Exam>
{
    private readonly IMapper _mapper;
    private readonly IExamManager _examManager;
    private const int _timeAllowedInMinutes = 40;

    public ExamController(IMapper mapper, IGenericRepository<Exam> genericRepository, IUserManager userManager, IExamManager examManager) : base(mapper, genericRepository, userManager)
    {
        _mapper = mapper;
        _examManager = examManager;
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
        var contextUser = await base.GetContextUser();
        var exams = await _examManager.GetPagedListAsync(requestParams, e => e.MainClass!.TeacherId == contextUser.Id);
        var examsToReturn = exams.Select(e => new ExamGetDto
        {
            Id = e.Id,
            MainClassName = e.MainClass!.Name,
            ExamPaperCode = e.ExamPaper!.ExamPaperCode,
            CourseName = e.ExamPaper.Course!.Name,
            From = e.From,
            To = e.To,
            TimeAllowedInMinutes = _timeAllowedInMinutes
        });

        return Ok(examsToReturn);
    }
}
