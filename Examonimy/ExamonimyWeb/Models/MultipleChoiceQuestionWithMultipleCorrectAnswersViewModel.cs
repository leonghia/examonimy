using ExamonimyWeb.DTOs.QuestionDTO;

namespace ExamonimyWeb.Models
{
    public class MultipleChoiceQuestionWithMultipleCorrectAnswersViewModel : QuestionViewModel
    {
        public required MultipleChoiceQuestionWithMultipleCorrectAnswersGetDto SpecificDetail { get; set; }
    }
}
