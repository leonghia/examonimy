namespace ExamonimyWeb.DTOs.NotificationDTO
{
    public class NotificationGetDto
    {
        public required int Id { get; set; }
        public required string MessageMarkup { get; set; }
        public required string IconMarkup { get; set; }
        public required string ActorProfilePicture { get; set; }  
        public required string Href { get; set; }
        public required string DateTimeAgoMarkup { get; set; }
        public required bool IsRead { get; set; }
    }
}
