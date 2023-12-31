﻿using AutoMapper;
using ExamonimyWeb.Attributes;
using ExamonimyWeb.Entities;
using ExamonimyWeb.Managers.UserManager;
using ExamonimyWeb.Repositories;
using ExamonimyWeb.Services.NotificationService;
using ExamonimyWeb.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace ExamonimyWeb.Controllers
{
    [Route("")]
    public class NotificationController : BaseController
    {          
        private readonly INotificationService _notificationService;
        private readonly IGenericRepository<NotificationReceiver> _notificationReceiverRepository;        

        public NotificationController(IMapper mapper, IUserManager userManager, INotificationService notificationService, IGenericRepository<NotificationReceiver> notificationReceiverRepository) : base(userManager)
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
            var notificationsToReturn = await _notificationService.GetNotificationsAsync(contextUser.Id, requestParams);
            
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
