using ExamonimyWeb.DTOs.ExamPaperDTO;
using ExamonimyWeb.Entities;
using ExamonimyWeb.Utilities;

namespace ExamonimyWeb.Managers.ExamPaperManager
{
    public interface IExamPaperManager
    {
        Task<IEnumerable<ExamPaperQuestionGetDto>> GetExamPaperQuestionsWithAnswersAsync(int examPaperId);
        Task<bool> IsAuthorAsync(int examPaperId, int userId);
        Task UpdateAsync(int examPaperId, List<ExamPaperQuestion> examPaperQuestionsToUpdate);
        Task AddReviewersAsync(int examPaperId, List<ExamPaperReviewer> examPaperReviewers);
        Task<int> GetReviewerIdAsync(int examPaperReviewerId);
        Task<Course> GetCourseAsync(int examPaperId);
        Task DeleteAsync(int examPaperId);      
        Task<ExamPaper?> GetByIdAsync(int examPaperId);
        Task<PagedList<ExamPaper>> GetPagedListAsync(RequestParamsForExamPaper requestParamsForExamPaper);
        Task<int> CountNumbersOfQuestions(int examPaperId);
        Task CreateAsync(ExamPaper examPaper);
        Task<bool> IsReviewerAsync(int examPaperId, int userId);
        Task<ExamPaperQuestion?> GetExamPaperQuestionAsync(int examPaperQuestionId);
        Task<List<ExamPaperReviewHistoryGetDto>> GetReviewHistories(int examPaperId);
        Task<ExamPaperReviewHistoryCommentGetDto> CommentOnExamPaperReviewAsync(int examPaperId, string comment, User commenter);
    }
}
