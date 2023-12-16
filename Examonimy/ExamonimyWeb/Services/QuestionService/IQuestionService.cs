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
        Task<IEnumerable<QuestionGetDto>> GetQuestionsAsync(List<Question> questions);
        Task<QuestionViewModel?> GetQuestionViewModelAsync(Question question, User user);
        Task<Tuple<int, T>> CreateQuestionAsync<T>(QuestionCreateDto questionCreateDto, IGenericRepository<T> specificQuestionRepository, int authorId) where T : class;
    }
}
