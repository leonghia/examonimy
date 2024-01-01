using System.ComponentModel.DataAnnotations;

namespace ExamonimyWeb.DTOs.ExamPaperDTO
{
    public class ExamPaperUpdateDto
    {
        [Required]
        public required ICollection<ExamPaperQuestionUpdateDto> ExamPaperQuestions { get; set; }

        [Required]
        public required string CommitMessage { get; set; }
    }
}
