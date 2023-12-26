using ExamonimyWeb.Entities;
using ExamonimyWeb.Utilities;

namespace ExamonimyWeb.Services.NotificationService
{
    public interface INotificationService
    {
        Task RequestReviewerForExamPaperAsync(List<ExamPaperReviewer> examPaperReviewers, int actorId);
        Task<string> GetMessageMarkupAsync(Notification notification);
        string GetIconMarkup(int notificationTypeId);
        string GetDateTimeAgoMarkup(DateTime dateTime); 
        Task<string> GetHrefAsync(Notification notification);
        Task<IEnumerable<Notification>> GetNotificationsAsync(int receiverId, RequestParams requestParams);
    }
}
