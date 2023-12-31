
namespace ExamonimyWeb.DTOs.ExamPaperDTO
{
    public class ExamPaperReviewHistoryCommentGetDto : ExamPaperReviewHistoryGetDto
    {
        public required string Comment { get; set; }
        public required bool IsAuthor { get; set; }
    }  
}
