using AutoMapper;
using ExamonimyWeb.Attributes;
using ExamonimyWeb.DTOs.CourseDTO;
using ExamonimyWeb.DTOs.ExamPaperDTO;
using ExamonimyWeb.DTOs.UserDTO;
using ExamonimyWeb.Entities;
using ExamonimyWeb.Managers.UserManager;
using ExamonimyWeb.Models;
using ExamonimyWeb.Repositories.GenericRepository;
using ExamonimyWeb.Utilities;
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

        public ExamPaperController(IMapper mapper, IGenericRepository<ExamPaper> examPaperRepository, IUserManager userManager, IGenericRepository<ExamPaperQuestion> examPaperQuestionRepository, IGenericRepository<Course> courseRepository) : base(mapper, examPaperRepository, userManager)
        {
            _mapper = mapper;
            _examPaperRepository = examPaperRepository;
            _userManager = userManager;
            _examPaperQuestionRepository = examPaperQuestionRepository;
            _courseRepository = courseRepository;
        }

        [CustomAuthorize(Roles = "Administrator,Teacher")]
        [HttpGet("exam-paper")]
        public async Task<IActionResult> RenderIndexView()
        {
            var examPapers = await _examPaperRepository.GetAsync(null, null, null, null);
            var examPaperGetDtos = new List<ExamPaperGetDto>();
            foreach (var examPaper in examPapers)
            {
                var examPaperGetDto = _mapper.Map<ExamPaperGetDto>(examPaper);
                Expression<Func<ExamPaperQuestion, bool>> predicate = ePQ => ePQ.ExamPaperId == examPaper.Id;
                examPaperGetDto.NumbersOfQuestion = await _examPaperQuestionRepository.CountAsync(predicate);
                examPaperGetDtos.Add(examPaperGetDto);

            }
            var user = await _userManager.FindByUsernameAsync(HttpContext.User.Identity!.Name!);
            var userGetDto = _mapper.Map<UserGetDto>(user);
            var viewModel = new ExamPaperBankViewModel { User = userGetDto, ExamPapers = examPaperGetDtos };
            return View("Index", viewModel);
        }

        [CustomAuthorize(Roles = "Administrator,Teacher")]
        [HttpGet("api/exam-paper")]
        [Produces("application/json")]
        public async Task<IActionResult> Get([FromQuery] RequestParamsForExamPaper requestParamsForExamPaper)
        {
            Expression<Func<ExamPaper, bool>>? searchPredicate = null;
            if (requestParamsForExamPaper.SearchQuery is not null)
            {
                searchPredicate = eP => eP.ExamPaperCode.ToUpperInvariant().Contains(requestParamsForExamPaper.SearchQuery.ToUpperInvariant());
            }
            Expression<Func<ExamPaper, bool>>? filterPredicate = null;
            if (requestParamsForExamPaper.CourseId is not null && requestParamsForExamPaper.CourseId > 0)
            {
                filterPredicate = eP => eP.CourseId == requestParamsForExamPaper.CourseId;
            }
            var examPapers = await _examPaperRepository.GetAsync(requestParamsForExamPaper, searchPredicate, filterPredicate, new List<string> { "Author", "Course" });
            var examPapersToReturn = examPapers.Select(eP => _mapper.Map<ExamPaperGetDto>(eP)).ToArray();

            for (var i = 0; i < examPapersToReturn.Length; i++)
            {
                examPapersToReturn[i].NumbersOfQuestion = await _examPaperQuestionRepository.CountAsync(examPaperQuestion => examPaperQuestion.ExamPaperId == examPapersToReturn[i].Id);
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
            examPaper.AuthorId = contextUser.Id;
            await _examPaperRepository.InsertAsync(examPaper);
            await _examPaperRepository.SaveAsync();
            var examPaperToReturn = _mapper.Map<ExamPaperGetDto>(examPaper);
            examPaperToReturn.NumbersOfQuestion = await _examPaperQuestionRepository.CountAsync(ePQ => ePQ.ExamPaperId == examPaper.Id);
            return CreatedAtRoute("GetExamPaperById", new { id = examPaper.Id }, examPaperToReturn);
        }
    }
}
