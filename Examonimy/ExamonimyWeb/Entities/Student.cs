using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamonimyWeb.Entities;

[Microsoft.EntityFrameworkCore.Index(nameof(RollNumber), IsUnique = true)]
public class Student
{
    
    [Key]
    [ForeignKey(nameof(User))]
    public required int UserId { get; set; }
    public User? User { get; set; }

    [Required]
    [StringLength(16, MinimumLength = 4)]
    public required string RollNumber { get; set; }

    [Required]
    [ForeignKey(nameof(MainClass))]
    public required int MainClassId { get; set; }
    public MainClass? MainClass { get; set; }
}
