using ExamonimyWeb.DTOs.QuestionDTO;
using ExamonimyWeb.Entities;
using ExamonimyWeb.Utilities;

namespace ExamonimyWeb.Managers.QuestionManager;

public interface IQuestionManager
{
    Task<Question?> GetByIdAsync(int id);

    Task<bool> IsAuthorAsync(int questionId, int userId);
    Task UpdateAsync(int id, QuestionUpdateDto questionUpdateDto);
    Task DeleteAsync(int id);
    Task<IDictionary<int, int>> CountByCourseAsync();
    Task<QuestionGetDto> GetFullQuestionDto(int questionTypeId, int questionId);
    Task<PagedList<Question>> GetFullPagedList(RequestParamsForQuestion? requestParamsForQuestion);
    Task CreateAsync(Question q);
    Task CreateAsync(MultipleChoiceQuestionWithOneCorrectAnswer q);
    Task CreateAsync(MultipleChoiceQuestionWithMultipleCorrectAnswers q);
    Task CreateAsync(TrueFalseQuestion q);
    Task CreateAsync(ShortAnswerQuestion q);
    Task CreateAsync(FillInBlankQuestion q);
    Dictionary<byte, char> QuestionAnswerByteToCharMappings { get; }
    Dictionary<char, byte> QuestionAnswerCharToByteMappings { get; }
}
