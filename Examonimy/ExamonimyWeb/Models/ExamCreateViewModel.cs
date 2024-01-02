using ExamonimyWeb.DTOs.CourseDTO;

namespace ExamonimyWeb.Models
{
    public class ExamCreateViewModel : AuthorizedViewModel
    {
        public required ICollection<CourseGetDto> Courses { get; set; }
    }
}
