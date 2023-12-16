using ExamonimyWeb.DTOs.QuestionDTO;

namespace ExamonimyWeb.Models
{
    public class TrueFalseQuestionViewModel : QuestionViewModel
    {
        public required TrueFalseQuestionGetDto SpecificDetail { get; set; }
    }
}
