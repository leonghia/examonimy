using ExamonimyWeb.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ExamonimyWeb.DTOs.QuestionDTO
{
    public class TrueFalseQuestionCreateDto : QuestionCreateDto
    {
              

        [Required]
        public required bool CorrectAnswer { get; set; }
    }
}
