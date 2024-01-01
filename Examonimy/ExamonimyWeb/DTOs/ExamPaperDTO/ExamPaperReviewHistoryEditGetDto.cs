using ExamonimyWeb.Entities;

namespace ExamonimyWeb.DTOs.ExamPaperDTO;

public class ExamPaperReviewHistoryEditGetDto : ExamPaperReviewHistoryGetDto
{
    public required string CommitMessage { get; set; }
}
