using AutoMapper;
using ExamonimyWeb.Attributes;
using ExamonimyWeb.DTOs.CourseDTO;
using ExamonimyWeb.DTOs.ExamPaperDTO;
using ExamonimyWeb.DTOs.UserDTO;
using ExamonimyWeb.Entities;
using ExamonimyWeb.Managers.UserManager;
using ExamonimyWeb.Models;
using ExamonimyWeb.Repositories.GenericRepository;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

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
        [HttpGet("api/exam-paper/{id}", Name = "GetExamPaperById")]
        [Produces("application/json")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            Expression<Func<ExamPaper, bool>> predicate = eP => eP.Id == id;
            var examPaperEntity = await _examPaperRepository.GetAsync(predicate, new List<string> { "Course", "Author" });
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
            var authorId = (await _userManager.FindByUsernameAsync(HttpContext.User.Identity!.Name!))!.Id;
            var examPaperEntity = _mapper.Map<ExamPaper>(examPaperCreateDto);
            examPaperEntity.AuthorId = authorId;
            await _examPaperRepository.InsertAsync(examPaperEntity);
            await _examPaperRepository.SaveAsync();           

            var examPaperToReturn = _mapper.Map<ExamPaperGetDto>(examPaperEntity);
            examPaperToReturn.NumbersOfQuestion = examPaperEntity.ExamPaperQuestions!.Count;
            Expression<Func<Course, bool>> coursePredicate = c => c.Id == examPaperEntity.CourseId;
            examPaperToReturn.Course = _mapper.Map<CourseGetDto>(await _courseRepository.GetAsync(coursePredicate, null));

            return CreatedAtRoute("GetExamPaperById", new { id = examPaperEntity.Id }, examPaperToReturn);
        }
    }
}
