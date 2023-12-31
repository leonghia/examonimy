namespace ExamonimyWeb.DTOs.ExamPaperDTO
{
    public class ExamPaperReviewCommentGetDto
    {
        public required string ExamPaperCode { get; set; }
        public required string CommenterName { get; set; }
        public required string CommenterProfilePicture { get; set; }
        public required string Comment { get; set; }
        public required DateTime CommentedAt { get; set; }
    }
}
