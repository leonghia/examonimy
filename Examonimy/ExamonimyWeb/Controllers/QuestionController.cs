using AutoMapper;
using ExamonimyWeb.Attributes;
using ExamonimyWeb.DTOs.QuestionDTO;
using ExamonimyWeb.DTOs.UserDTO;
using ExamonimyWeb.Entities;
using ExamonimyWeb.Managers.UserManager;
using ExamonimyWeb.Repositories.GenericRepository;
using Microsoft.AspNetCore.Mvc;

namespace ExamonimyWeb.Controllers
{
    [Route("")]
    public class QuestionController : GenericController<Question>
    {
        private readonly IUserManager _userManager;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<QuestionType> _questionTypeRepository;
        private readonly IGenericRepository<QuestionLevel> _questionLevelRepository;

        public QuestionController(IUserManager userManager, IMapper mapper, IGenericRepository<Question> questionRepository, IGenericRepository<QuestionType> questionTypeRepository, IGenericRepository<QuestionLevel> questionLevelRepository) : base(mapper, questionRepository)
        {
            _userManager = userManager;
            _mapper = mapper;
            _questionTypeRepository = questionTypeRepository;
            _questionLevelRepository = questionLevelRepository;
        }

        [CustomAuthorize(Roles = "Administrator")]
        [HttpGet("question")]
        public async Task<IActionResult> Index()
        {
            var username = HttpContext.User.Identity!.Name;
            var user = await _userManager.FindByUsernameAsync(username!);         
            var userGetDto = _mapper.Map<UserGetDto>(user);
            return View("Bank", userGetDto);
        }

        [CustomAuthorize(Roles = "Administrator")]
        [HttpGet("question/add")]
        public async Task<IActionResult> Create()
        {
            var username = HttpContext.User.Identity!.Name;
            var user = await _userManager.FindByUsernameAsync(username!);        
            var userGetDto = _mapper.Map<UserGetDto>(user);
            return View(userGetDto);
        }

        [CustomAuthorize(Roles = "Administrator")]
        [HttpGet("api/question/type")]
        [Produces("application/json")]
        public async Task<IActionResult> GetQuestionTypes()
        {
            var questionTypes = await _questionTypeRepository.GetAsync(null, null, null);
            var questionTypesToReturn = questionTypes.Select(questionType => _mapper.Map<QuestionTypeGetDto>(questionType));
            return Ok(questionTypesToReturn);
        }

        [CustomAuthorize(Roles = "Administrator")]
        [HttpGet("api/question/level")]
        [Produces("application/json")]
        public async Task<IActionResult> GetQuestionLevels()
        {
            var questionLevels = await _questionLevelRepository.GetAsync(null, null, null);
            var questionLevelsToReturn = questionLevels.Select(questionLevel => _mapper.Map<QuestionLevelGetDto>(questionLevel));
            return Ok(questionLevelsToReturn);
        }
    }
}
