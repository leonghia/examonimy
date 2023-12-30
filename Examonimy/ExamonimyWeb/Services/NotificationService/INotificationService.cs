using ExamonimyWeb.Entities;
using ExamonimyWeb.Utilities;

namespace ExamonimyWeb.Services.NotificationService
{
    public interface INotificationService
    {
        Task RequestReviewerForExamPaperAsync(int examPaperId, List<ExamPaperReviewer> examPaperReviewers, int actorId);
        Task<string> GetMessageMarkupAsync(Notification notification, bool isRead);
        string GetIconMarkup(int notificationTypeId);
        string GetHref(Notification notification);
        Task<IEnumerable<NotificationReceiver>> GetNotificationsAsync(int receiverId, RequestParams requestParams);
        Task DeleteThenSaveAsync(int entityId, List<int> notificationTypeIds);        
    }
}
