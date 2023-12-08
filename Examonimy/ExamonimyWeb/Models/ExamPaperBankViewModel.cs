using ExamonimyWeb.DTOs.ExamPaperDTO;

namespace ExamonimyWeb.Models
{
    public class ExamPaperBankViewModel : AuthorizedViewModel
    {
        public required IEnumerable<ExamPaperGetDto> ExamPapers { get; set; }
    }
}
