using ExamonimyWeb.DTOs.NotificationDTO;

namespace ExamonimyWeb.Hubs;

public interface INotificationClient
{
    Task ReceiveNotification(NotificationGetDto notification);
}
