namespace ExamonimyWeb.Entities
{
    public class NotificationReceiver
    {
        public required int NotificationId { get; set; } 

        public required int ReceiverId { get; set; }

        public bool IsRead { get; set; } = false;

        public Notification? Notification { get; set; }
        public User? Receiver { get; set; }
    }
}
