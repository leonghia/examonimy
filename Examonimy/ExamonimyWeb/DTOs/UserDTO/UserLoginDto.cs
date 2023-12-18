using ExamonimyWeb.Utilities;
using System.ComponentModel.DataAnnotations;

namespace ExamonimyWeb.DTOs.UserDTO
{
    public class UserLoginDto
    {
        [Required(ErrorMessage = $"Email {ErrorMessages.RequiredMessage}.")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = $"Email {ErrorMessages.ValidTypeMessage}.")]
        [StringLength(320, ErrorMessage = "Email phải chứa không quá 320 ký tự.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = $"Mật khẩu {ErrorMessages.RequiredMessage}.")]
        [DataType(DataType.Password)]
        [StringLength(64, ErrorMessage = "Mật khẩu phải chứa từ 6 đến 64 ký tự.", MinimumLength = 6)]
        public required string Password { get; set; }

        [Required]
        public required bool RememberMe { get; set; } = false;
    }
}
