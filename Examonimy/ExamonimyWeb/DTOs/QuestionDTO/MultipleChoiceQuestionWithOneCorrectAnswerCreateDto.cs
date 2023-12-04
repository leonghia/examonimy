using ExamonimyWeb.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ExamonimyWeb.DTOs.QuestionDTO
{
    public class MultipleChoiceQuestionWithOneCorrectAnswerCreateDto : QuestionCreateDto
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
        public required byte CorrectAnswer { get; set; }
    }
}
