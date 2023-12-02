using System.ComponentModel.DataAnnotations;

namespace ExamonimyWeb.Entities
{
    public class Course
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(256, MinimumLength = 3)]
        public required string Name { get; set; }

        [Required]
        [StringLength(16, MinimumLength = 1)]
        public required string CourseCode { get; set; }
    }
}
