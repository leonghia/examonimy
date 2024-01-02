using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamonimyWeb.Entities
{
    public class MainClass
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 3)]
        public required string Name { get; set; }

        [Required]
        [ForeignKey(nameof(Teacher))]
        public required int TeacherId { get; set; }
        public User? Teacher { get; set; }

        public ICollection<Student>? Students { get; set; }
    }
}
