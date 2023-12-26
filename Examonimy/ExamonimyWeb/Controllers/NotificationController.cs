using AutoMapper;
using ExamonimyWeb.Attributes;
using ExamonimyWeb.DTOs.NotificationDTO;
using ExamonimyWeb.Entities;
using ExamonimyWeb.Managers.UserManager;
using ExamonimyWeb.Repositories.GenericRepository;
using ExamonimyWeb.Services.NotificationService;
using ExamonimyWeb.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace ExamonimyWeb.Controllers
{
    [Route("")]
    public class NotificationController : GenericController<Notification>
    {          
        private readonly INotificationService _notificationService;       
        private const int _defaultTimezoneOffset = 0;
        private const string _timezoneOffsetRequestHeaderKey = "TimezoneOffset";
        private const int _offsetMultiplier = -1;

        public NotificationController(IMapper mapper, IGenericRepository<Notification> notificationRepository, IUserManager userManager, INotificationService notificationService) : base(mapper, notificationRepository, userManager)
        {                    
            _notificationService = notificationService;           
        }

        [CustomAuthorize]
        [HttpGet("api/notification")]
        [Produces("application/json")]
        public async Task<IActionResult> Get([FromQuery] RequestParams requestParams)
        {
            var contextUser = await base.GetContextUser();
            var notifications = await _notificationService.GetNotificationsAsync(contextUser.Id, requestParams);
            var notificationsToReturn = new List<NotificationGetDto>();
            foreach (var notification in notifications)
            {
                var str = Request.Headers[_timezoneOffsetRequestHeaderKey];
                DateTime createdAt;
                if (StringValues.IsNullOrEmpty(str))
                    createdAt = notification.CreatedAt.AddMinutes(Convert.ToDouble(_defaultTimezoneOffset) * _offsetMultiplier);
                else
                    createdAt = notification.CreatedAt.AddMinutes(Convert.ToDouble(int.Parse(str!)) * _offsetMultiplier);
                notificationsToReturn.Add(new NotificationGetDto
                {
                    Id = notification.Id,
                    MessageMarkup = await _notificationService.GetMessageMarkupAsync(notification),
                    ActorProfilePicture = contextUser.ProfilePicture,                   
                    Href = await _notificationService.GetHrefAsync(notification),                   
                    IconMarkup = _notificationService.GetIconMarkup(notification.NotificationTypeId),
                    DateTimeAgoMarkup = _notificationService.GetDateTimeAgoMarkup(notification.CreatedAt)
                });
            }
            return Ok(notificationsToReturn);
        }
    }
}
