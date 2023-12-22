using ExamonimyWeb.DTOs.CourseDTO;
using ExamonimyWeb.DTOs.ExamPaperDTO;
using ExamonimyWeb.Utilities;

namespace ExamonimyWeb.Models
{
    public class ExamPaperBankViewModel : AuthorizedViewModel
    {
        public required IEnumerable<CourseGetDto> Courses { get; set; }
        public required IEnumerable<ExamPaperStatusModel> Statuses { get; set; }
    }
}
