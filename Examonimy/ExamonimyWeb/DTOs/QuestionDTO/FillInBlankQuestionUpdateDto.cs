using System.ComponentModel.DataAnnotations;

namespace ExamonimyWeb.DTOs.QuestionDTO
{
    public class FillInBlankQuestionUpdateDto : QuestionUpdateDto
    {

        [Required]
        public required IEnumerable<string> CorrectAnswers { get; set; }
    }
}
