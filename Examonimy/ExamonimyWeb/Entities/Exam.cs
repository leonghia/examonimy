using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamonimyWeb.Entities
{
    public class Exam
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(256)]
        public required string Name { get; set; }

        [Required]
        [ForeignKey("Course")]
        public required int CourseId { get; set; }

        public Course? Course { get; set; }

        [Required]
        [ForeignKey("Author")]
        public required int AuthorId { get; set; }

        public User? Author { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public required int TimeAllowedInMinutes { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public required DateTime From { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public required DateTime To { get; set; }

        [StringLength(512)]
        public string? Description { get; set; }
    }
}
