using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamonimyWeb.Entities
{
    public class Test
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Course")]
        [Required]
        public required int CourseId { get; set; }
        public Course? Course { get; set; }

        [Required]
        public byte TimeAllowedInMinutes { get; set; }

        [ForeignKey("Author")]
        [Required]
        public required int AuthorId { get; set; }
        public User? Author { get; set; }
    }
}
