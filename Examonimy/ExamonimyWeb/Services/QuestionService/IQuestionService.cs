using ExamonimyWeb.DTOs.QuestionDTO;
using ExamonimyWeb.DTOs.UserDTO;
using ExamonimyWeb.Entities;
using ExamonimyWeb.Models;
using ExamonimyWeb.Repositories.GenericRepository;

namespace ExamonimyWeb.Services.QuestionService
{
    public interface IQuestionService
    {
        IGenericRepository<MultipleChoiceQuestionWithOneCorrectAnswer> MultipleChoiceQuestionWithOneCorrectAnswerRepository { get; }
        IGenericRepository<MultipleChoiceQuestionWithMultipleCorrectAnswers> MultipleChoiceQuestionWithMultipleCorrectAnswersRepository { get; }
        IGenericRepository<TrueFalseQuestion> TrueFalseQuestionRepository { get; }
        IGenericRepository<ShortAnswerQuestion> ShortAnswerQuestionRepository { get; }
        IGenericRepository<FillInBlankQuestion> FillInBlankQuestionRepository { get; }
        Task<IEnumerable<QuestionGetDto>> GetQuestionDetailsAsDtos(List<Question> questions);
        Task<QuestionDetailViewModel?> GetQuestionDetailViewModel(Question question, UserGetDto user);
        Task<Tuple<int, T>> CreateQuestion<T>(QuestionCreateDto questionCreateDto, IGenericRepository<T> specificQuestionRepository, int authorId) where T : class;
    }
}
