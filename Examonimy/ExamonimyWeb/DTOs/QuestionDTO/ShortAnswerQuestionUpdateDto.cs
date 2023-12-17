using System.ComponentModel.DataAnnotations;

namespace ExamonimyWeb.DTOs.QuestionDTO
{
    public class ShortAnswerQuestionUpdateDto : QuestionUpdateDto
    {
        [Required]
        public required string CorrectAnswer { get; set; }
    }
}
