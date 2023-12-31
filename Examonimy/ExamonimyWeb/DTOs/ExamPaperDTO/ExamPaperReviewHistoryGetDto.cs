
using System.Text.Json.Serialization;

namespace ExamonimyWeb.DTOs.ExamPaperDTO;

[JsonDerivedType(typeof(ExamPaperReviewHistoryAddReviewerGetDto))]
[JsonDerivedType(typeof(ExamPaperReviewHistoryCommentGetDto))]
public class ExamPaperReviewHistoryGetDto
{
    public int Id { get; set; }
    public required string ActorName { get; set; }
    public string? ActorProfilePicture { get; set; }
    public required DateTime CreatedAt { get; set; }      
    public required int OperationId { get; set; }
}
