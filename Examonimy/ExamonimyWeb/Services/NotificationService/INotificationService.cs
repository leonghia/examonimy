using ExamonimyWeb.DTOs.NotificationDTO;
using ExamonimyWeb.Entities;
using ExamonimyWeb.Enums;
using ExamonimyWeb.Utilities;

namespace ExamonimyWeb.Services.NotificationService
{
    public interface INotificationService
    {
        Task NotifyUponAddingReviewersAsync(int examPaperId, List<ExamPaperReviewer> examPaperReviewers, int actorId);        
        Task<IEnumerable<NotificationGetDto>> GetNotificationsAsync(int receiverId, RequestParams requestParams);
        Task DeleteNotificationsAsync(int entityId, List<Operation> operations);
        Task NotifyAboutExamPaperCommentAsync(int examPaperId, int commenterId, int examPaperAuthorId, string comment);
        Task NotifyAboutEditedExamPaperAsync(int examPaperId, string commitMessage);
        Task NotifyAboutApprovedExamPaperAsync(int examPaperId, int reviewerId);
        Task NotifyAboutRejectedExamPaperAsync(int examPaperId, int reviewerId);
        Task NotifyAboutUpcomingExamAsync(int teacherId, int examId, List<int> mainClassIds);
        Task NotifyAboutChangedExamSchedule(int examId, int actorId);
    }
}
