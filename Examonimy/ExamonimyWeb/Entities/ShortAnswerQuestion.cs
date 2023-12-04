using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamonimyWeb.Entities
{
    public class ShortAnswerQuestion
    {
        [Key]
        [ForeignKey("Question")]
        [Required]
        public required int QuestionId { get; set; }
        public Question? Question { get; set; }

        [Required]
        public required string CorrectAnswer { get; set; }
    }
}
