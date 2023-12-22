using ExamonimyWeb.DTOs.ExamPaperDTO;

namespace ExamonimyWeb.Models
{
    public class ExamPaperSingleViewModel : AuthorizedViewModel
    {
        public required ExamPaperGetDto ExamPaper { get; set; }
    }
}
