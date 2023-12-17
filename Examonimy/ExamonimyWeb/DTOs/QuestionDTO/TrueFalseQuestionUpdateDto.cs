using System.ComponentModel.DataAnnotations;

namespace ExamonimyWeb.DTOs.QuestionDTO
{
    public class TrueFalseQuestionUpdateDto : QuestionUpdateDto
    {
        [Required]      
        public required char CorrectAnswer { get; set; }
    }
}
