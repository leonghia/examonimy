using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamonimyWeb.Entities
{
    public class MultipleChoiceQuestionWithMultipleCorrectAnswers
    {
        [Key]
        [ForeignKey("Question")]
        [Required]
        public required int QuestionId { get; set; }
        public Question? Question { get; set; }

        [Required]
        public required string ChoiceA { get; set; }

        [Required]
        public required string ChoiceB { get; set; }

        [Required]
        public required string ChoiceC { get; set; }

        [Required]
        public required string ChoiceD { get; set; }

        [Required]
        [StringLength(7)]
        public required string CorrectAnswers { get; set; }
    }
}
