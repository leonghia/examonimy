using ExamonimyWeb.DTOs.NotificationDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace ExamonimyWeb.Hubs.NotificationHub;

[Authorize]
public class NotificationHub : Hub<INotificationClient>
{
    public async Task SendNotification(NotificationGetDto notification, List<string> usernames)
    {
        await Clients.Users(usernames).ReceiveNotification(notification);
    }
}
