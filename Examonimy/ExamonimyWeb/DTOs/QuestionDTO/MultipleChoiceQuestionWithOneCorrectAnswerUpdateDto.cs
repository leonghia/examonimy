using System.ComponentModel.DataAnnotations;

namespace ExamonimyWeb.DTOs.QuestionDTO
{
    public class MultipleChoiceQuestionWithOneCorrectAnswerUpdateDto : QuestionUpdateDto
    {
        [Required]
        public required string ChoiceA { get; set; }

        [Required]
        public required string ChoiceB { get; set; }

        [Required]
        public required string ChoiceC { get; set; }

        [Required]
        public required string ChoiceD { get; set; }

        [Required]
        public required char CorrectAnswer { get; set; }
    }
}
