using System.ComponentModel.DataAnnotations;

namespace ExamonimyWeb.Models
{
    public class RefreshRequest
    {
        [Required]
        [StringLength(256)]
        public required string AccessToken { get; set; }

        [Required]
        [StringLength(256)]
        public required string RefreshToken { get; set; }
    }
}
