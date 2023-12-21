using ExamonimyWeb.DTOs.ExamPaperDTO;

namespace ExamonimyWeb.Managers.ExamPaperManager
{
    public interface IExamPaperManager
    {
        Task<IEnumerable<ExamPaperQuestionGetDto>> GetExamPaperQuestionsAsync(int examPaperId);
        Task<bool> IsAuthorAsync(int examPaperId, int userId);
    }
}
