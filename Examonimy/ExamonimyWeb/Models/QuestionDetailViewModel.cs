using ExamonimyWeb.DTOs.QuestionDTO;
using ExamonimyWeb.DTOs.UserDTO;

namespace ExamonimyWeb.Models
{
    public class QuestionDetailViewModel : AuthorizedViewModel
    {      
        public required QuestionGetDto Question { get; set; }      
    }
}
