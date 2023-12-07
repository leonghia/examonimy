using ExamonimyWeb.DTOs.CourseDTO;
using ExamonimyWeb.DTOs.UserDTO;

namespace ExamonimyWeb.DTOs.QuestionDTO
{
    public class QuestionGetDto
    {
        
        public int Id { get; set; }
          
        public required CourseGetDto Course { get; set; }
           
        public required QuestionTypeGetDto QuestionType { get; set; }
          
        public required QuestionLevelGetDto QuestionLevel { get; set; }
      
        public required string QuestionContent { get; set; }
             
        public required UserGetDto Author { get; set; }
    }
}
