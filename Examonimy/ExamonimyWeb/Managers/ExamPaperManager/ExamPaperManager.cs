﻿using ExamonimyWeb.DTOs.ExamPaperDTO;
using ExamonimyWeb.Entities;
using ExamonimyWeb.Enums;
using ExamonimyWeb.Managers.QuestionManager;
using ExamonimyWeb.Repositories.GenericRepository;
using ExamonimyWeb.Utilities;
using LinqKit;

namespace ExamonimyWeb.Managers.ExamPaperManager
{
    public class ExamPaperManager : IExamPaperManager
    {
        private readonly IGenericRepository<ExamPaperQuestion> _examPaperQuestionRepository;      
        private readonly IQuestionManager _questionManager;
        private readonly IGenericRepository<ExamPaper> _examPaperRepository;
        private readonly IGenericRepository<ExamPaperReviewer> _examPaperReviewerRepository;
        private readonly IGenericRepository<ExamPaperComment> _examPaperCommentRepository;
        private readonly IGenericRepository<ExamPaperReviewHistory> _examPaperReviewHistoryRepository;

        public ExamPaperManager(IGenericRepository<ExamPaperQuestion> examPaperQuestionRepository, IQuestionManager questionManager, IGenericRepository<ExamPaper> examPaperRepository, IGenericRepository<ExamPaperReviewer> examPaperReviewerRepository, IGenericRepository<ExamPaperComment> examPaperCommentRepository, IGenericRepository<ExamPaperReviewHistory> examPaperReviewHistoryRepository)
        {
            _examPaperQuestionRepository = examPaperQuestionRepository;         
            _questionManager = questionManager;
            _examPaperRepository = examPaperRepository;
            _examPaperReviewerRepository = examPaperReviewerRepository;
            _examPaperCommentRepository = examPaperCommentRepository;
            _examPaperReviewHistoryRepository = examPaperReviewHistoryRepository;
        }

        public async Task AddReviewersThenSaveAsync(int examPaperId, List<ExamPaperReviewer> examPaperReviewers)
        {
            _examPaperReviewerRepository.DeleteRange(ePR => ePR.ExamPaperId == examPaperId);
            await _examPaperReviewerRepository.InsertRangeAsync(examPaperReviewers);
            await _examPaperReviewerRepository.SaveAsync();

            var examPaper = await _examPaperRepository.GetSingleByIdAsync(examPaperId) ?? throw new ArgumentException(null, nameof(examPaperId));
            var authorId = examPaper.AuthorId;

            // add to the review history
            var eprh = examPaperReviewers.Select(epr => new ExamPaperReviewHistory
            {
                OperationId = (int)Operation.AskForReviewForExamPaper,
                ExamPaperId = examPaperId,
                EntityId = epr.Id,
                CreatedAt = DateTime.UtcNow,
                ActorId = authorId
            }).ToList();
            await _examPaperReviewHistoryRepository.InsertRangeAsync(eprh);
            await _examPaperReviewHistoryRepository.SaveAsync();
        }

        public async Task AddThenSaveAsync(ExamPaper examPaper)
        {
            await _examPaperRepository.InsertAsync(examPaper);
            await _examPaperRepository.SaveAsync();

            // add to the review history
            await _examPaperReviewHistoryRepository.InsertAsync(new ExamPaperReviewHistory
            {
                ExamPaperId = examPaper.Id,
                ActorId = examPaper.AuthorId,
                OperationId = (int)Operation.CreateExamPaper,
                EntityId = examPaper.Id,
                CreatedAt = DateTime.UtcNow
            });
            await _examPaperReviewHistoryRepository.SaveAsync();
        }

        public async Task<int> CountNumbersOfQuestions(int examPaperId)
        {
            return await _examPaperQuestionRepository.CountAsync(examPaperQuestion => examPaperQuestion.ExamPaperId == examPaperId);
        }

        public async Task DeleteThenSaveAsync(int examPaperId)
        {
            // Delete all the related examPaperQuestions
            _examPaperQuestionRepository.DeleteRange(ePQ => ePQ.ExamPaperId == examPaperId);
            await _examPaperQuestionRepository.SaveAsync();

            // Delete all the related examPaperReviewers
            _examPaperReviewerRepository.DeleteRange(ePR => ePR.ExamPaperId == examPaperId);
            await _examPaperReviewerRepository.SaveAsync();

            // Delete all the related examPaperComments
            _examPaperCommentRepository.DeleteRange(epc => epc.ExamPaperId == examPaperId);
            await _examPaperCommentRepository.SaveAsync();

            // Delete all the related examPaperReviewHistories
            _examPaperReviewHistoryRepository.DeleteRange(eprh => eprh.ExamPaperId == examPaperId);
            await _examPaperReviewHistoryRepository.SaveAsync();

            // Delete the examPaper itself         
            await _examPaperRepository.DeleteAsync(examPaperId);
            await _examPaperRepository.SaveAsync();
        }

        public async Task<ExamPaper?> GetByIdAsync(int examPaperId)
        {
            return await _examPaperRepository.GetSingleAsync(eP => eP.Id == examPaperId, new List<string> { "Author", "Course" });
        }

        public async Task<Course> GetCourseAsync(int examPaperId)
        {
            var examPaper = await _examPaperRepository.GetSingleAsync(eP => eP.Id == examPaperId, new List<string> { "Course" }) ?? throw new ArgumentException(null, nameof(examPaperId));
            return examPaper.Course!;
        }

        public async Task<ExamPaperQuestion?> GetExamPaperQuestionAsync(int examPaperQuestionId)
        {
            return await _examPaperQuestionRepository.GetSingleByIdAsync(examPaperQuestionId);
        }

        public async Task<IEnumerable<ExamPaperQuestionGetDto>> GetExamPaperQuestionsWithAnswersAsync(int examPaperId)
        {
            var examPaperQuestionsToReturn = new List<ExamPaperQuestionGetDto>();
            var examPaperQuestions = await _examPaperQuestionRepository.GetAsync(ePQ => ePQ.ExamPaperId == examPaperId, new List<string> { "ExamPaperQuestionComments", "ExamPaperQuestionComments.Commenter" });
            foreach (var examPaperQuestion in examPaperQuestions)
            {
                var question = await _questionManager.GetSingleByIdAsync(examPaperQuestion.QuestionId) ?? throw new ArgumentException(null, nameof(examPaperId));
                var questionToReturn = await _questionManager.GetSpecificQuestionWithAnswerDtoAsync(question.Id);
                examPaperQuestionsToReturn.Add(new ExamPaperQuestionGetDto
                {
                    Id = examPaperQuestion.Id,
                    Number = examPaperQuestion.Number,
                    Question = questionToReturn                  
                });
            }           
            examPaperQuestionsToReturn.Sort((a, b) => a.Number.CompareTo(b.Number));
            return examPaperQuestionsToReturn;
        }

        public async Task<PagedList<ExamPaper>> GetPagedListAsync(RequestParamsForExamPaper requestParamsForExamPaper)
        {
            var predicate = PredicateBuilder.New<ExamPaper>(true);
            if (requestParamsForExamPaper.SearchQuery is not null)
            {
                predicate = predicate.And(eP => eP.ExamPaperCode.ToUpper().Contains(requestParamsForExamPaper.SearchQuery.ToUpper()));
            }
            
            if (requestParamsForExamPaper.CourseId is not null && requestParamsForExamPaper.CourseId > 0)
            {
                predicate = predicate.And(eP => eP.CourseId == requestParamsForExamPaper.CourseId);
            }
            if (requestParamsForExamPaper.Status is not null)
            {
                predicate = predicate.And(eP => eP.Status == requestParamsForExamPaper.Status);
            }
            return await _examPaperRepository.GetPagedListAsync(requestParamsForExamPaper, predicate, new List<string> { "Author", "Course", "Reviewers" });           
        }

        public async Task<int> GetReviewerIdAsync(int examPaperReviewerId)
        {
            var examPaperReviewer = await _examPaperReviewerRepository.GetSingleByIdAsync(examPaperReviewerId) ?? throw new ArgumentException(null, nameof(examPaperReviewerId));
            return examPaperReviewer.ReviewerId;
        }

        public async Task<List<ExamPaperReviewHistory>> GetReviewHistories(int examPaperId)
        {
            var examPaperReviewHistories = await _examPaperReviewHistoryRepository.GetAsync(e => e.ExamPaperId == examPaperId);

            foreach (var h in examPaperReviewHistories)
            {
                
            }
        }

        public async Task<bool> IsAuthorAsync(int examPaperId, int userId)
        {
            var examPaper = await _examPaperRepository.GetSingleAsync(eP => eP.Id == examPaperId, new List<string> { "Author" }) ?? throw new ArgumentException(null, nameof(examPaperId));                       
            return examPaper.Author!.Id == userId;
        }

        public async Task<bool> IsReviewerAsync(int examPaperId, int userId)
        {
            var examPaperReviewers = await _examPaperReviewerRepository.GetAsync(ePR => ePR.ExamPaperId == examPaperId);
            var reviewerIds = examPaperReviewers.Select(ePR => ePR.ReviewerId);
            return reviewerIds.Contains(userId);
        }

        public async Task UpdateThenSaveAsync(int examPaperId, List<ExamPaperQuestion> examPaperQuestionsToUpdate)
        {
            _examPaperQuestionRepository.DeleteRange(ePQ => ePQ.ExamPaperId == examPaperId);
            await _examPaperQuestionRepository.InsertRangeAsync(examPaperQuestionsToUpdate);
            await _examPaperQuestionRepository.SaveAsync();
        }
    }
}
