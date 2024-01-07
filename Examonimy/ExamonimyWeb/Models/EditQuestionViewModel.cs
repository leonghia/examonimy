using ExamonimyWeb.DTOs.CourseDTO;
using ExamonimyWeb.DTOs.QuestionDTO;

namespace ExamonimyWeb.Models
{
    public class EditQuestionViewModel : AuthorizedViewModel
    {      
        public required IEnumerable<QuestionTypeGetDto> QuestionTypes { get; set; }
        
        public required QuestionGetDto Question { get; set; }
    }
}
