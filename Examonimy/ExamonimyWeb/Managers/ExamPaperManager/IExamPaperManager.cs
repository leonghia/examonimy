using ExamonimyWeb.DTOs.ExamPaperDTO;
using ExamonimyWeb.Entities;
using ExamonimyWeb.Utilities;

namespace ExamonimyWeb.Managers.ExamPaperManager
{
    public interface IExamPaperManager
    {
        Task<IEnumerable<ExamPaperQuestionGetDto>> GetExamPaperQuestionsWithAnswersAsync(int examPaperId);
        Task<bool> IsAuthorAsync(int examPaperId, int userId);
        Task UpdateThenSaveAsync(int examPaperId, List<ExamPaperQuestion> examPaperQuestionsToUpdate);
        Task AddReviewersThenSaveAsync(int examPaperId, List<ExamPaperReviewer> examPaperReviewers);
        Task<int> GetReviewerIdAsync(int examPaperReviewerId);
        Task<Course> GetCourseAsync(int examPaperId);
        Task DeleteThenSaveAsync(int examPaperId);      
        Task<ExamPaper?> GetByIdAsync(int examPaperId);
        Task<PagedList<ExamPaper>> GetPagedListAsync(RequestParamsForExamPaper requestParamsForExamPaper);
        Task<int> CountNumbersOfQuestions(int examPaperId);
        Task AddThenSaveAsync(ExamPaper examPaper);
        Task<bool> IsReviewerAsync(int examPaperId, int userId);
        Task<ExamPaperQuestion?> GetExamPaperQuestionAsync(int examPaperQuestionId);
        Task<List<ExamPaperReviewHistory>> GetReviewHistories(int examPaperId);
    }
}
