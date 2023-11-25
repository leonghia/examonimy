using System.ComponentModel.DataAnnotations;

namespace ExamonimyWeb.DTOs.UserDTO
{
    public class UserLoginDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [StringLength(320)]
        public required string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(64, MinimumLength = 6)]
        public required string Password { get; set; }
    }
}
