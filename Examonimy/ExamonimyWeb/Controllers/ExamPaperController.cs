using AutoMapper;
using ExamonimyWeb.Attributes;
using ExamonimyWeb.DTOs.CourseDTO;
using ExamonimyWeb.DTOs.ExamPaperDTO;
using ExamonimyWeb.DTOs.UserDTO;
using ExamonimyWeb.Entities;
using ExamonimyWeb.Enums;
using ExamonimyWeb.Extensions;
using ExamonimyWeb.Managers.ExamPaperManager;
using ExamonimyWeb.Managers.UserManager;
using ExamonimyWeb.Models;
using ExamonimyWeb.Repositories.GenericRepository;
using ExamonimyWeb.Services.NotificationService;
using ExamonimyWeb.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ExamonimyWeb.Controllers
{
    [Route("")]
    public class ExamPaperController : GenericController<ExamPaper>
    {
        private readonly IMapper _mapper;        
        private readonly IUserManager _userManager;     
        private readonly IGenericRepository<Course> _courseRepository;
        private readonly IExamPaperManager _examPaperManager;
        private readonly INotificationService _notificationService;

        public ExamPaperController(IMapper mapper, IGenericRepository<ExamPaper> examPaperRepository, IUserManager userManager, IGenericRepository<Course> courseRepository, IExamPaperManager examPaperManager, INotificationService notificationService) : base(mapper, examPaperRepository, userManager)
        {
            _mapper = mapper;          
            _userManager = userManager;          
            _courseRepository = courseRepository;
            _examPaperManager = examPaperManager;
            _notificationService = notificationService;
        }

        [CustomAuthorize(Roles = "Administrator,Teacher")]
        [HttpGet("exam-paper")]
        public async Task<IActionResult> RenderIndexView()
        {

            var contextUser = await base.GetContextUser();
            var userToReturn = _mapper.Map<UserGetDto>(contextUser);
            var coursesCount = await _courseRepository.CountAsync(null);
            var coursesToReturn = (await _courseRepository.GetPagedListAsync(new RequestParams { PageSize = coursesCount }, null, null, null)).Select(c => _mapper.Map<CourseGetDto>(c));
            var statusesToReturn = new List<ExamPaperStatusModel>();
            foreach (ExamPaperStatus e in Enum.GetValues(typeof(ExamPaperStatus)))
            {
                statusesToReturn.Add(new ExamPaperStatusModel { ExamPaperStatus = (int)e, ExamPaperStatusAsString = e.ToVietnameseString() });
            }
            var viewModel = new ExamPaperBankViewModel { User = userToReturn, Courses = coursesToReturn, Statuses = statusesToReturn };
            return View("Index", viewModel);
        }

        [CustomAuthorize(Roles = "Administrator,Teacher")]
        [HttpGet("exam-paper/{id}")]
        public async Task<IActionResult> RenderSingleView([FromRoute] int id)
        {
            var contextUser = await base.GetContextUser();
            var userToReturn = _mapper.Map<UserGetDto>(contextUser);
            var examPaper = await _examPaperManager.GetByIdAsync(id);
            if (examPaper is null)
                return NotFound();
            var examPaperToReturn = _mapper.Map<ExamPaperGetDto>(examPaper);

            var examPaperSingleViewModel = new ExamPaperSingleViewModel
            {
                User = userToReturn,
                ExamPaper = examPaperToReturn
            };
            return View("Single", examPaperSingleViewModel);
        }

        [CustomAuthorize(Roles = "Administrator,Teacher")]
        [HttpGet("api/exam-paper/{examPaperId:int}/question")]
        [Produces("application/json")]
        public async Task<IActionResult> GetExamPaperQuestions([FromRoute] int examPaperId)
        {
            var examPaper = await _examPaperManager.GetByIdAsync(examPaperId);
            if (examPaper is null)
                return NotFound();
            var examPaperQuestionsToReturn = await _examPaperManager.GetExamPaperQuestionsAsync(examPaperId);
            return Ok(examPaperQuestionsToReturn);
        }

        [CustomAuthorize(Roles = "Administrator,Teacher")]
        [HttpGet("api/exam-paper")]
        [Produces("application/json")]
        public async Task<IActionResult> Get([FromQuery] RequestParamsForExamPaper requestParamsForExamPaper)
        {
            var examPapers = await _examPaperManager.GetPagedListAsync(requestParamsForExamPaper);
            var examPapersToReturn = examPapers.Select(eP => _mapper.Map<ExamPaperGetDto>(eP)).ToArray();

            for (var i = 0; i < examPapersToReturn.Length; i++)
            {
                examPapersToReturn[i].NumbersOfQuestion = await _examPaperManager.CountNumbersOfQuestions(examPapersToReturn[i].Id);
                examPapersToReturn[i].StatusAsString = examPapersToReturn[i].Status.ToVietnameseString();
            }    

            var paginationMetadata = new PaginationMetadata
            {
                TotalCount = examPapers.TotalCount,
                PageSize = examPapers.PageSize,
                CurrentPage = examPapers.PageNumber,
                TotalPages = examPapers.TotalPages
            };

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

            return Ok(examPapersToReturn);
        }

        [CustomAuthorize(Roles = "Administrator,Teacher")]
        [HttpGet("api/exam-paper/{id}", Name = "GetExamPaperById")]
        [Produces("application/json")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {           
            var examPaper = await _examPaperManager.GetByIdAsync(id);
            if (examPaper is null)
                return NotFound();
            var examPaperToReturn = _mapper.Map<ExamPaperGetDto>(examPaper);           
            examPaperToReturn.NumbersOfQuestion = await _examPaperManager.CountNumbersOfQuestions(examPaper.Id);
            return Ok(examPaperToReturn);
        }

        [CustomAuthorize(Roles = "Administrator,Teacher")]
        [HttpGet("exam-paper/create")]
        public async Task<IActionResult> RenderCreateView()
        {           
            var userToReturn = _mapper.Map<UserGetDto>(await _userManager.FindByUsernameAsync(HttpContext.User.Identity!.Name!));
            var viewModel = new AuthorizedViewModel { User = userToReturn };
            return View("Create", viewModel);
        }

        [CustomAuthorize(Roles = "Administrator,Teacher")]
        [HttpPost("api/exam-paper")]
        [Consumes("application/json")]
        [Produces("application/json", "application/problem+json")]
        public async Task<IActionResult> Create([FromBody] ExamPaperCreateDto examPaperCreateDto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var contextUser = await base.GetContextUser();
            var examPaper = _mapper.Map<ExamPaper>(examPaperCreateDto);
            examPaper.Status = (byte)ExamPaperStatus.Pending;
            examPaper.AuthorId = contextUser.Id;
            await _examPaperManager.AddThenSaveAsync(examPaper);
            var examPaperToReturn = _mapper.Map<ExamPaperGetDto>(examPaper);
            examPaperToReturn.NumbersOfQuestion = await _examPaperManager.CountNumbersOfQuestions(examPaper.Id);
            return CreatedAtRoute("GetExamPaperById", new { id = examPaper.Id }, examPaperToReturn);
        }

        [CustomAuthorize(Roles = "Administrator,Teacher")]
        [HttpGet("exam-paper/edit/{id:int}")]
        public async Task<IActionResult> RenderEditView([FromRoute] int id)
        {
            var examPaper = await _examPaperManager.GetByIdAsync(id);
            if (examPaper is null)
                return NotFound();
            var contextUser = await base.GetContextUser();
            if (!await _examPaperManager.IsAuthorAsync(id, contextUser.Id))
                return Forbid();
            return View("Edit", new ExamPaperEditViewModel { ExamPaperId = examPaper.Id, CourseId = examPaper.CourseId, User = _mapper.Map<UserGetDto>(contextUser) });
        }

        [CustomAuthorize(Roles = "Administrator,Teacher")]
        [HttpDelete("api/exam-paper/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var examPaper = await _examPaperManager.GetByIdAsync(id);
            if (examPaper is null)
                return NotFound();
            var contextUser = await base.GetContextUser();
            if (examPaper.AuthorId != contextUser.Id)
                return Forbid();
            await _examPaperManager.DeleteThenSaveAsync(id);
            
            return NoContent();
        }

        [CustomAuthorize(Roles = "Administrator,Teacher")]
        [HttpPut("api/exam-paper/{id:int}")]
        [Consumes("application/json")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] ExamPaperUpdateDto examPaperUpdateDto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var examPaper = await _examPaperManager.GetByIdAsync(id);
            if (examPaper is null)
                return NotFound();
            var contextUser = await base.GetContextUser();
            if (examPaper.AuthorId != contextUser.Id)
                return Forbid();
            var examPaperQuestionsToUpdate = examPaperUpdateDto.ExamPaperQuestions
                .Select(e => _mapper.Map<ExamPaperQuestion>(e))
                .Select(e =>
                {
                    e.ExamPaperId = examPaper.Id;
                    return e;
                })
                .ToList();
            await _examPaperManager.UpdateThenSaveAsync(examPaper.Id, examPaperQuestionsToUpdate);         
            return NoContent();
        }

        [CustomAuthorize(Roles = "Administrator,Teacher")]
        [HttpPost("api/exam-paper/{id:int}/reviewer")]
        [Consumes("application/json")]
        public async Task<IActionResult> AddReviewers([FromRoute] int id, [FromBody] ExamPaperReviewerCreateDto examPaperReviewerCreateDto)
        {
            var examPaper = await _examPaperManager.GetByIdAsync(id);
            if (examPaper is null)
                return NotFound();
            var contextUser = await base.GetContextUser();
            if (examPaper.AuthorId != contextUser.Id)
                return Forbid();
            var examPaperReviewers = examPaperReviewerCreateDto.ReviewerIds.Select(id => new ExamPaperReviewer { ExamPaperId = examPaper.Id, ReviewerId = id }).ToList();
            await _examPaperManager.AddReviewersThenSaveAsync(examPaper.Id, examPaperReviewers);
            await _notificationService.RequestReviewerForExamPaperAsync(examPaper.Id, examPaperReviewers, contextUser.Id);

            return Accepted();
        }
    }
}