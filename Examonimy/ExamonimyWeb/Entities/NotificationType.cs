using System.ComponentModel.DataAnnotations;
using ExamonimyWeb.Enums;

namespace ExamonimyWeb.Entities
{
    public class NotificationType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required Entity Entity { get; set; }

        [Required]
        public required Operation Operation { get; set; }
    }
}
