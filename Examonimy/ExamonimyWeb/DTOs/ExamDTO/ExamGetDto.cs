using ExamonimyWeb.DTOs.CourseDTO;
using ExamonimyWeb.DTOs.UserDTO;

namespace ExamonimyWeb.DTOs.ExamDTO
{
    public class ExamGetDto
    {
        
        public int Id { get; set; }
      
        public required string Name { get; set; }      

        public CourseGetDto? Course { get; set; }
            
        public UserGetDto? Author { get; set; }
      
        public required int TimeAllowed { get; set; }
      
        public required DateTime From { get; set; }
       
        public required DateTime To { get; set; }
      
        public string? Description { get; set; }
    }
}
