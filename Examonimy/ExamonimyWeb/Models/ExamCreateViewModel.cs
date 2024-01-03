using ExamonimyWeb.DTOs.ClassDTO;
using ExamonimyWeb.DTOs.CourseDTO;

namespace ExamonimyWeb.Models
{
    public class ExamCreateViewModel : AuthorizedViewModel
    {
        public required ICollection<CourseGetDto> Courses { get; set; }
        public required ICollection<MainClassGetDto> MainClasses { get; set; }
    }
}
