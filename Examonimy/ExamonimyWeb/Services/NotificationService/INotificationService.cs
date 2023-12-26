using ExamonimyWeb.Entities;
using ExamonimyWeb.Utilities;

namespace ExamonimyWeb.Services.NotificationService
{
    public interface INotificationService
    {
        Task RequestReviewerForExamPaperAsync(List<ExamPaperReviewer> examPaperReviewers, int actorId);
        Task<string> GetMessageMarkupAsync(Notification notification, bool isRead);
        string GetIconMarkup(int notificationTypeId);
        string GetDateTimeAgoMarkup(DateTime dateTime, bool isRead); 
        Task<string> GetHrefAsync(Notification notification);
        Task<IEnumerable<NotificationReceiver>> GetNotificationsAsync(int receiverId, RequestParams requestParams);
    }
}
