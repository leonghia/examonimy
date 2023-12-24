using ExamonimyWeb.DTOs.ExamPaperDTO;
using ExamonimyWeb.Entities;

namespace ExamonimyWeb.Managers.ExamPaperManager
{
    public interface IExamPaperManager
    {
        Task<IEnumerable<ExamPaperQuestionGetDto>> GetExamPaperQuestionsAsync(int examPaperId);
        Task<bool> IsAuthorAsync(int examPaperId, int userId);
        Task UpdateThenSaveAsync(int examPaperId, List<ExamPaperQuestion> examPaperQuestionsToUpdate);
        Task AddReviewersThenSaveAsync(int examPaperId, List<ExamPaperReviewer> examPaperReviewers);
        Task<int> GetReviewerIdAsync(int examPaperReviewerId);
    }
}
