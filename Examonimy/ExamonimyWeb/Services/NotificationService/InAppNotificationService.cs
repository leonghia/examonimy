using ExamonimyWeb.Entities;
using ExamonimyWeb.Managers.ExamPaperManager;
using ExamonimyWeb.Repositories.GenericRepository;
using ExamonimyWeb.Utilities;
using ExamonimyWeb.Utilities.NotificationMessages;
using System.Runtime.CompilerServices;

namespace ExamonimyWeb.Services.NotificationService
{
    public class InAppNotificationService : INotificationService
    {
        private readonly IExamPaperManager _examPaperManager;
        private readonly IGenericRepository<Notification> _notificationRepository;
        private readonly IGenericRepository<NotificationReceiver> _notificationReceiverRepository;
        private readonly IGenericRepository<User> _userRepository;      
       

        public InAppNotificationService(IExamPaperManager examPaperManager, IGenericRepository<Notification> notificationRepository, IGenericRepository<NotificationReceiver> notificationReceiverRepository, IGenericRepository<User> userRepository)
        {
            _examPaperManager = examPaperManager;
            _notificationRepository = notificationRepository;
            _notificationReceiverRepository = notificationReceiverRepository;
            _userRepository = userRepository;          
        }

        public async Task<string> GetHrefAsync(Notification notification)
        {
            switch (notification.NotificationTypeId)
            {
                case NotificationTypeIds.AskForReviewForExamPaper:
                    var examPaperId = await _examPaperManager.GetExamPaperIdAsync(notification.EntityId);
                    return $"/exam-paper/{examPaperId}/review";
                default:
                    throw new SwitchExpressionException(notification.NotificationTypeId);
            }
        }

        public string GetIconMarkup(int notificationTypeId)
        {
            return notificationTypeId switch
            {
                NotificationTypeIds.AskForReviewForExamPaper => @"<div class=""absolute flex items-center justify-center w-5 h-5 ms-6 -mt-5 bg-yellow-600 border border-white rounded-full dark:border-gray-800""><svg xmlns=""http://www.w3.org/2000/svg"" viewBox=""0 0 24 24"" fill=""currentColor"" class=""w-2 h-2 text-white""><path d=""M5.625 1.5c-1.036 0-1.875.84-1.875 1.875v17.25c0 1.035.84 1.875 1.875 1.875h12.75c1.035 0 1.875-.84 1.875-1.875V12.75A3.75 3.75 0 0 0 16.5 9h-1.875a1.875 1.875 0 0 1-1.875-1.875V5.25A3.75 3.75 0 0 0 9 1.5H5.625Z"" /><path d=""M12.971 1.816A5.23 5.23 0 0 1 14.25 5.25v1.875c0 .207.168.375.375.375H16.5a5.23 5.23 0 0 1 3.434 1.279 9.768 9.768 0 0 0-6.963-6.963Z"" /></svg></div>",
                _ => throw new SwitchExpressionException(notificationTypeId)
            };
        }

        public async Task<string> GetMessageMarkupAsync(Notification notification, bool isRead)
        {
            var actor = await _userRepository.GetByIdAsync(notification.ActorId) ?? throw new ArgumentException(null, nameof(notification.ActorId));
            var actorFullName = actor.FullName;
            switch (notification.NotificationTypeId)
            {
                case NotificationTypeIds.AskForReviewForExamPaper:
                    var examPaperId = await _examPaperManager.GetExamPaperIdAsync(notification.EntityId);
                    var course = await _examPaperManager.GetCourseAsync(examPaperId) ?? throw new ArgumentException(null, nameof(notification.EntityId));      
                    return new AskForReviewForExamPaperNotiMessage(actorFullName, course.Name, isRead).ToVietnamese();
                default:
                    throw new SwitchExpressionException(notification.NotificationTypeId);
    
        
            }
        }

        public async Task<IEnumerable<NotificationReceiver>> GetNotificationsAsync(int receiverId, RequestParams requestParams)
        {
            var notificationReceivers = await _notificationReceiverRepository.GetPagedListAsync(requestParams, null, nR => nR.ReceiverId == receiverId, new List<string> { "Notification", "Notification.Actor" }, q => q.OrderByDescending(nR => nR.Notification!.CreatedAt));
            return notificationReceivers;
        }

        public async Task RequestReviewerForExamPaperAsync(int examPaperId, List<ExamPaperReviewer> examPaperReviewers, int actorId, List<int> entityIdsToDelete)
        {
            var notificationsToDelete = new List<Notification>();
            foreach (var entityId in entityIdsToDelete)
            {
                var n = await _notificationRepository.GetAsync(n => n.NotificationTypeId == NotificationTypeIds.AskForReviewForExamPaper && n.EntityId == entityId, null) ?? throw new ArgumentException(null, nameof(entityId));
                notificationsToDelete.Add(n);
            }

            var notificationIds = notificationsToDelete.Select(n => n.Id);
            var notificationReceiversToDelete = new List<NotificationReceiver>();
            foreach (var notificationId in notificationIds)
            {
                var nR = await _notificationReceiverRepository.GetAsync(nR => nR.NotificationId == notificationId, null) ?? throw new ArgumentException(null, nameof(notificationId));
                notificationReceiversToDelete.Add(nR);
            }

            _notificationReceiverRepository.DeleteRange(notificationReceiversToDelete);
            _notificationRepository.DeleteRange(notificationsToDelete);

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
