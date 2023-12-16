using ExamonimyWeb.DTOs.QuestionDTO;

namespace ExamonimyWeb.Models
{
    public class FillInBlankQuestionViewModel : QuestionViewModel
    {
        public required FillInBlankQuestionGetDto SpecificDetail { get; set; }
    }
}
