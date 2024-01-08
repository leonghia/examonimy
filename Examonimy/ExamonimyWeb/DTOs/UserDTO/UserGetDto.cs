using ExamonimyWeb.Enums;

namespace ExamonimyWeb.DTOs.UserDTO;

public class UserGetDto
{
    public required int Id { get; set; }

    public required string FullName { get; set; }      
  
    public required string ProfilePicture { get; set; }
    public required string Role { get; set; }
}
