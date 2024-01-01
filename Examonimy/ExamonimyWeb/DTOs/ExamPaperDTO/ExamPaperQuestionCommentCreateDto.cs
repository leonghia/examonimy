using System.ComponentModel.DataAnnotations;

namespace ExamonimyWeb.DTOs.ExamPaperDTO
{
    public class ExamPaperQuestionCommentCreateDto
    {
        [Required]
        public required int ExamPaperQuestionId { get; set; }

        [Required]
        public required string Comment { get; set; }
    }
}
