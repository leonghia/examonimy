﻿using System.ComponentModel.DataAnnotations;
using ExamonimyWeb.Utilities;

namespace ExamonimyWeb.DTOs.UserDTO
{
    public class UserRegisterDto
    {

        [Required(ErrorMessage = $"Họ tên {ErrorMessages.RequiredMessage}.")]
        [StringLength(55, ErrorMessage = "Họ tên phải chứa từ 3 đến 55 ký tự.", MinimumLength = 3)]
        public required string FullName { get; set; }

        [Required(ErrorMessage = $"Tên đăng nhập {ErrorMessages.RequiredMessage}.")]
        [StringLength(20, ErrorMessage = "Tên đăng nhập phải chứa từ 3 đến 20 ký tự.", MinimumLength = 3)]
        public required string Username { get; set; }

        [Required(ErrorMessage = $"Email {ErrorMessages.RequiredMessage}.")]
        [DataType(DataType.EmailAddress)]
        [StringLength(320, ErrorMessage = "Email phải chứa không quá 320 ký tự.")]
        [EmailAddress(ErrorMessage = $"Email {ErrorMessages.ValidTypeMessage}.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = $"Mật khẩu {ErrorMessages.RequiredMessage}.")]
        [DataType(DataType.Password)]
        [StringLength(64, ErrorMessage = "Mật khẩu phải chứa từ 6 đến 64 ký tự.", MinimumLength = 6)]
        public required string Password { get; set; }


    }
}
