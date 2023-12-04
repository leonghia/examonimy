using System.ComponentModel.DataAnnotations;

namespace ExamonimyWeb.Entities
{
    public class QuestionType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public required string Name { get; set; }
    }
}
