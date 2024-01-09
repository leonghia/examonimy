
using ExamonimyWeb.Attributes;
using ExamonimyWeb.DTOs.CourseDTO;
using ExamonimyWeb.DTOs.QuestionDTO;
using ExamonimyWeb.DTOs.UserDTO;
using ExamonimyWeb.Entities;
using ExamonimyWeb.Managers.QuestionManager;
using ExamonimyWeb.Managers.UserManager;
using ExamonimyWeb.Models;
using ExamonimyWeb.Repositories;
using ExamonimyWeb.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Text.Json;

namespace ExamonimyWeb.Controllers
{
    [Route("")]
    public class QuestionController : BaseController
    {
        private readonly IUserManager _userManager;
        
        private readonly IGenericRepository<Question> _questionRepository;
        private readonly IGenericRepository<QuestionType> _questionTypeRepository;            
        private readonly IGenericRepository<Course> _courseRepository;
        private readonly IQuestionManager _questionManager;
         

        public QuestionController(IUserManager userManager, IGenericRepository<Question> questionRepository, IGenericRepository<QuestionType> questionTypeRepository, IGenericRepository<Course> courseRepository, IQuestionManager questionManager) : base(userManager)
        {
            _userManager = userManager;
            
            _questionRepository = questionRepository;
            _questionTypeRepository = questionTypeRepository;         
            _courseRepository = courseRepository;
            _questionManager = questionManager;
        }

        [CustomAuthorize(Roles = "Administrator,Teacher")]
        [HttpGet("question")]
        public async Task<IActionResult> RenderIndexView()
        {
          
            var questionTypesToReturn = (await _questionTypeRepository.GetPagedListAsync(null, null, null, null)).Select(qT => new QuestionTypeGetDto { Id = qT.Id, Name = qT.Name });
            
            var coursesTotalCount = await _courseRepository.CountAsync();
            var coursesToReturn = (await _courseRepository.GetPagedListAsync(new RequestParams { PageNumber = 1, PageSize = coursesTotalCount}, null, null, null)).Select(c => new CourseGetDto { CourseCode = c.CourseCode, Id = c.Id, Name = c.Name });
            var viewModel = new QuestionBankViewModel
            {
                User = (await base.GetContextUser()).Item2,               
                QuestionTypes = questionTypesToReturn,               
                Courses = coursesToReturn
            };
            return View("Index", viewModel);
        }

        [CustomAuthorize(Roles = "Administrator,Teacher")]
        [HttpGet("api/question")]
        [Produces("application/json")]
        public async Task<IActionResult> GetFullQuestions([FromQuery] RequestParamsForQuestion? questionRequestParams)
        {

            var questions = await _questionManager.GetFullPagedList(questionRequestParams);

            var paginationMetadata = new PaginationMetadata
            {
                PageSize = questions.PageSize,
                TotalCount = questions.TotalCount,
                CurrentPage = questions.PageNumber,
                TotalPages = questions.TotalPages
            };

            var questionsToReturn = new List<QuestionGetDto>(questions.Count);

            foreach (var q in questions)
            {
                questionsToReturn.Add(await _questionManager.GetFullQuestionDto(q.QuestionTypeId, q.Id));
            }

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

            return Ok(questionsToReturn);
        }

        [CustomAuthorize(Roles = "Administrator,Teacher")]
        [HttpGet("question/create")]
        public async Task<IActionResult> RenderCreateView()
        {
            var userToReturn = (await base.GetContextUser()).Item2;
            var authorizedViewModel = new AuthorizedViewModel
            {
                User = userToReturn
            };
            return View("Create", authorizedViewModel);
        }      

        [CustomAuthorize(Roles = "Administrator,Teacher")]
        [HttpGet("api/question/{id}", Name = "GetQuestionById")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var question = await _questionManager.GetByIdAsync(id);
            if (question is null) return NotFound();

            var questionToReturn = await _questionManager.GetFullQuestionDto(question.QuestionTypeId, question.Id);
            return Ok(questionToReturn);
        }

        [CustomAuthorize(Roles = "Administrator,Teacher")]
        [HttpPost("api/question/multiplechoicewithonecorrectanswer")]
        public async Task<IActionResult> Create([FromBody] MultipleChoiceQuestionWithOneCorrectAnswerCreateDto dto)
        {
            if (!ModelState.IsValid) return base.ValidationProblem(ModelState);
            var user = (await base.GetContextUser()).Item1;
            var q = new Question
            {
                CourseId = dto.CourseId,
                QuestionTypeId = dto.QuestionTypeId,
                QuestionContent = dto.QuestionContent,
                AuthorId = user.Id,
                CourseModuleId = dto.CourseModuleId
            };
            await _questionManager.CreateAsync(q);
            var qq = new MultipleChoiceQuestionWithOneCorrectAnswer
            {
                QuestionId = q.Id,
                ChoiceA = dto.ChoiceA,
                ChoiceB = dto.ChoiceB,
                ChoiceC = dto.ChoiceC,
                ChoiceD = dto.ChoiceD,
                CorrectAnswer = _questionManager.QuestionAnswerCharToByteMappings[dto.CorrectAnswer]
            };
            await _questionManager.CreateAsync(qq);
            var questionToReturn = await _questionManager.GetFullQuestionDto(q.QuestionTypeId, q.Id);
            return CreatedAtRoute("GetQuestionById", new { id = q.Id }, questionToReturn);
        }

        [CustomAuthorize(Roles = "Administrator,Teacher")]
        [HttpPost("api/question/multiplechoicewithmultiplecorrectanswers")]
        public async Task<IActionResult> Create([FromBody] MultipleChoiceQuestionWithMultipleCorrectAnswersCreateDto dto)
        {
            if (!ModelState.IsValid) return base.ValidationProblem(ModelState);
            var user = (await base.GetContextUser()).Item1;
            var q = new Question
            {
                CourseId = dto.CourseId,
                QuestionTypeId = dto.QuestionTypeId,
                QuestionContent = dto.QuestionContent,
                AuthorId = user.Id,
                CourseModuleId = dto.CourseModuleId
            };
            await _questionManager.CreateAsync(q);
            var qq = new MultipleChoiceQuestionWithMultipleCorrectAnswers
            {
                QuestionId = q.Id,
                ChoiceA = dto.ChoiceA,
                ChoiceB = dto.ChoiceB,
                ChoiceC = dto.ChoiceC,
                ChoiceD = dto.ChoiceD,
                CorrectAnswers = string.Join('|', dto.CorrectAnswers.Select(e => _questionManager.QuestionAnswerCharToByteMappings[e]))
            };
            await _questionManager.CreateAsync(qq);
            var questionToReturn = await _questionManager.GetFullQuestionDto(q.QuestionTypeId, q.Id);
            return CreatedAtRoute("GetQuestionById", new { id = q.Id }, questionToReturn);
        }

        [CustomAuthorize(Roles = "Administrator,Teacher")]
        [HttpPost("api/question/truefalse")]
        public async Task<IActionResult> Create([FromBody] TrueFalseQuestionCreateDto dto)
        {
            if (!ModelState.IsValid) return base.ValidationProblem(ModelState);
            var contextUser = (await base.GetContextUser()).Item1;
            var q = new Question
            {
                CourseId = dto.CourseId,
                QuestionTypeId = dto.QuestionTypeId,
                QuestionContent = dto.QuestionContent,
                AuthorId = contextUser.Id,
                CourseModuleId = dto.CourseModuleId
            };
            await _questionManager.CreateAsync(q);
            var qq = new TrueFalseQuestion
            {
                QuestionId = q.Id,
                CorrectAnswer = dto.CorrectAnswer
            };
            await _questionManager.CreateAsync(qq);
            var questionToReturn = await _questionManager.GetFullQuestionDto(q.QuestionTypeId, q.Id);
            return CreatedAtRoute("GetQuestionById", new { id = q.Id }, questionToReturn);
        }

        [CustomAuthorize(Roles = "Administrator,Teacher")]
        [HttpPost("api/question/shortanswer")]
        public async Task<IActionResult> Create([FromBody] ShortAnswerQuestionCreateDto dto)
        {
            if (!ModelState.IsValid) return base.ValidationProblem(ModelState);
            var contextUser = (await base.GetContextUser()).Item1;
            var q = new Question
            {
                CourseId = dto.CourseId,
                QuestionTypeId = dto.QuestionTypeId,
                QuestionContent = dto.QuestionContent,
                AuthorId = contextUser.Id,
                CourseModuleId = dto.CourseModuleId
            };
            await _questionManager.CreateAsync(q);
            var qq = new ShortAnswerQuestion
            {
                QuestionId = q.Id,              
                CorrectAnswer = dto.CorrectAnswer
            };
            await _questionManager.CreateAsync(qq);
            var questionToReturn = await _questionManager.GetFullQuestionDto(q.QuestionTypeId, q.Id);
            return CreatedAtRoute("GetQuestionById", new { id = q.Id }, questionToReturn);
        }

        [CustomAuthorize(Roles = "Administrator,Teacher")]
        [HttpPost("api/question/fillinblank")]
        public async Task<IActionResult> Create([FromBody] FillInBlankQuestionCreateDto dto)
        {
            if (!ModelState.IsValid) return base.ValidationProblem(ModelState);
            var contextUser = (await base.GetContextUser()).Item1;
            var q = new Question
            {
                CourseId = dto.CourseId,
                QuestionTypeId = dto.QuestionTypeId,
                QuestionContent = dto.QuestionContent,
                AuthorId = contextUser.Id,
                CourseModuleId = dto.CourseModuleId
            };
            await _questionManager.CreateAsync(q);
            var qq = new FillInBlankQuestion
            {
                QuestionId = q.Id,
                CorrectAnswers = string.Join('|', dto.CorrectAnswers)
            };
            await _questionManager.CreateAsync(qq);
            var questionToReturn = await _questionManager.GetFullQuestionDto(q.QuestionTypeId, q.Id);
            return CreatedAtRoute("GetQuestionById", new { id = q.Id }, questionToReturn);
        }

        [CustomAuthorize(Roles = "Administrator,Teacher")]
        [HttpGet("api/question/type")]
        [Produces("application/json")]
        public async Task<IActionResult> GetQuestionTypes()
        {
            var questionTypes = await _questionTypeRepository.GetPagedListAsync(null, null, null, null);
            var questionTypesToReturn = questionTypes.Select(questionType => new QuestionTypeGetDto { Id = questionType.Id, Name = questionType.Name });
            return Ok(questionTypesToReturn);
        }      
        
          

        [CustomAuthorize(Roles = "Administrator,Teacher")]
        [HttpDelete("api/question/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var q = await _questionManager.GetByIdAsync(id);
            if (q is null) return NotFound();
            await _questionManager.DeleteAsync(id);
            return NoContent();
        }
    }
}
