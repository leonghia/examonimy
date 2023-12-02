using ExamonimyWeb.Entities;
using ExamonimyWeb.Models.DTOs.RoleDTO;

namespace ExamonimyWeb.Models.DTOs.UserDTO
{
    public class UserGetDto
    {
        public int Id { get; set; }

        public required string FullName { get; set; }

        public required string Username { get; set; }

        public required string Email { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public required RoleGetDto Role { get; set; }
    }
}
