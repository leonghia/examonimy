using ExamonimyWeb.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ExamonimyWeb.DTOs.UserDTO
{
    public class UserRegisterDto
    {     

        [Required]
        [StringLength(255, MinimumLength = 3)]       
        public required string FullName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]      
        public required string Username { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [StringLength(320)]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(64, MinimumLength = 6)]       
        public required string Password { get; set; }
    }
}
