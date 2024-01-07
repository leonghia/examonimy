using ExamonimyWeb.DTOs.RoleDTO;
using ExamonimyWeb.Entities;

namespace ExamonimyWeb.DTOs.UserDTO
{
    public class UserGetDto
    {
        public required int Id { get; set; }

        public required string FullName { get; set; }      
      
        public required string ProfilePicture { get; set; }
    }
}
