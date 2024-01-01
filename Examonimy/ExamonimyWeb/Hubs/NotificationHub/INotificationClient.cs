using ExamonimyWeb.DTOs.NotificationDTO;

namespace ExamonimyWeb.Hubs.NotificationHub;

public interface INotificationClient
{
    Task ReceiveNotification(NotificationGetDto notification);
}
