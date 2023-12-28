using Azure.Core;
using ExamonimyWeb.DTOs.NotificationDTO;
using ExamonimyWeb.Entities;
using ExamonimyWeb.Hubs;
using ExamonimyWeb.Managers.ExamPaperManager;
using ExamonimyWeb.Repositories.GenericRepository;
using ExamonimyWeb.Utilities;
using ExamonimyWeb.Utilities.NotificationMessages;
using Microsoft.AspNetCore.SignalR;
using System.Runtime.CompilerServices;

namespace ExamonimyWeb.Services.NotificationService
{
    public class InAppNotificationService : INotificationService
    {
        private readonly IExamPaperManager _examPaperManager;
        private readonly IGenericRepository<Notification> _notificationRepository;
        private readonly IGenericRepository<NotificationReceiver> _notificationReceiverRepository;
        private readonly IGenericRepository<User> _userRepository;
        private readonly IHubContext<NotificationHub, INotificationClient> _notificationHubContext;

        public InAppNotificationService(IExamPaperManager examPaperManager, IGenericRepository<Notification> notificationRepository, IGenericRepository<NotificationReceiver> notificationReceiverRepository, IGenericRepository<User> userRepository, IHubContext<NotificationHub, INotificationClient> notificationHubContext)
        {
            _examPaperManager = examPaperManager;
            _notificationRepository = notificationRepository;
            _notificationReceiverRepository = notificationReceiverRepository;
            _userRepository = userRepository;
            _notificationHubContext = notificationHubContext;
        }

        public async Task DeleteThenSaveAsync(int entityId, List<int> notificationTypeIds)
        {
            var notificationsToDelete = (await _notificationRepository.GetAsync(null, n => n.EntityId == entityId && notificationTypeIds.Contains(n.NotificationTypeId))).ToList();
            var notificationsIdsToDelete = notificationsToDelete.Select(n => n.Id);
            _notificationReceiverRepository.DeleteRange(nR => notificationsIdsToDelete.Contains(nR.NotificationId));
            await _notificationReceiverRepository.SaveAsync();
            _notificationRepository.DeleteRange(notificationsToDelete);
            await _notificationRepository.SaveAsync();
        }

        public string GetHref(Notification notification)
        {
            switch (notification.NotificationTypeId)
            {
                case NotificationTypeIds.AskForReviewForExamPaper:
                    var examPaperId = notification.EntityId;
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
                    var examPaperId = notification.EntityId;
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

        public async Task RequestReviewerForExamPaperAsync(int examPaperId, List<ExamPaperReviewer> examPaperReviewers, int actorId)
        {
          
            var notification = await _notificationRepository.GetAsync(n => n.NotificationTypeId == NotificationTypeIds.AskForReviewForExamPaper && n.EntityId == examPaperId, null);
            // if this noti does not exist, we persist it and create its receivers
            if (notification is null)
            {
                var notificationToCreate = new Notification
                {
                    NotificationTypeId = NotificationTypeIds.AskForReviewForExamPaper,
                    EntityId = examPaperId,
                    ActorId = actorId
                };

                await _notificationRepository.InsertAsync(notificationToCreate);
                await _notificationRepository.SaveAsync();

                var notificationReceiversToCreate = examPaperReviewers.Select(ePR => new NotificationReceiver
                {
                    ReceiverId = ePR.ReviewerId,
                    NotificationId = notificationToCreate.Id
                }).ToList();

                await _notificationReceiverRepository.InsertRangeAsync(notificationReceiversToCreate);
                await _notificationReceiverRepository.SaveAsync();

                // send noti via signalr
                var ids = notificationReceiversToCreate.Select(nR => nR.ReceiverId);
                var usernames = (await _userRepository.GetAsync(null, u => ids.Contains(u.Id))).Select(u => u.Username).ToList();
                var actor = await _userRepository.GetByIdAsync(actorId) ?? throw new ArgumentException(null, nameof(actorId));
                await _notificationHubContext.Clients.Users(usernames).ReceiveNotification(new NotificationGetDto
                {
                    Id = notificationToCreate.Id,
                    MessageMarkup = await GetMessageMarkupAsync(notificationToCreate, false),
                    ActorProfilePicture = actor.ProfilePicture,
                    Href = GetHref(notificationToCreate),
                    IconMarkup = GetIconMarkup(notificationToCreate.NotificationTypeId),
                    DateTimeAgo = notificationToCreate.CreatedAt,
                    IsRead = false
                });

                return;
            }

            // else, we just need to update its receivers
            var existingReceivers = await _notificationReceiverRepository.GetAsync(null, nR => nR.NotificationId == notification.Id);
            var existingReceiverIds = existingReceivers.Select(e => e.ReceiverId);
            var receiverIds = examPaperReviewers.Select(e => e.ReviewerId);
            var receiversToAdd = receiverIds
                .Where(id => !existingReceiverIds.Contains(id))
                .Select(receiverId => new NotificationReceiver { NotificationId = notification.Id, ReceiverId = receiverId })
                .ToList();
            var receiversToDelete = existingReceivers
                .Where(r => !receiverIds.Contains(r.ReceiverId))
                .ToList();
            _notificationReceiverRepository.DeleteRange(receiversToDelete);
            await _notificationReceiverRepository.InsertRangeAsync(receiversToAdd);
            await _notificationReceiverRepository.SaveAsync();

            // send noti via signalr
        }
    }
}
