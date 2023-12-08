using ExamonimyWeb.DTOs.CourseDTO;
using ExamonimyWeb.DTOs.ExamPaperQuestionDTO;
using ExamonimyWeb.DTOs.QuestionDTO;
using ExamonimyWeb.DTOs.UserDTO;

namespace ExamonimyWeb.DTOs.ExamPaperDTO
{
    public class ExamPaperGetDto
    {
       
        public int Id { get; set; }

        
        public required string ExamPaperCode { get; set; }

        
        public required CourseGetDto Course { get; set; }

        
        public byte TimeAllowedInMinutes { get; set; }

        
        public required UserGetDto Author { get; set; }

        public required int NumbersOfQuestions { get; set; }

        public ICollection<QuestionGetDto>? Questions { get; set; }
        public ICollection<ExamPaperQuestionGetDto>? ExamPaperQuestions { get; set; }
    }
}
