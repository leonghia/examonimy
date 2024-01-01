using ExamonimyWeb.Entities;
using ExamonimyWeb.Enums;
using ExamonimyWeb.Utilities;

namespace ExamonimyWeb.Services.NotificationService
{
    public interface INotificationService
    {
        Task RequestReviewerForExamPaperAsync(int examPaperId, List<ExamPaperReviewer> examPaperReviewers, int actorId);
        Task<string> GetMessageMarkupAsync(Notification notification, bool isRead);
        string GetIconMarkup(Operation operation);
        string GetHref(Notification notification);
        Task<IEnumerable<NotificationReceiver>> GetNotificationsAsync(int receiverId, RequestParams requestParams);
        Task DeleteThenSaveAsync(int entityId, List<Operation> operations);
        Task CommentOnExamPaperReviewAsync(int examPaperId, int commenterId, int examPaperAuthorId, string comment);
        Task EditExamPaperAsync(int examPaperId, string commitMessage);
    }
}
