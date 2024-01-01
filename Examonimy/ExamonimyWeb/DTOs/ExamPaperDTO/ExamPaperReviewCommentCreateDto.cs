using System.ComponentModel.DataAnnotations;

namespace ExamonimyWeb.DTOs.ExamPaperDTO
{
    public class ExamPaperReviewCommentCreateDto
    {
        [Required]
        public required string Comment { get; set; }
    }
}
