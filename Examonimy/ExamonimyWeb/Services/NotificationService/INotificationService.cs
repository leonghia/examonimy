using ExamonimyWeb.Entities;
using ExamonimyWeb.Utilities;

namespace ExamonimyWeb.Services.NotificationService
{
    public interface INotificationService
    {
        Task RequestReviewerForExamPaperAsync(int examPaperId, List<ExamPaperReviewer> examPaperReviewers, int actorId, List<int> entityIdsToDelete);
        Task<string> GetMessageMarkupAsync(Notification notification, bool isRead);
        string GetIconMarkup(int notificationTypeId);
        Task<string> GetHrefAsync(Notification notification);
        Task<IEnumerable<NotificationReceiver>> GetNotificationsAsync(int receiverId, RequestParams requestParams);
    }
}
