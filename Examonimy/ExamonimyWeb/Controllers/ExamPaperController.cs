using AutoMapper;
using ExamonimyWeb.Attributes;
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

        public ExamPaperController(IMapper mapper, IGenericRepository<ExamPaper> examPaperRepository, IUserManager userManager, IGenericRepository<ExamPaperQuestion> examPaperQuestionRepository) : base(mapper, examPaperRepository)
        {
            _mapper = mapper;
            _examPaperRepository = examPaperRepository;
            _userManager = userManager;
            _examPaperQuestionRepository = examPaperQuestionRepository;
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
    }
}
