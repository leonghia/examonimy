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

        public ExamPaperController(IMapper mapper, IGenericRepository<ExamPaper> examPaperRepository, IUserManager userManager, IGenericRepository<ExamPaperQuestion> examPaperQuestionRepository, IGenericRepository<Course> courseRepository) : base(mapper, examPaperRepository)
        {
            _mapper = mapper;
            _examPaperRepository = examPaperRepository;
            _userManager = userManager;
            _examPaperQuestionRepository = examPaperQuestionRepository;
            _courseRepository = courseRepository;
        }

        [CustomAuthorize(Roles = "Administrator")]
        [HttpGet("exam-paper")]
        public async Task<IActionResult> Bank()
        {
            var examPapers = await _examPaperRepository.GetAsync(null, null, null);
            var examPaperGetDtos = new List<ExamPaperGetDto>();
            foreach (var examPaper in examPapers)
            {
                var examPaperGetDto = _mapper.Map<ExamPaperGetDto>(examPaper);
                Expression<Func<ExamPaperQuestion, bool>> predicate = ePQ => ePQ.ExamPaperId == examPaper.Id;
                examPaperGetDto.NumbersOfQuestions = await _examPaperQuestionRepository.CountAsync(predicate);
                examPaperGetDtos.Add(examPaperGetDto);

            }
            var user = await _userManager.FindByUsernameAsync(HttpContext.User.Identity!.Name!);
            var userGetDto = _mapper.Map<UserGetDto>(user);
            var viewModel = new ExamPaperBankViewModel { User = userGetDto, ExamPapers = examPaperGetDtos };
            return View(viewModel);
        }

        [CustomAuthorize(Roles = "Administrator")]
        [HttpGet("exam-paper/create")]
        public async Task<IActionResult> Create()
        {           
            var userToReturn = _mapper.Map<UserGetDto>(await _userManager.FindByUsernameAsync(HttpContext.User.Identity!.Name!));
            var viewModel = new AuthorizedViewModel { User = userToReturn };
            return View(viewModel);
        }
    }
}
