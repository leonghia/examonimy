using AutoMapper;
using ExamonimyWeb.Attributes;
using ExamonimyWeb.DTOs.NotificationDTO;
using ExamonimyWeb.Entities;
using ExamonimyWeb.Managers.UserManager;
using ExamonimyWeb.Repositories.GenericRepository;
using ExamonimyWeb.Services.NotificationService;
using ExamonimyWeb.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace ExamonimyWeb.Controllers
{
    [Route("")]
    public class NotificationController : GenericController<Notification>
    {          
        private readonly INotificationService _notificationService;
        private readonly IGenericRepository<NotificationReceiver> _notificationReceiverRepository;        

        public NotificationController(IMapper mapper, IGenericRepository<Notification> notificationRepository, IUserManager userManager, INotificationService notificationService, IGenericRepository<NotificationReceiver> notificationReceiverRepository) : base(mapper, notificationRepository, userManager)
        {                    
            _notificationService = notificationService;
            _notificationReceiverRepository = notificationReceiverRepository;
        }

        [CustomAuthorize]
        [HttpGet("api/notification")]
        [Produces("application/json")]
        public async Task<IActionResult> Get([FromQuery] RequestParams requestParams)
        {
            var contextUser = await base.GetContextUser();
            var notifications = await _notificationService.GetNotificationsAsync(contextUser.Id, requestParams);
            var notificationsToReturn = new List<NotificationGetDto>();
            if (notifications.Any())
            {
                foreach (var notification in notifications)
                {
                    notificationsToReturn.Add(new NotificationGetDto
                    {
                        Id = notification.NotificationId,
                        MessageMarkup = await _notificationService.GetMessageMarkupAsync(notification.Notification!, notification.IsRead),
                        ActorProfilePicture = notification.Notification!.Actor!.ProfilePicture,
                        Href = _notificationService.GetHref(notification.Notification!),
                        IconMarkup = _notificationService.GetIconMarkup(notification.Notification!.NotificationTypeId),
                        NotifiedAt = notification.Notification.CreatedAt,
                        IsRead = notification.IsRead
                    });
                }
            }
            
            return Ok(notificationsToReturn);
        }

        [CustomAuthorize]
        [HttpPut("api/notification/{id:int}")]
        public async Task<IActionResult> MarkAsRead([FromRoute] int id)
        {
            var contextUser = await base.GetContextUser();
            var notificationReceiver = await _notificationReceiverRepository.GetAsync(nR => nR.NotificationId == id && nR.ReceiverId == contextUser.Id, null);
            if (notificationReceiver is null)
                return NotFound();
            notificationReceiver.IsRead = true;
            _notificationReceiverRepository.Update(notificationReceiver);
            await _notificationReceiverRepository.SaveAsync();
            return NoContent();
        }
    }
}
