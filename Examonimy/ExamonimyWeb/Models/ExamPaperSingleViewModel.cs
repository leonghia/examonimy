using ExamonimyWeb.DTOs.ExamPaperDTO;

namespace ExamonimyWeb.Models
{
    public class ExamPaperSingleViewModel : AuthorizedViewModel
    {
        public required ExamPaperFullGetDto ExamPaper { get; set; }
        public required bool IsReviewer { get; set; }
        public required bool IsAuthor { get; set; }
    }
}
