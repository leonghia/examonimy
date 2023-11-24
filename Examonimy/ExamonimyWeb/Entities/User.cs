using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamonimyWeb.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 3)]
        public required string FullName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public required string Username { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public required string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(55, MinimumLength = 6)]
        public required string PasswordHash { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public required string NormalizedUsername { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public required string NormalizedEmail { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DateOfBirth { get; set; }

        [Required]
        [ForeignKey("Role")]
        public required int RoleId { get; set; }
        public required Role Role { get; set; }
    }
}
