using ExamonimyWeb.DTOs.CourseDTO;
using ExamonimyWeb.DTOs.UserDTO;
using ExamonimyWeb.Utilities;

namespace ExamonimyWeb.DTOs.ExamPaperDTO
{
    public class ExamPaperGetDto
    {
       
        public int Id { get; set; }
        
        public required string ExamPaperCode { get; set; }
        
        public required CourseGetDto Course { get; set; }     
        
        public required UserGetDto Author { get; set; }

        public required int NumbersOfQuestion { get; set; }   
        
        public required string StatusAsString { get; set; }
        public ExamPaperStatus? Status { get; set; }
        public ICollection<UserGetDto>? Reviewers { get; set; }
    }
}
