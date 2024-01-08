using ExamonimyWeb.DTOs.CourseDTO;
using ExamonimyWeb.DTOs.UserDTO;
using ExamonimyWeb.Enums;

namespace ExamonimyWeb.DTOs.ExamPaperDTO
{
    public class ExamPaperFullGetDto
    {
       
        public int Id { get; set; }
        
        public required string ExamPaperCode { get; set; }
        
        public required string Course { get; set; }     
        
        public required string Author { get; set; }   
        
        public required string StatusAsString { get; set; }
        public required byte Status { get; set; }
        public required IEnumerable<UserGetDto> Reviewers { get; set; }
    }
}
