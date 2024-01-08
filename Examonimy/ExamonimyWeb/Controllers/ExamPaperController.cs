using ExamonimyWeb.Attributes;
using ExamonimyWeb.DTOs.CourseDTO;
using ExamonimyWeb.DTOs.ExamPaperDTO;
using ExamonimyWeb.DTOs.UserDTO;
using ExamonimyWeb.Entities;
using ExamonimyWeb.Enums;
using ExamonimyWeb.Extensions;
using ExamonimyWeb.Managers.ExamPaperManager;
using ExamonimyWeb.Managers.UserManager;
using ExamonimyWeb.Models;
using ExamonimyWeb.Repositories;
using ExamonimyWeb.Services.NotificationService;
using ExamonimyWeb.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ExamonimyWeb.Controllers
{
    [Route("")]
    public class ExamPaperController : BaseController
    {
               
        private readonly IUserManager _userManager;     
        private readonly IGenericRepository<Course> _courseRepository;
        private readonly IExamPaperManager _examPaperManager;
        private readonly INotificationService _notificationService;

        public ExamPaperController(IUserManager userManager, IGenericRepository<Course> courseRepository, IExamPaperManager examPaperManager, INotificationService notificationService) : base(userManager)
        {
                
            _userManager = userManager;          
            _courseRepository = courseRepository;
            _examPaperManager = examPaperManager;
            _notificationService = notificationService;
        }

        [CustomAuthorize(Roles = "Administrator,Teacher")]
        [HttpGet("exam-paper")]
        public async Task<IActionResult> RenderIndexView()
        {

            
            var coursesCount = await _courseRepository.CountAsync(null);
            var coursesToReturn = (await _courseRepository.GetPagedListAsync(new RequestParams { PageSize = coursesCount }, null, null, null)).Select(c => new CourseGetDto{ CourseCode = c.CourseCode, Id = c.Id, Name = c.Name });
            var statusesToReturn = new List<ExamPaperStatusModel>();
            foreach (ExamPaperStatus e in Enum.GetValues(typeof(ExamPaperStatus)))
            {
                statusesToReturn.Add(new ExamPaperStatusModel { ExamPaperStatus = (int)e, ExamPaperStatusAsString = e.ToVietnameseString() });
            }
            var viewModel = new ExamPaperBankViewModel { User = (await base.GetContextUser()).Item2, Courses = coursesToReturn, Statuses = statusesToReturn };
            return View("Index", viewModel);
        }

        [CustomAuthorize(Roles = "Admin,Teacher")]
        [HttpGet("exam-paper/{id}")]
        [HttpGet("exam-paper/{id}/review")]
        public async Task<IActionResult> RenderSingleView([FromRoute] int id)
        {
            
            var examPaper = await _examPaperManager.GetAsync(ep => ep.Id == id, new List<string> { "Author", "Course", "Reviewers" });
            if (examPaper is null)
                return NotFound();
            var examPaperToReturn = new ExamPaperFullGetDto
            {
                ExamPaperCode = examPaper.ExamPaperCode,
                Course = examPaper.Course!.Name,
                Author = examPaper.Author!.FullName,
                StatusAsString = examPaper.Status.ToVietnameseString(),
                Status = (byte)examPaper.Status,
                Reviewers = examPaper.Reviewers!.Select(r => new UserGetDto
                {
                    Id = r.Id,
                    FullName = r.FullName,
                    ProfilePicture = r.ProfilePicture,
                    Role = Enums.Role.Teacher.ToString()
                })
            };
            var userToReturn = (await base.GetContextUser()).Item2;
            var examPaperSingleViewModel = new ExamPaperSingleViewModel
            {
                User = userToReturn,
                ExamPaper = examPaperToReturn,
                IsAuthor = await _examPaperManager.IsAuthorAsync(examPaper.Id, userToReturn.Id),
                IsReviewer = await _examPaperManager.IsReviewerAsync(examPaper.Id, userToReturn.Id)
            };
            return View("Single", examPaperSingleViewModel);
        }

        [CustomAuthorize(Roles = "Admin,Teacher")]
        [HttpGet("api/exam-paper/{examPaperId:int}/question-with-answer")]
        [Produces("application/json")]
        public async Task<IActionResult> GetExamPaperQuestionsWithAnswers([FromRoute] int examPaperId)
        {
            var examPaper = await _examPaperManager.GetByIdAsync(examPaperId);
            if (examPaper is null)
                return NotFound();
            var examPaperQuestionsToReturn = await _examPaperManager.GetWithFullQuestions(examPaperId);
            return Ok(examPaperQuestionsToReturn);
        }


        [RequestHeaderMatchesMediaType("Accept", "application/vnd.examonimy.exampaper.full+json")]
        [CustomAuthorize(Roles = "Admin,Teacher")]
        [HttpGet("api/exam-paper")]
        [Produces("application/vnd.examonimy.exampaper.full+json")]
        public async Task<IActionResult> GetFullExamPapers([FromQuery] RequestParamsForExamPaper requestParamsForExamPaper)
        {
            var examPapers = await _examPaperManager.GetPagedListAsync(requestParamsForExamPaper, new List<string> { "Reviewers", "Course", "Author", "Author.Role" });
            var examPapersToReturn = examPapers.Select(ep => new ExamPaperFullGetDto
            {
                StatusAsString = ep.Status.ToVietnameseString(),
                ExamPaperCode = ep.ExamPaperCode,
                Course = ep.Course!.Name,
                Author = ep.Author!.FullName,
                Status = (byte)ep.Status,
                Reviewers = ep.Reviewers!.Select(r => new UserGetDto
                {
                    ProfilePicture = r.ProfilePicture,
                    FullName = r.FullName,
                    Id = r.Id,
                    Role = r.Role!.Name
                }).ToList()
            });

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

        [RequestHeaderMatchesMediaType("Accept", "application/json")]
        [CustomAuthorize(Roles = "Admin,Teacher")]
        [HttpGet("api/exam-paper")]
        [Produces("application/json")]
        public async Task<IActionResult> GetRange([FromQuery] RequestParamsForExamPaper requestParamsForExamPaper)
        {

            var examPapers = await _examPaperManager.GetPagedListAsync(requestParamsForExamPaper);
            var examPapersToReturn = examPapers.Select(ep => new ExamPaperGetDto
            {
                Id = ep.Id,
                ExamPaperCode = ep.ExamPaperCode,
                Author = ep.Author!.FullName
            });
            return Ok(examPapersToReturn);
        }

        [CustomAuthorize(Roles = "Administrator,Teacher")]
        [HttpGet("api/exam-paper/{id}", Name = "GetExamPaperById")]
        [Produces("application/json")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {           
            var examPaper = await _examPaperManager.GetAsync(ep => ep.Id == id, new List<string> { "Author" });
            if (examPaper is null)
                return NotFound();
            var examPaperToReturn = new ExamPaperGetDto
            {
                Id = examPaper.Id,
                ExamPaperCode = examPaper.ExamPaperCode,
                Author = examPaper.Author!.FullName
            };           
            
            return Ok(examPaperToReturn);
        }

        [CustomAuthorize(Roles = "Administrator,Teacher")]
        [HttpGet("exam-paper/create")]
        public async Task<IActionResult> RenderCreateView()
        {
            
            var viewModel = new AuthorizedViewModel { User = (await base.GetContextUser()).Item2 };
            return View("Create", viewModel);
        }

        [CustomAuthorize(Roles = "Administrator,Teacher")]
        [HttpPost("api/exam-paper")]
        [Consumes("application/json")]
        [Produces("application/json", "application/problem+json")]
        public async Task<IActionResult> Create([FromBody] ExamPaperCreateDto examPaperCreateDto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var contextUser = (await base.GetContextUser()).Item1;
            var examPaperToCreate = new ExamPaper
            {
                ExamPaperCode = examPaperCreateDto.ExamPaperCode,
                CourseId = examPaperCreateDto.CourseId,
                AuthorId = contextUser.Id
            };
            await _examPaperManager.CreateAsync(examPaperToCreate);
            var examPaperToReturn = new ExamPaperGetDto
            {
                Id = examPaperToCreate.Id,
                ExamPaperCode = examPaperToCreate.ExamPaperCode,
                Author = contextUser.FullName
            };
            
            return CreatedAtRoute("GetExamPaperById", new { id = examPaperToReturn.Id }, examPaperToReturn);
        }

        [CustomAuthorize(Roles = "Administrator,Teacher")]
        [HttpGet("exam-paper/edit/{id:int}")]
        public async Task<IActionResult> RenderEditView([FromRoute] int id)
        {
            var examPaper = await _examPaperManager.GetAsync(ep => ep.Id == id, new List<string> { "Course", "Author" });
            if (examPaper is null)
                return NotFound();
            var (user, userToReturn) = await base.GetContextUser();
            
            if (!await _examPaperManager.IsAuthorAsync(id, user.Id))
                return Forbid();
            return View("Edit", new ExamPaperEditViewModel 
            {
                ExamPaperId = examPaper.Id,
                CourseId = examPaper.CourseId,
                User = userToReturn,
                ExamPaperCode = examPaper.ExamPaperCode,
                CourseName = examPaper.Course!.Name,
                AuthorName = examPaper.Author!.FullName
            });
        }

        [CustomAuthorize(Roles = "Administrator,Teacher")]
        [HttpDelete("api/exam-paper/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var examPaper = await _examPaperManager.GetByIdAsync(id);
            if (examPaper is null)
                return NotFound();
            var contextUser = (await base.GetContextUser()).Item1;
            if (examPaper.AuthorId != contextUser.Id)
                return Forbid();
            await _examPaperManager.DeleteAsync(id);
            await _notificationService.DeleteNotificationsAsync(examPaper.Id, new List<Operation> { Operation.AskForReviewForExamPaper, Operation.CommentExamPaper, Operation.ApproveExamPaper, Operation.RejectExamPaper });
            return NoContent();
        }

        [CustomAuthorize(Roles = "Administrator,Teacher")]
        [HttpPut("api/exam-paper/{id:int}")]
        [Consumes("application/json")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] ExamPaperUpdateDto examPaperUpdateDto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var examPaper = await _examPaperManager.GetByIdAsync(id);
            if (examPaper is null)
                return NotFound();
            var contextUser = (await base.GetContextUser()).Item1;
            if (examPaper.AuthorId != contextUser.Id)
                return Forbid();
            var examPaperQuestionsToUpdate = examPaperUpdateDto.ExamPaperQuestions
                .Select(e => new ExamPaperQuestion { ExamPaperId = examPaper.Id, Number = e.Number, QuestionId = e.QuestionId })
                .Select(e => 
                {
                    e.ExamPaperId = examPaper.Id;
                    return e;
                })
                .ToList();
            await _examPaperManager.UpdateAsync(examPaper.Id, examPaperQuestionsToUpdate, examPaperUpdateDto.CommitMessage);
            await _notificationService.NotifyAboutEditedExamPaperAsync(examPaper.Id, examPaperUpdateDto.CommitMessage);
            return NoContent();
        }

        [CustomAuthorize(Roles = "Administrator,Teacher")]
        [HttpPost("api/exam-paper/{id:int}/reviewer")]
        [Consumes("application/json")]
        public async Task<IActionResult> AddReviewers([FromRoute] int id, [FromBody] ExamPaperReviewerCreateDto examPaperReviewerCreateDto)
        {
            var examPaper = await _examPaperManager.GetByIdAsync(id);
            if (examPaper is null)
                return NotFound();
            var contextUser = (await base.GetContextUser()).Item1;
            if (examPaper.AuthorId != contextUser.Id)
                return Forbid();
            var examPaperReviewers = examPaperReviewerCreateDto.ReviewerIds.Select(id => new ExamPaperReviewer { ExamPaperId = examPaper.Id, ReviewerId = id }).ToList();
            await _examPaperManager.AddReviewersAsync(examPaper.Id, examPaperReviewers);
            await _notificationService.NotifyUponAddingReviewersAsync(examPaper.Id, examPaperReviewers, contextUser.Id);

            return Accepted();
        }

        [CustomAuthorize(Roles = "Admin,Teacher")]
        [HttpGet("api/exam-paper/{id:int}/review-history")]
        [Produces("application/json")]
        public async Task<IActionResult> GetReviewHistories([FromRoute] int id)
        {
            var examPaper = await _examPaperManager.GetByIdAsync(id);
            if (examPaper is null)
                return NotFound();
            var histories = await _examPaperManager.GetReviewHistories(id);
            return Ok(histories);
        }

        [CustomAuthorize(Roles = "Teacher")]
        [HttpPost("api/exam-paper/{id:int}/review/comment")]
        [Consumes("application/json")]
        public async Task<IActionResult> CommentOnExamPaperReview([FromRoute] int id, [FromBody] ExamPaperReviewCommentCreateDto comment)
        {
            var examPaper = await _examPaperManager.GetByIdAsync(id);
            if (examPaper is null) return NotFound();
            var contextUser = (await base.GetContextUser()).Item1;
            var isAuthor = await _examPaperManager.IsAuthorAsync(id, contextUser.Id);
            if (!isAuthor && !await _examPaperManager.IsReviewerAsync(id, contextUser.Id)) return Forbid();
            var commentToReturn = await _examPaperManager.CommentOnExamPaperReviewAsync(id, comment.Comment, contextUser);
            // send notification to examPaperAuthor if the contextUser is not him
            await _notificationService.NotifyAboutExamPaperCommentAsync(examPaper.Id, contextUser.Id, examPaper.AuthorId, comment.Comment);

            return Created("", commentToReturn);
        }

        [CustomAuthorize(Roles = "Teacher")]
        [HttpPut("api/exam-paper/{id:int}/review/approve")]
        public async Task<IActionResult> ApproveExamPaperReview([FromRoute] int id)
        {
            var examPaper = await _examPaperManager.GetByIdAsync(id);
            if (examPaper is null) return NotFound();
            var contextUser = (await base.GetContextUser()).Item1;
            if (!await _examPaperManager.IsReviewerAsync(id, contextUser.Id)) return Forbid();
            await _examPaperManager.ApproveExamPaperReviewAsync(id, contextUser.Id);
            await _notificationService.NotifyAboutApprovedExamPaperAsync(id, contextUser.Id);
            return NoContent();
        }

        [CustomAuthorize(Roles = "Teacher")]
        [HttpPut("api/exam-paper/{id:int}/review/reject")]
        public async Task<IActionResult> RejectExamPaperReview([FromRoute] int id)
        {
            var examPaper = await _examPaperManager.GetByIdAsync(id);
            if (examPaper is null) return NotFound();
            var contextUser = (await base.GetContextUser()).Item1;
            if (!await _examPaperManager.IsReviewerAsync(id, contextUser.Id)) return Forbid();
            await _examPaperManager.RejectExamPaperReviewAsync(id, contextUser.Id);
            await _notificationService.NotifyAboutRejectedExamPaperAsync(id, contextUser.Id);
            return NoContent();
        }
    }
}