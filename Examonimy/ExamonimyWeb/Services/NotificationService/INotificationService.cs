using ExamonimyWeb.Entities;

namespace ExamonimyWeb.Services.NotificationService
{
    public interface INotificationService
    {
        Task RequestReviewerForExamPaper(List<ExamPaperReviewer> examPaperReviewers, int actorId);
    }
}
