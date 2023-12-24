using ExamonimyWeb.Entities;
using ExamonimyWeb.Managers.ExamPaperManager;
using ExamonimyWeb.Repositories.GenericRepository;
using ExamonimyWeb.Utilities;

namespace ExamonimyWeb.Services.NotificationService
{
    public class InAppNotificationService : INotificationService
    {
        private readonly IExamPaperManager _examPaperManager;
        private readonly IGenericRepository<Notification> _notificationRepository;
        private readonly IGenericRepository<NotificationReceiver> _notificationReceiverRepository;

        public InAppNotificationService(IExamPaperManager examPaperManager, IGenericRepository<Notification> notificationRepository, IGenericRepository<NotificationReceiver> notificationReceiverRepository)
        {
            _examPaperManager = examPaperManager;
            _notificationRepository = notificationRepository;
            _notificationReceiverRepository = notificationReceiverRepository;
        }

        public async Task RequestReviewerForExamPaper(List<ExamPaperReviewer> examPaperReviewers, int actorId)
        {
            // create notification
            var notifications = examPaperReviewers.Select(ePR => new Notification { NotificationTypeId = NotificationTypeIds.AskForReviewForExamPaper, EntityId = ePR.Id, ActorId = actorId }).ToList();
            await _notificationRepository.InsertRangeAsync(notifications);
            await _notificationRepository.SaveAsync();

            var notificationReceivers = new List<NotificationReceiver>();
            foreach (var n in notifications)
            {
                notificationReceivers.Add(new NotificationReceiver { NotificationId = n.Id, ReceiverId = await _examPaperManager.GetReviewerIdAsync(n.EntityId) });
            }
            await _notificationReceiverRepository.InsertRangeAsync(notificationReceivers);
            await _notificationReceiverRepository.SaveAsync();
        }
    }
}
