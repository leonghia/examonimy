using ExamonimyWeb.DTOs.QuestionDTO;

namespace ExamonimyWeb.Models
{
    public class QuestionBankViewModel : AuthorizedViewModel
    {        
        public required IEnumerable<QuestionTypeGetDto> QuestionTypes { get; set; }
    }
}
