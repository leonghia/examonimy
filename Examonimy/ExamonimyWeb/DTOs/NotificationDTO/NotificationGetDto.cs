﻿namespace ExamonimyWeb.DTOs.NotificationDTO
{
    public class NotificationGetDto
    {
        public int Id { get; set; }
        public required string MessageMarkup { get; set; }
        public string? ActorProfilePicture { get; set; }  
        public required string Href { get; set; }
        public required DateTime NotifiedAt { get; set; }
        public required bool IsRead { get; set; }
        public required int Operation { get; set; }
    }
}
