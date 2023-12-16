using ExamonimyWeb.DTOs.QuestionDTO;

namespace ExamonimyWeb.Models
{
    public class QuestionViewModel : AuthorizedViewModel
    {      
        public required QuestionGetDto GeneralDetail { get; set; }      
    }
}
