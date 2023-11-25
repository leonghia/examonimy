using System.ComponentModel.DataAnnotations;

namespace ExamonimyWeb.Entities
{
    public class Role
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(25, MinimumLength = 3)]
        public required string Name { get; set; }

        public ICollection<User>? Users { get; set; }
    }
}
