using ExamonimyWeb.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ExamonimyWeb.DTOs.QuestionDTO
{
    public class FillInBlankQuestionCreateDto : QuestionCreateDto
    {
             
        [Required]
        public required IEnumerable<string> CorrectAnswers { get; set; }
    }
}
