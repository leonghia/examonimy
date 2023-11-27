using ExamonimyWeb.Helpers;
using System.ComponentModel.DataAnnotations;

namespace ExamonimyWeb.DTOs.UserDTO
{
    public class UserLoginDto
    {
        [Required(ErrorMessage = $"Email {ErrorMessagesHelper.RequiredMessage}.")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = $"Email {ErrorMessagesHelper.ValidTypeMessage}.")]
        [StringLength(320, ErrorMessage = "Email phải chứa không quá 320 ký tự.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = $"Mật khẩu {ErrorMessagesHelper.RequiredMessage}.")]
        [DataType(DataType.Password)]
        [StringLength(64, ErrorMessage = "Mật khẩu phải chứa từ 6 đến 64 ký tự.", MinimumLength = 6)]
        public required string Password { get; set; }
    }
}
