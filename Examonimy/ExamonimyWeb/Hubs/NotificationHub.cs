using ExamonimyWeb.DTOs.NotificationDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace ExamonimyWeb.Hubs;

[Authorize]
public class NotificationHub : Hub<INotificationClient>
{
    public async Task SendNotification(NotificationGetDto notification, List<string> userIds)
    {
        await Clients.Users(userIds).ReceiveNotification(notification);
    }
}
