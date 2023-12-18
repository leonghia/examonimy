using ExamonimyWeb.DTOs.QuestionDTO;

namespace ExamonimyWeb.Models
{
    public class TrueFalseQuestionViewModel : QuestionViewModel
    {
        public required TrueFalseQuestionGetDto Question { get; set; }
    }
}
