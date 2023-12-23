using System.ComponentModel.DataAnnotations;

namespace ExamonimyWeb.DTOs.ExamPaperDTO
{
    public class ExamPaperReviewerCreateDto
    {
        [Required]
        public required ICollection<int> ReviewerIds { get; set; }
    }
}
