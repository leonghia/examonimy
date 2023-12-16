using ExamonimyWeb.DTOs.QuestionDTO;

namespace ExamonimyWeb.Models
{
    public class MultipleChoiceQuestionWithOneCorrectAnswerViewModel : QuestionViewModel
    {
        public required MultipleChoiceQuestionWithOneCorrectAnswerGetDto SpecificDetail { get; set; }
    }
}
