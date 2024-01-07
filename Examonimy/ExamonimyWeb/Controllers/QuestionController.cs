
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
using LinqKit;
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

            var contextUser = await base.GetContextUser();
            var userToReturn = new UserGetDto
            {
                Id = contextUser.Id,
                FullName = contextUser.FullName,
                ProfilePicture = contextUser.ProfilePicture
            };
            var questionTypesToReturn = (await _questionTypeRepository.GetPagedListAsync(null, null, null, null)).Select(qT => new QuestionTypeGetDto { Id = qT.Id, Name = qT.Name });
            
            var coursesTotalCount = await _courseRepository.CountAsync();
            var coursesToReturn = (await _courseRepository.GetPagedListAsync(new RequestParams { PageNumber = 1, PageSize = coursesTotalCount}, null, null, null)).Select(c => new CourseGetDto { CourseCode = c.CourseCode, Id = c.Id, Name = c.Name });
            var viewModel = new QuestionBankViewModel
            {
                User = userToReturn,               
                QuestionTypes = questionTypesToReturn,               
                Courses = coursesToReturn
            };
            return View("Index", viewModel);
        }

        [CustomAuthorize(Roles = "Administrator,Teacher")]
        [HttpGet("api/question")]
        [Produces("application/json")]
        public async Task<IActionResult> Get([FromQuery] RequestParamsForQuestion? questionRequestParams)
        {
            var predicate = PredicateBuilder.New<Question>(true);

            if (questionRequestParams?.SearchQuery is not null)
            {
                predicate = predicate.And(q => q.QuestionContent.ToUpper().Contains(questionRequestParams.SearchQuery.ToUpper()));
            }           

            if (questionRequestParams?.CourseId is not null)
            {
                predicate = predicate.And(q => q.CourseId == questionRequestParams.CourseId);
            }

            if (questionRequestParams?.QuestionTypeId is not null)
            {
                predicate = predicate.And(q => q.QuestionTypeId == questionRequestParams.QuestionTypeId);
            }

            

            var questions = await _questionRepository.GetPagedListAsync(questionRequestParams, predicate, new List<string> { "Course", "QuestionType", "QuestionLevel", "Author" });
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
        public async Task<IActionResult> RenderCreateView()
        {                 
            var userGetDto = _mapper.Map<UserGetDto>(await base.GetContextUser());
            var authorizedViewModel = new AuthorizedViewModel
            {
                User = userGetDto
            };
            return View("Create", authorizedViewModel);
        }

        [CustomAuthorize(Roles = "Administrator,Teacher")]
        [HttpGet("question/{id}", Name = "GetQuestionById")]
        public async Task<IActionResult> RenderSingleView([FromRoute] int id)
        {
            if (!await _questionManager.DoesQuestionExistAsync(id))
                return NotFound();
            var user = await base.GetContextUser();
            var viewModel = await _questionManager.GetQuestionViewModelAsync(id, user);           
            return View(viewModel.ViewName, viewModel);
        }

        [CustomAuthorize(Roles = "Administrator,Teacher")]
        [HttpGet("api/question/{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            if (!await _questionManager.DoesQuestionExistAsync(id))
                return NotFound();
            var user = await base.GetContextUser();
            if (!await _questionManager.IsAuthorAsync(id, user.Id))
                return Forbid();
            var questionToReturn = await _questionManager.GetSpecificQuestionWithAnswerDtoAsync(id);
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
            var questionTypes = await _questionTypeRepository.GetPagedListAsync(null, null, null, null);
            var questionTypesToReturn = questionTypes.Select(questionType => _mapper.Map<QuestionTypeGetDto>(questionType));
            return Ok(questionTypesToReturn);
        }

        

        [CustomAuthorize(Roles = "Administrator,Teacher")]
        [HttpGet("question/edit/{id}")]
        public async Task<IActionResult> RenderUpdateView([FromRoute] int id)
        {
            Expression<Func<Question, bool>> predicate = q => q.Id == id;
            var question = await _questionRepository.GetAsync(predicate, new List<string> { "Author", "Course", "QuestionType", "QuestionLevel" });
            if (question is null)
                return NotFound();
            var user = await base.GetContextUser();
            if (question.Author!.Id != user.Id)
                return Forbid();           
            var questionTypesToReturn = (await _questionTypeRepository.GetPagedListAsync(null, null, null, null)).Select(qT => _mapper.Map<QuestionTypeGetDto>(qT));
            
            var questionToReturn = _mapper.Map<QuestionGetDto>(question);
            var editQuestionViewModel = new EditQuestionViewModel
            {
                User = _mapper.Map<UserGetDto>(user),               
                QuestionTypes = questionTypesToReturn,
                
                Question = questionToReturn
            };
            return View("Edit", editQuestionViewModel);
        }

        [CustomAuthorize(Roles = "Administrator,Teacher")]
        [HttpPut("api/question/multiplechoicewithonecorrectanswer/{id}")]
        [Consumes("application/json")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] MultipleChoiceQuestionWithOneCorrectAnswerUpdateDto questionUpdateDto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);
            var exist = await _questionManager.DoesQuestionExistAsync(id);
            if (!exist)
                return NotFound();
            var contextUser = await base.GetContextUser();
            var isAuthor = await _questionManager.IsAuthorAsync(id, contextUser.Id);
            if (!isAuthor)
                return Forbid();
            await _questionManager.UpdateThenSaveAsync(id, questionUpdateDto);
            return NoContent();
        }

        [CustomAuthorize(Roles = "Administrator,Teacher")]
        [HttpPut("api/question/multiplechoicewithmultiplecorrectanswers/{id}")]
        [Consumes("application/json")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] MultipleChoiceQuestionWithMultipleCorrectAnswersUpdateDto questionUpdateDto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var exist = await _questionManager.DoesQuestionExistAsync(id);
            if (!exist)
                return NotFound();
            var contextUser = await base.GetContextUser();
            var isAuthor = await _questionManager.IsAuthorAsync(id, contextUser.Id);
            if (!isAuthor)
                return Forbid();
            await _questionManager.UpdateThenSaveAsync(id, questionUpdateDto);
            return NoContent();
        }

        [CustomAuthorize(Roles = "Administrator,Teacher")]
        [HttpPut("api/question/truefalse/{id}")]
        [Consumes("application/json")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] TrueFalseQuestionUpdateDto questionUpdateDto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var exist = await _questionManager.DoesQuestionExistAsync(id);
            if (!exist)
                return NotFound();
            var contextUser = await base.GetContextUser();
            var isAuthor = await _questionManager.IsAuthorAsync(id, contextUser.Id);
            if (!isAuthor)
                return Forbid();
            await _questionManager.UpdateThenSaveAsync(id, questionUpdateDto);
            return NoContent();
        }

        [CustomAuthorize(Roles = "Administrator,Teacher")]
        [HttpPut("api/question/shortanswer/{id}")]
        [Consumes("application/json")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] ShortAnswerQuestionUpdateDto questionUpdateDto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var exist = await _questionManager.DoesQuestionExistAsync(id);
            if (!exist)
                return NotFound();
            var contextUser = await base.GetContextUser();
            var isAuthor = await _questionManager.IsAuthorAsync(id, contextUser.Id);
            if (!isAuthor)
                return Forbid();
            await _questionManager.UpdateThenSaveAsync(id, questionUpdateDto);
            return NoContent();
        }

        [CustomAuthorize(Roles = "Administrator,Teacher")]
        [HttpPut("api/question/fillinblank/{id}")]
        [Consumes("application/json")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] FillInBlankQuestionUpdateDto questionUpdateDto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var exist = await _questionManager.DoesQuestionExistAsync(id);
            if (!exist)
                return NotFound();
            var contextUser = await base.GetContextUser();
            var isAuthor = await _questionManager.IsAuthorAsync(id, contextUser.Id);
            if (!isAuthor)
                return Forbid();
            await _questionManager.UpdateThenSaveAsync(id, questionUpdateDto);
            return NoContent();
        }

        [CustomAuthorize(Roles = "Administrator,Teacher")]
        [HttpDelete("api/question/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var exist = await _questionManager.DoesQuestionExistAsync(id);
            if (!exist)
                return NotFound();
            var contextUser = await base.GetContextUser();
            var isAuthor = await _questionManager.IsAuthorAsync(id, contextUser.Id);
            if (!isAuthor)
                return Forbid();
            await _questionManager.DeleteThenSaveAsync(id);
            return NoContent();
        }
    }
}
