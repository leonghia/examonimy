using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamonimyWeb.Entities;

public class CourseModule
{
    [Key]
    public int Id { get; set; }

    [Required]
    public required string Name { get; set; }

    [Required]
    [ForeignKey(nameof(Course))]
    public required int CourseId { get; set; }
    public Course? Course { get; set; }

    public ICollection<Question>? Questions { get; set; }
}
