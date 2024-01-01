using ExamonimyWeb.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamonimyWeb.Entities
{

    public class Notification
    {
        [Key]
        public int Id { get; set; }      

        [Required]
        public required int EntityId { get; set; }

        public string? Href { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        [ForeignKey(nameof(Actor))]
        public required int ActorId { get; set; }
        public User? Actor { get; set; }

        public ICollection<User>? Receivers { get; set; }
        public ICollection<NotificationReceiver>? NotificationReceivers { get; set; }

        [Required]
        public required Operation Operation { get; set; }

    }
}
