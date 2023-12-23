using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ExamonimyWeb.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 3)]
        public required string FullName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]     
        public required string Username { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [StringLength(320)]
        public required string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string? PasswordHash { get; set; }

        [Required]
        [StringLength(255)]
        public string? PasswordSalt { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string? NormalizedUsername { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [StringLength(320)]
        public string? NormalizedEmail { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DateOfBirth { get; set; }

        [Required]
        [ForeignKey("Role")]
        public int? RoleId { get; set; }
        public Role? Role { get; set; }

        [StringLength(256)]
        public string? RefreshToken { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? RefreshTokenExpiryTime { get; set; }

        public string? ProfilePicture { get; set; } = "https://nghia.b-cdn.net/examonimy/images/examonimy-default-pfp.jpg";

        public ICollection<ExamPaper>? ExamPapersCreated { get; set; }      
        
        public ICollection<ExamPaper>? ExamPapersToReview { get; set; }
        public ICollection<ExamPaperReviewer>? ExamPaperReviewers { get; set; }

    }
}
