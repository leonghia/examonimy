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
        [StringLength(320)]
        public required string Email { get; set; }

        [Required]
        [StringLength(255)]
        public required string PasswordHash { get; set; }

        [Required]
        [StringLength(255)]
        public required string PasswordSalt { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public required string NormalizedUsername { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [StringLength(320)]
        public required string NormalizedEmail { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DateOfBirth { get; set; }

        [Required]
        [ForeignKey("Role")]
        public required int RoleId { get; set; }
        public required Role Role { get; set; }
    }
}
