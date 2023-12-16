using ExamonimyWeb.DTOs.QuestionDTO;

namespace ExamonimyWeb.Models
{
    public class ShortAnswerQuestionViewModel : QuestionViewModel
    {
        public required ShortAnswerQuestionGetDto SpecificDetail { get; set; }
    }
}
