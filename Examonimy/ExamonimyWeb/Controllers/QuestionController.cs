using AutoMapper;
using ExamonimyWeb.Attributes;
using ExamonimyWeb.DTOs.QuestionDTO;
using ExamonimyWeb.DTOs.UserDTO;
using ExamonimyWeb.Entities;
using ExamonimyWeb.Enums;
using ExamonimyWeb.Managers.UserManager;
using ExamonimyWeb.Models;
using ExamonimyWeb.Repositories.GenericRepository;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace ExamonimyWeb.Controllers
{
    [Route("")]
    public class QuestionController : GenericController<Question>
    {
        private readonly IUserManager _userManager;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Question> _questionRepository;
        private readonly IGenericRepository<QuestionType> _questionTypeRepository;
        private readonly IGenericRepository<QuestionLevel> _questionLevelRepository;
        private readonly IGenericRepository<MultipleChoiceQuestionWithOneCorrectAnswer> _multipleChoiceQuestionWithOneCorrectAnswerRepository;
        private readonly IGenericRepository<MultipleChoiceQuestionWithMultipleCorrectAnswers> _multipleChoiceQuestionWithMultipleCorrectAnswersRepository;
        private readonly IGenericRepository<TrueFalseQuestion> _trueFalseQuestionRepository;
        private readonly IGenericRepository<ShortAnswerQuestion> _shortAnswerQuestionRepository;
        private readonly IGenericRepository<FillInBlankQuestion> _fillInBlankQuestionRepository;

        public QuestionController(IUserManager userManager, IMapper mapper, IGenericRepository<Question> questionRepository, IGenericRepository<QuestionType> questionTypeRepository, IGenericRepository<QuestionLevel> questionLevelRepository, IGenericRepository<MultipleChoiceQuestionWithOneCorrectAnswer> multipleChoiceQuestionWithOneCorrectAnswerRepository, IGenericRepository<MultipleChoiceQuestionWithMultipleCorrectAnswers> multipleChoiceQuestionWithMultipleCorrectAnswersRepository, IGenericRepository<TrueFalseQuestion> trueFalseQuestionRepository, IGenericRepository<ShortAnswerQuestion> shortAnswerQuestionRepository, IGenericRepository<FillInBlankQuestion> fillInBlankQuestionRepository) : base(mapper, questionRepository)
        {
            _userManager = userManager;
            _mapper = mapper;
            _questionRepository = questionRepository;
            _questionTypeRepository = questionTypeRepository;
            _questionLevelRepository = questionLevelRepository;
            _multipleChoiceQuestionWithOneCorrectAnswerRepository = multipleChoiceQuestionWithOneCorrectAnswerRepository;
            _multipleChoiceQuestionWithMultipleCorrectAnswersRepository = multipleChoiceQuestionWithMultipleCorrectAnswersRepository;
            _trueFalseQuestionRepository = trueFalseQuestionRepository;
            _shortAnswerQuestionRepository = shortAnswerQuestionRepository;
            _fillInBlankQuestionRepository = fillInBlankQuestionRepository;
        }

        [CustomAuthorize(Roles = "Administrator")]
        [HttpGet("question")]
        public async Task<IActionResult> Bank()
        {
            var username = HttpContext.User.Identity!.Name;                  
            var userToReturn = _mapper.Map<UserGetDto>(await _userManager.FindByUsernameAsync(username!));          
            var questionsToReturn = (await _questionRepository.GetAsync(null, null, new List<string> { "QuestionType", "QuestionLevel", "Course", "Author" })).Select(q => _mapper.Map<QuestionGetDto>(q));          
            var questionTypesToReturn = (await _questionTypeRepository.GetAsync(null, null, null)).Select(qT => _mapper.Map<QuestionTypeGetDto>(qT));
            var viewModel = new QuestionBankViewModel
            {
                User = userToReturn,
                Questions = questionsToReturn,
                QuestionTypes = questionTypesToReturn
            };
            return View(viewModel);
        }

        [CustomAuthorize(Roles = "Administrator")]
        [HttpGet("question/create")]
        public async Task<IActionResult> Create()
        {
            var username = HttpContext.User.Identity!.Name;
            var user = await _userManager.FindByUsernameAsync(username!);        
            var userGetDto = _mapper.Map<UserGetDto>(user);
            var authorizedViewModel = new AuthorizedViewModel
            {
                User = userGetDto
            };
            return View(authorizedViewModel);
        }

        [CustomAuthorize]
        [HttpGet("question/{id}", Name = "GetQuestionById")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            Expression<Func<Question, bool>> predicate = q => q.Id == id;
            var question = await _questionRepository.GetAsync(predicate, null);
            if (question is null)
                return NotFound();
            switch (question.QuestionTypeId)
            {
                case (int)QuestionTypeId.MultipleChoiceWithOneCorrectAnswer:
                    var multipleChoiceQuestionWithOneCorrectAnswer = await _multipleChoiceQuestionWithOneCorrectAnswerRepository.GetAsync(q => q.QuestionId == id, new List<string> { "Question" });
                    if (multipleChoiceQuestionWithOneCorrectAnswer is null)
                        return NotFound();
                    var mutipleChoiceQuestionWithOneCorrectAnswerToReturn = _mapper.Map<MultipleChoiceQuestionWithOneCorrectAnswerGetDto>(multipleChoiceQuestionWithOneCorrectAnswer);
                    return View("MultipleChoiceWithOneCorrectAnswer", mutipleChoiceQuestionWithOneCorrectAnswerToReturn);
                case (int)QuestionTypeId.MultipleChoiceWithMultipleCorrectAnswers:
                    var multipleChoiceQuestionWithMultipleCorrectAnswers = await _multipleChoiceQuestionWithMultipleCorrectAnswersRepository.GetAsync(q => q.QuestionId == id, new List<string> { "Question" });
                    if (multipleChoiceQuestionWithMultipleCorrectAnswers is null)
                        return NotFound();
                    var multipleChoiceQuestionWithMultipleCorrectAnswersToReturn = _mapper.Map<MultipleChoiceQuestionWithMultipleCorrectAnswersGetDto>(multipleChoiceQuestionWithMultipleCorrectAnswers);
                    return View("MultipleChoiceWithMultipleCorrectAnswers", multipleChoiceQuestionWithMultipleCorrectAnswersToReturn);
                case (int)QuestionTypeId.TrueFalse:
                    var trueFalseQuestion = await _trueFalseQuestionRepository.GetAsync(q => q.QuestionId == id, new List<string> { "Question" });
                    if (trueFalseQuestion is null)
                        return NotFound();
                    var trueFalseQuestionToReturn = _mapper.Map<TrueFalseQuestionGetDto>(trueFalseQuestion);
                    return View("MultipleChoiceWithMultipleCorrectAnswers", trueFalseQuestionToReturn);
                case (int)QuestionTypeId.ShortAnswer:
                    var shortAnswerQuestion = await _shortAnswerQuestionRepository.GetAsync(q => q.QuestionId == id, new List<string> { "Question" });
                    if (shortAnswerQuestion is null)
                        return NotFound();
                    var shortAnswerQuestionToReturn = _mapper.Map<TrueFalseQuestionGetDto>(shortAnswerQuestion);
                    return View("MultipleChoiceWithMultipleCorrectAnswers", shortAnswerQuestionToReturn);
                case (int)QuestionTypeId.FillInBlank:
                    var fillInBlankQuestion = await _trueFalseQuestionRepository.GetAsync(q => q.QuestionId == id, new List<string> { "Question" });
                    if (fillInBlankQuestion is null)
                        return NotFound();
                    var fillInBlankQuestionToReturn = _mapper.Map<TrueFalseQuestionGetDto>(fillInBlankQuestion);
                    return View("MultipleChoiceWithMultipleCorrectAnswers", fillInBlankQuestionToReturn);
                default:
                    return NotFound();
            }
        }

        private async Task<Tuple<int, T>> CreateQuestion<T>(QuestionCreateDto questionCreateDto, IGenericRepository<T> specificQuestionRepository, string username) where T : class
        {
            var user = await _userManager.FindByUsernameAsync(username!);
            var questionToCreate = new Question
            {
                CourseId = questionCreateDto.CourseId,
                QuestionTypeId = questionCreateDto.QuestionTypeId,
                QuestionLevelId = questionCreateDto.QuestionLevelId,
                QuestionContent = questionCreateDto.QuestionContent,
                AuthorId = user!.Id
            };
            await _questionRepository.InsertAsync(questionToCreate);
            await _questionRepository.SaveAsync();
            var specificQuestionToCreate = _mapper.Map<T>(questionCreateDto);
            specificQuestionToCreate.GetType().GetProperty("QuestionId")!.SetValue(specificQuestionToCreate, questionToCreate.Id);
            await specificQuestionRepository.InsertAsync(specificQuestionToCreate);
            await specificQuestionRepository.SaveAsync();
            return Tuple.Create(questionToCreate.Id, specificQuestionToCreate);
        }

        [CustomAuthorize(Roles = "Administrator")]
        [HttpPost("api/question/multiplechoicewithonecorrectanswer")]
        public async Task<IActionResult> Create([FromBody] MultipleChoiceQuestionWithOneCorrectAnswerCreateDto multipleChoiceQuestionWithOneCorrectAnswerCreateDto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);          
            var tuple = await CreateQuestion(multipleChoiceQuestionWithOneCorrectAnswerCreateDto, _multipleChoiceQuestionWithOneCorrectAnswerRepository, HttpContext.User.Identity!.Name!);
            return CreatedAtRoute("GetQuestionById", new { id = tuple.Item1 }, _mapper.Map<MultipleChoiceQuestionWithOneCorrectAnswerGetDto>(tuple.Item2));
        }

        [CustomAuthorize(Roles = "Administrator")]
        [HttpPost("api/question/multiplechoicewithmultiplecorrectanswers")]
        public async Task<IActionResult> Create([FromBody] MultipleChoiceQuestionWithMultipleCorrectAnswersCreateDto multipleChoiceQuestionWithMultipleCorrectAnswersCreateDto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);
            var tuple = await CreateQuestion(multipleChoiceQuestionWithMultipleCorrectAnswersCreateDto, _multipleChoiceQuestionWithMultipleCorrectAnswersRepository, HttpContext.User.Identity!.Name!);
            return CreatedAtRoute("GetQuestionById", new { id = tuple.Item1 }, _mapper.Map<MultipleChoiceQuestionWithMultipleCorrectAnswersGetDto>(tuple.Item2));
        }

        [CustomAuthorize(Roles = "Administrator")]
        [HttpPost("api/question/truefalse")]
        public async Task<IActionResult> Create([FromBody] TrueFalseQuestionCreateDto trueFalseQuestionCreateDto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);
            var tuple = await CreateQuestion(trueFalseQuestionCreateDto, _trueFalseQuestionRepository, HttpContext.User.Identity!.Name!);
            return CreatedAtRoute("GetQuestionById", new { id = tuple.Item1 }, _mapper.Map<TrueFalseQuestionGetDto>(tuple.Item2));
        }

        [CustomAuthorize(Roles = "Administrator")]
        [HttpPost("api/question/shortanswer")]
        public async Task<IActionResult> Create([FromBody] ShortAnswerQuestionCreateDto shortAnswerQuestionCreateDto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);
            var tuple = await CreateQuestion(shortAnswerQuestionCreateDto, _shortAnswerQuestionRepository, HttpContext.User.Identity!.Name!);
            return CreatedAtRoute("GetQuestionById", new { id = tuple.Item1 }, _mapper.Map<ShortAnswerQuestionGetDto>(tuple.Item2));
        }

        [CustomAuthorize(Roles = "Administrator")]
        [HttpPost("api/question/fillinblank")]
        public async Task<IActionResult> Create([FromBody] FillInBlankQuestionCreateDto fillInBlankQuestionCreateDto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);
            var tuple = await CreateQuestion(fillInBlankQuestionCreateDto, _fillInBlankQuestionRepository, HttpContext.User.Identity!.Name!);
            return CreatedAtRoute("GetQuestionById", new { id = tuple.Item1 }, _mapper.Map<FillInBlankQuestionGetDto>(tuple.Item2));
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
