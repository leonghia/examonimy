using ExamonimyWeb.DTOs.ExamPaperDTO;
using ExamonimyWeb.Entities;
using ExamonimyWeb.Managers.QuestionManager;
using ExamonimyWeb.Repositories.GenericRepository;
using ExamonimyWeb.Utilities;
using LinqKit;
using System.Linq.Expressions;

namespace ExamonimyWeb.Managers.ExamPaperManager
{
    public class ExamPaperManager : IExamPaperManager
    {
        private readonly IGenericRepository<ExamPaperQuestion> _examPaperQuestionRepository;
        private readonly IGenericRepository<Question> _questionRepository;
        private readonly IQuestionManager _questionManager;
        private readonly IGenericRepository<ExamPaper> _examPaperRepository;
        private readonly IGenericRepository<ExamPaperReviewer> _examPaperReviewerRepository;

        public ExamPaperManager(IGenericRepository<ExamPaperQuestion> examPaperQuestionRepository, IGenericRepository<Question> questionRepository, IQuestionManager questionManager, IGenericRepository<ExamPaper> examPaperRepository, IGenericRepository<ExamPaperReviewer> examPaperReviewerRepository)
        {
            _examPaperQuestionRepository = examPaperQuestionRepository;
            _questionRepository = questionRepository;
            _questionManager = questionManager;
            _examPaperRepository = examPaperRepository;
            _examPaperReviewerRepository = examPaperReviewerRepository;
        }

        public async Task AddReviewersThenSaveAsync(int examPaperId, List<ExamPaperReviewer> examPaperReviewers)
        {
            _examPaperReviewerRepository.DeleteRange(ePR => ePR.ExamPaperId == examPaperId);
            await _examPaperReviewerRepository.InsertRangeAsync(examPaperReviewers);
            await _examPaperReviewerRepository.SaveAsync();         
        }

        public async Task AddThenSaveAsync(ExamPaper examPaper)
        {
            await _examPaperRepository.InsertAsync(examPaper);
            await _examPaperRepository.SaveAsync();
        }

        public async Task<int> CountNumbersOfQuestions(int examPaperId)
        {
            return await _examPaperQuestionRepository.CountAsync(examPaperQuestion => examPaperQuestion.ExamPaperId == examPaperId);
        }

        public async Task DeleteThenSaveAsync(int examPaperId)
        {
            // Delete all related examPaperQuestions
            _examPaperQuestionRepository.DeleteRange(ePQ => ePQ.ExamPaperId == examPaperId);
            await _examPaperQuestionRepository.SaveAsync();

            // Delete the examPaper itself         
            await _examPaperRepository.DeleteAsync(examPaperId);
            await _examPaperRepository.SaveAsync();
        }

        public async Task<ExamPaper?> GetByIdAsync(int examPaperId)
        {
            return await _examPaperRepository.GetAsync(eP => eP.Id == examPaperId, new List<string> { "Author", "Course" });
        }

        public async Task<Course> GetCourseAsync(int examPaperId)
        {
            var examPaper = await _examPaperRepository.GetAsync(eP => eP.Id == examPaperId, new List<string> { "Course" }) ?? throw new ArgumentException(null, nameof(examPaperId));
            return examPaper.Course!;
        }      

        public async Task<IEnumerable<ExamPaperQuestionGetDto>> GetExamPaperQuestionsAsync(int examPaperId)
        {
            var examPaperQuestionsToReturn = new List<ExamPaperQuestionGetDto>();
            var examPaperQuestions = await _examPaperQuestionRepository.GetAsync(ePQ => ePQ.ExamPaperId == examPaperId, null, null);
            foreach (var examPaperQuestion in examPaperQuestions)
            {
                var question = await _questionRepository.GetByIdAsync(examPaperQuestion.QuestionId) ?? throw new ArgumentException(null, nameof(examPaperId));
                var questionToReturn = await _questionManager.GetSpecificQuestionWithoutAnswerDtoAsync(question.Id);
                examPaperQuestionsToReturn.Add(new ExamPaperQuestionGetDto
                {
                    Number = examPaperQuestion.Number,
                    Question = questionToReturn
                });
            }
            examPaperQuestionsToReturn.Sort((a, b) => a.Number.CompareTo(b.Number));
            return examPaperQuestionsToReturn;
        }

        public async Task<PagedList<ExamPaper>> GetPagedListAsync(RequestParamsForExamPaper requestParamsForExamPaper)
        {
            Expression<Func<ExamPaper, bool>>? searchPredicate = null;
            if (requestParamsForExamPaper.SearchQuery is not null)
            {
                searchPredicate = eP => eP.ExamPaperCode.ToUpper().Contains(requestParamsForExamPaper.SearchQuery.ToUpper());
            }
            var filterPredicate = PredicateBuilder.New<ExamPaper>(true);
            if (requestParamsForExamPaper.CourseId is not null && requestParamsForExamPaper.CourseId > 0)
            {
                filterPredicate = filterPredicate.And(eP => eP.CourseId == requestParamsForExamPaper.CourseId);
            }
            if (requestParamsForExamPaper.Status is not null)
            {
                filterPredicate = filterPredicate.And(eP => eP.Status == requestParamsForExamPaper.Status);
            }
            return await _examPaperRepository.GetPagedListAsync(requestParamsForExamPaper, searchPredicate, filterPredicate, new List<string> { "Author", "Course", "Reviewers" });           
        }

        public async Task<int> GetReviewerIdAsync(int examPaperReviewerId)
        {
            var examPaperReviewer = await _examPaperReviewerRepository.GetByIdAsync(examPaperReviewerId) ?? throw new ArgumentException(null, nameof(examPaperReviewerId));
            return examPaperReviewer.ReviewerId;
        }

        public async Task<bool> IsAuthorAsync(int examPaperId, int userId)
        {
            var examPaper = await _examPaperRepository.GetAsync(eP => eP.Id == examPaperId, new List<string> { "Author" }) ?? throw new ArgumentException(null, nameof(examPaperId));                       
            return examPaper.Author!.Id == userId;
        }

        public async Task UpdateThenSaveAsync(int examPaperId, List<ExamPaperQuestion> examPaperQuestionsToUpdate)
        {
            _examPaperQuestionRepository.DeleteRange(ePQ => ePQ.ExamPaperId == examPaperId);
            await _examPaperQuestionRepository.InsertRangeAsync(examPaperQuestionsToUpdate);
            await _examPaperQuestionRepository.SaveAsync();
        }
    }
}
