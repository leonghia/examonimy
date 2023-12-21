using AutoMapper;
using ExamonimyWeb.Attributes;
using ExamonimyWeb.DTOs.CourseDTO;
using ExamonimyWeb.DTOs.ExamPaperDTO;
using ExamonimyWeb.DTOs.UserDTO;
using ExamonimyWeb.Entities;
using ExamonimyWeb.Extensions;
using ExamonimyWeb.Managers.ExamPaperManager;
using ExamonimyWeb.Managers.UserManager;
using ExamonimyWeb.Models;
using ExamonimyWeb.Repositories.GenericRepository;
using ExamonimyWeb.Utilities;
using LinqKit;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Immutable;
using System.Linq.Expressions;
using System.Text.Json;

namespace ExamonimyWeb.Controllers
{
    [Route("")]
    public class ExamPaperController : GenericController<ExamPaper>
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<ExamPaper> _examPaperRepository;
        private readonly IUserManager _userManager;
        private readonly IGenericRepository<ExamPaperQuestion> _examPaperQuestionRepository;
        private readonly IGenericRepository<Course> _courseRepository;
        private readonly IExamPaperManager _examPaperManager;

        public ExamPaperController(IMapper mapper, IGenericRepository<ExamPaper> examPaperRepository, IUserManager userManager, IGenericRepository<ExamPaperQuestion> examPaperQuestionRepository, IGenericRepository<Course> courseRepository, IExamPaperManager examPaperManager) : base(mapper, examPaperRepository, userManager)
        {
            _mapper = mapper;
            _examPaperRepository = examPaperRepository;
            _userManager = userManager;
            _examPaperQuestionRepository = examPaperQuestionRepository;
            _courseRepository = courseRepository;
            _examPaperManager = examPaperManager;
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
            var examPaperToReturn = _mapper.Map<ExamPaperGetDto>(await _examPaperRepository.GetAsync(eP => eP.Id == id, new List<string> { "Author", "Course" }));

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
            var examPaper = await _examPaperRepository.GetByIdAsync(examPaperId);
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
            Expression<Func<ExamPaper, bool>>? searchPredicate = null;
            if (requestParamsForExamPaper.SearchQuery is not null)
            {
                searchPredicate = eP => eP.ExamPaperCode.ToUpper().Contains(requestParamsForExamPaper.SearchQuery.ToUpper());
            }
            var filterPredicate = PredicateBuilder.New<ExamPaper>(true);
            if (requestParamsForExamPaper.CourseId is not null && requestParamsForExamPaper.CourseId > 0)
            {
                filterPredicate = filterPredicate.And(eP => eP.CourseId == requestParamsForExamPaper.CourseId);
            }
            if (requestParamsForExamPaper.Status is not null)
            {
                filterPredicate = filterPredicate.And(eP => eP.Status == requestParamsForExamPaper.Status);
            }
            var examPapers = await _examPaperRepository.GetPagedListAsync(requestParamsForExamPaper, searchPredicate, filterPredicate, new List<string> { "Author", "Course" });
            var examPapersToReturn = examPapers.Select(eP => _mapper.Map<ExamPaperGetDto>(eP)).ToArray();

            for (var i = 0; i < examPapersToReturn.Length; i++)
            {
                examPapersToReturn[i].NumbersOfQuestion = await _examPaperQuestionRepository.CountAsync(examPaperQuestion => examPaperQuestion.ExamPaperId == examPapersToReturn[i].Id);
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
            var examPaperEntity = await _examPaperRepository.GetAsync(eP => eP.Id == id, new List<string> { "Course", "Author" });
            if (examPaperEntity is null)
                return NotFound();
            var examPaperToReturn = _mapper.Map<ExamPaperGetDto>(examPaperEntity);
            Expression<Func<ExamPaperQuestion, bool>> examPaperQuestionPredicate = ePQ => ePQ.ExamPaperId == examPaperEntity.Id;
            examPaperToReturn.NumbersOfQuestion = await _examPaperQuestionRepository.CountAsync(examPaperQuestionPredicate);
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
            var contextUser = await base.GetContextUser();
            var examPaper = _mapper.Map<ExamPaper>(examPaperCreateDto);
            examPaper.Status = (byte)ExamPaperStatus.Pending;
            examPaper.AuthorId = contextUser.Id;
            await _examPaperRepository.InsertAsync(examPaper);
            await _examPaperRepository.SaveAsync();
            var examPaperToReturn = _mapper.Map<ExamPaperGetDto>(examPaper);
            examPaperToReturn.NumbersOfQuestion = await _examPaperQuestionRepository.CountAsync(ePQ => ePQ.ExamPaperId == examPaper.Id);
            return CreatedAtRoute("GetExamPaperById", new { id = examPaper.Id }, examPaperToReturn);
        }

        [CustomAuthorize(Roles = "Administrator,Teacher")]
        [HttpGet("exam-paper/edit/{id:int}")]
        public async Task<IActionResult> RenderEditView([FromRoute] int id)
        {
            var examPaper = await _examPaperRepository.GetByIdAsync(id);
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
            var examPaper = await _examPaperRepository.GetByIdAsync(id);
            if (examPaper is null)
                return NotFound();
            var contextUser = await base.GetContextUser();
            if (examPaper.AuthorId != contextUser.Id)
                return Forbid();
            _examPaperQuestionRepository.DeleteRange(ePQ => ePQ.ExamPaperId == examPaper.Id);
            await _examPaperQuestionRepository.SaveAsync();
            _examPaperRepository.Delete(examPaper);
            await _examPaperRepository.SaveAsync();
            return NoContent();
        }
    }
}