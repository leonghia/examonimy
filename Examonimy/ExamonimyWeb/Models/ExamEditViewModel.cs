using ExamonimyWeb.DTOs.ExamPaperDTO;

namespace ExamonimyWeb.Models;

public class ExamEditViewModel : AuthorizedViewModel
{
    public required int ExamId { get; set; }
    public required ICollection<ExamPaperGetDto> ExamPapers { get; set; }
    public required string CourseName { get; set; }
    public required int ExamPaperId { get; set; }
}
