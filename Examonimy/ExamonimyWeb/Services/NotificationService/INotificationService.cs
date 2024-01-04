using ExamonimyWeb.DTOs.NotificationDTO;
using ExamonimyWeb.Entities;
using ExamonimyWeb.Enums;
using ExamonimyWeb.Utilities;

namespace ExamonimyWeb.Services.NotificationService
{
    public interface INotificationService
    {
        Task RequestReviewerForExamPaperAsync(int examPaperId, List<ExamPaperReviewer> examPaperReviewers, int actorId);        
        Task<IEnumerable<NotificationGetDto>> GetNotificationsAsync(int receiverId, RequestParams requestParams);
        Task DeleteThenSaveAsync(int entityId, List<Operation> operations);
        Task CommentOnExamPaperReviewAsync(int examPaperId, int commenterId, int examPaperAuthorId, string comment);
        Task EditExamPaperAsync(int examPaperId, string commitMessage);
        Task ApproveExamPaperReviewAsync(int examPaperId, int reviewerId);
        Task RejectExamPaperReviewAsync(int examPaperId, int reviewerId);
        Task NotifyAboutUpcomingExamAsync(int teacherId, int examId, List<int> mainClassIds, string courseName);
    }
}
