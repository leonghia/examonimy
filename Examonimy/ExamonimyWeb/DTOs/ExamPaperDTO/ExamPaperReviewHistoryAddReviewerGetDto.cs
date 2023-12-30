
namespace ExamonimyWeb.DTOs.ExamPaperDTO
{
    public class ExamPaperReviewHistoryAddReviewerGetDto : ExamPaperReviewHistoryGetDto
    {
        public required string ReviewerName { get; set; }
    }
}
