﻿using AutoMapper;
using ExamonimyWeb.Attributes;
using ExamonimyWeb.DTOs.CourseDTO;
using ExamonimyWeb.DTOs.QuestionDTO;
using ExamonimyWeb.DTOs.UserDTO;
using ExamonimyWeb.Entities;
using ExamonimyWeb.Managers.QuestionManager;
using ExamonimyWeb.Managers.UserManager;
using ExamonimyWeb.Models;
using ExamonimyWeb.Repositories.GenericRepository;
using ExamonimyWeb.Utilities;
using LinqKit;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Text.Json;

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
        private readonly IGenericRepository<Course> _courseRepository;
        private readonly IQuestionManager _questionManager;

        public QuestionController(IUserManager userManager, IMapper mapper, IGenericRepository<Question> questionRepository, IGenericRepository<QuestionType> questionTypeRepository, IGenericRepository<QuestionLevel> questionLevelRepository, IGenericRepository<Course> courseRepository, IQuestionManager questionManager) : base(mapper, questionRepository, userManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _questionRepository = questionRepository;
            _questionTypeRepository = questionTypeRepository;
            _questionLevelRepository = questionLevelRepository;          
            _courseRepository = courseRepository;
            _questionManager = questionManager;
        }

        [CustomAuthorize(Roles = "Administrator,Teacher")]
        [HttpGet("question")]
        public async Task<IActionResult> Index()
        {
            var username = HttpContext.User.Identity!.Name;                  
            var userToReturn = _mapper.Map<UserGetDto>(await _userManager.FindByUsernameAsync(username!));    
            var questionTypesToReturn = (await _questionTypeRepository.GetAsync(null, null, null, null)).Select(qT => _mapper.Map<QuestionTypeGetDto>(qT));
            var questionLevelsToReturn = (await _questionLevelRepository.GetAsync(null, null, null, null)).Select(qL => _mapper.Map<QuestionLevelGetDto>(qL));
            var coursesTotalCount = await _courseRepository.CountAsync();
            var coursesToReturn = (await _courseRepository.GetAsync(new RequestParams { PageNumber = 1, PageSize = coursesTotalCount}, null, null, null)).Select(c => _mapper.Map<CourseGetDto>(c));
            var viewModel = new QuestionBankViewModel
            {
                User = userToReturn,               
                QuestionTypes = questionTypesToReturn,
                QuestionLevels = questionLevelsToReturn,
                Courses = coursesToReturn
            };
            return View(viewModel);
        }

        [CustomAuthorize(Roles = "Administrator,Teacher")]
        [HttpGet("api/question")]
        [Produces("application/json")]
        public async Task<IActionResult> Get([FromQuery] QuestionRequestParams? questionRequestParams)
        {
            Expression<Func<Question, bool>>? searchPredicate = null; 
            if (questionRequestParams?.SearchQuery is not null)
            {
                searchPredicate = q => q.QuestionContent.ToUpper().Contains(questionRequestParams.SearchQuery.ToUpper());
            }

            var filterPredicate = PredicateBuilder.New<Question>(true);

            if (questionRequestParams?.CourseId is not null)
            {
                filterPredicate = filterPredicate.And(q => q.CourseId == questionRequestParams.CourseId);
            }

            if (questionRequestParams?.QuestionTypeId is not null)
            {
                filterPredicate = filterPredicate.And(q => q.QuestionTypeId == questionRequestParams.QuestionTypeId);
            }

            if (questionRequestParams?.QuestionLevelId is not null)
            {
                filterPredicate = filterPredicate.And(q => q.QuestionLevelId == questionRequestParams.QuestionLevelId);
            }

            var questions = await _questionRepository.GetAsync(questionRequestParams, searchPredicate, filterPredicate, new List<string> { "Course", "QuestionType", "QuestionLevel", "Author" });
            var questionsToReturn = await _questionManager.GetQuestionsAsync(questions);         
            

            var paginationMetadata = new PaginationMetadata
            {
                PageSize = questions.PageSize,
                TotalCount = questions.TotalCount,
                CurrentPage = questions.PageNumber,
                TotalPages = questions.TotalPages
            };

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

            return Ok(questionsToReturn);
        }

        [CustomAuthorize(Roles = "Administrator,Teacher")]
        [HttpGet("question/create")]
        public async Task<IActionResult> Create()
        {                 
            var userGetDto = _mapper.Map<UserGetDto>(await base.GetContextUser());
            var authorizedViewModel = new AuthorizedViewModel
            {
                User = userGetDto
            };
            return View(authorizedViewModel);
        }

        [CustomAuthorize(Roles = "Administrator,Teacher")]
        [HttpGet("question/{id}", Name = "GetQuestionById")]
        public async Task<IActionResult> Single([FromRoute] int id)
        {
            if (!await _questionManager.ExistAsync(id))
                return NotFound();
            var user = await base.GetContextUser();
            var viewModel = await _questionManager.GetQuestionViewModelAsync(id, user);           
            return View(viewModel.ViewName, viewModel);
        }

        [CustomAuthorize(Roles = "Administrator,Teacher")]
        [HttpGet("api/question/{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            if (!await _questionManager.ExistAsync(id))
                return NotFound();
            var user = await base.GetContextUser();
            if (!await _questionManager.IsAuthor(id, user.Id))
                return Forbid();
            var questionToReturn = await _questionManager.GetSpecificQuestionDtoAsync(id);
            return Ok(questionToReturn);
        }

        [CustomAuthorize(Roles = "Administrator,Teacher")]
        [HttpPost("api/question/multiplechoicewithonecorrectanswer")]
        public async Task<IActionResult> Create([FromBody] MultipleChoiceQuestionWithOneCorrectAnswerCreateDto multipleChoiceQuestionWithOneCorrectAnswerCreateDto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);
            var authorId = (await base.GetContextUser()).Id;         
            var tuple = await _questionManager.CreateQuestionAsync(multipleChoiceQuestionWithOneCorrectAnswerCreateDto, _questionManager.MultipleChoiceQuestionWithOneCorrectAnswerRepository, authorId);
            return CreatedAtRoute("GetQuestionById", new { id = tuple.Item1 }, _mapper.Map<MultipleChoiceQuestionWithOneCorrectAnswerGetDto>(tuple.Item2));
        }

        [CustomAuthorize(Roles = "Administrator,Teacher")]
        [HttpPost("api/question/multiplechoicewithmultiplecorrectanswers")]
        public async Task<IActionResult> Create([FromBody] MultipleChoiceQuestionWithMultipleCorrectAnswersCreateDto multipleChoiceQuestionWithMultipleCorrectAnswersCreateDto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);
            var authorId = (await base.GetContextUser()).Id;
            var tuple = await _questionManager.CreateQuestionAsync(multipleChoiceQuestionWithMultipleCorrectAnswersCreateDto, _questionManager.MultipleChoiceQuestionWithMultipleCorrectAnswersRepository, authorId);
            return CreatedAtRoute("GetQuestionById", new { id = tuple.Item1 }, _mapper.Map<MultipleChoiceQuestionWithMultipleCorrectAnswersGetDto>(tuple.Item2));
        }

        [CustomAuthorize(Roles = "Administrator,Teacher")]
        [HttpPost("api/question/truefalse")]
        public async Task<IActionResult> Create([FromBody] TrueFalseQuestionCreateDto trueFalseQuestionCreateDto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);
            var authorId = (await base.GetContextUser()).Id;
            var tuple = await _questionManager.CreateQuestionAsync(trueFalseQuestionCreateDto, _questionManager.TrueFalseQuestionRepository, authorId);
            return CreatedAtRoute("GetQuestionById", new { id = tuple.Item1 }, _mapper.Map<TrueFalseQuestionGetDto>(tuple.Item2));
        }

        [CustomAuthorize(Roles = "Administrator,Teacher")]
        [HttpPost("api/question/shortanswer")]
        public async Task<IActionResult> Create([FromBody] ShortAnswerQuestionCreateDto shortAnswerQuestionCreateDto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);
            var authorId = (await base.GetContextUser()).Id;
            var tuple = await  _questionManager.CreateQuestionAsync(shortAnswerQuestionCreateDto, _questionManager.ShortAnswerQuestionRepository, authorId);
            return CreatedAtRoute("GetQuestionById", new { id = tuple.Item1 }, _mapper.Map<ShortAnswerQuestionGetDto>(tuple.Item2));
        }

        [CustomAuthorize(Roles = "Administrator,Teacher")]
        [HttpPost("api/question/fillinblank")]
        public async Task<IActionResult> Create([FromBody] FillInBlankQuestionCreateDto fillInBlankQuestionCreateDto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);
            var authorId = (await base.GetContextUser()).Id;
            var tuple = await _questionManager.CreateQuestionAsync(fillInBlankQuestionCreateDto, _questionManager.FillInBlankQuestionRepository, authorId);
            return CreatedAtRoute("GetQuestionById", new { id = tuple.Item1 }, _mapper.Map<FillInBlankQuestionGetDto>(tuple.Item2));
        }

        [CustomAuthorize(Roles = "Administrator,Teacher")]
        [HttpGet("api/question/type")]
        [Produces("application/json")]
        public async Task<IActionResult> GetQuestionTypes()
        {
            var questionTypes = await _questionTypeRepository.GetAsync(null, null, null, null);
            var questionTypesToReturn = questionTypes.Select(questionType => _mapper.Map<QuestionTypeGetDto>(questionType));
            return Ok(questionTypesToReturn);
        }

        [CustomAuthorize(Roles = "Administrator,Teacher")]
        [HttpGet("api/question/level")]
        [Produces("application/json")]
        public async Task<IActionResult> GetQuestionLevels()
        {
            var questionLevels = await _questionLevelRepository.GetAsync(null, null, null, null);
            var questionLevelsToReturn = questionLevels.Select(questionLevel => _mapper.Map<QuestionLevelGetDto>(questionLevel));
            return Ok(questionLevelsToReturn);
        }

        [CustomAuthorize(Roles = "Administrator,Teacher")]
        [HttpGet("question/edit/{id}")]
        public async Task<IActionResult> Update([FromRoute] int id)
        {
            Expression<Func<Question, bool>> predicate = q => q.Id == id;
            var question = await _questionRepository.GetAsync(predicate, new List<string> { "Author", "Course", "QuestionType", "QuestionLevel" });
            if (question is null)
                return NotFound();
            var user = await base.GetContextUser();
            if (question.Author!.Id != user.Id)
                return Forbid();           
            var questionTypesToReturn = (await _questionTypeRepository.GetAsync(null, null, null, null)).Select(qT => _mapper.Map<QuestionTypeGetDto>(qT));
            var questionLevelsToReturn = (await _questionLevelRepository.GetAsync(null, null, null, null)).Select(qL => _mapper.Map<QuestionLevelGetDto>(qL));
            var questionToReturn = _mapper.Map<QuestionGetDto>(question);
            var editQuestionViewModel = new EditQuestionViewModel
            {
                User = _mapper.Map<UserGetDto>(user),               
                QuestionTypes = questionTypesToReturn,
                QuestionLevels = questionLevelsToReturn,
                Question = questionToReturn
            };
            return View("Edit", editQuestionViewModel);
        }
    }
}
