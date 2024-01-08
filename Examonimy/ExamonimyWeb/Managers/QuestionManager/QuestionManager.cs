using ExamonimyWeb.DTOs.QuestionDTO;
using ExamonimyWeb.Entities;
using ExamonimyWeb.Enums;
using ExamonimyWeb.Helpers;
using ExamonimyWeb.Repositories;
using ExamonimyWeb.Utilities;
using LinqKit;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace ExamonimyWeb.Managers.QuestionManager;

public class QuestionManager : IQuestionManager
{
    
    private readonly IGenericRepository<Question> _questionRepository;
    private readonly IGenericRepository<MultipleChoiceQuestionWithOneCorrectAnswer> _multipleChoiceQuestionWithOneCorrectAnswerRepository;
    private readonly IGenericRepository<MultipleChoiceQuestionWithMultipleCorrectAnswers> _multipleChoiceQuestionWithMultipleCorrectAnswersRepository;
    private readonly IGenericRepository<TrueFalseQuestion> _trueFalseQuestionRepository;
    private readonly IGenericRepository<ShortAnswerQuestion> _shortAnswerQuestionRepository;
    private readonly IGenericRepository<FillInBlankQuestion> _fillInBlankQuestionRepository;
    private readonly Dictionary<byte, char> _questionAnswerByteToCharMappings = new()
    {
        { 0, 'A' },
        { 1, 'B' },
        { 2, 'C' },
        { 3, 'D' }
    };
    private readonly Dictionary<char, byte> _questionAnswerCharToByteMappings = new()
    {
        { 'A', 0 },
        { 'B', 1 },
        { 'C' , 2 },
        { 'D' , 3 }
    };

    public Dictionary<byte, char> QuestionAnswerByteToCharMappings => _questionAnswerByteToCharMappings;
    public Dictionary<char, byte> QuestionAnswerCharToByteMappings => _questionAnswerCharToByteMappings;

    public QuestionManager(IGenericRepository<Question> questionRepository, IGenericRepository<MultipleChoiceQuestionWithOneCorrectAnswer> multipleChoiceQuestionWithOneCorrectAnswerRepository, IGenericRepository<MultipleChoiceQuestionWithMultipleCorrectAnswers> multipleChoiceQuestionWithMultipleCorrectAnswersRepository, IGenericRepository<TrueFalseQuestion> trueFalseQuestionRepository, IGenericRepository<ShortAnswerQuestion> shortAnswerQuestionRepository, IGenericRepository<FillInBlankQuestion> fillInBlankQuestionRepository)
    {
        
        _questionRepository = questionRepository;
        _multipleChoiceQuestionWithOneCorrectAnswerRepository = multipleChoiceQuestionWithOneCorrectAnswerRepository;
        _multipleChoiceQuestionWithMultipleCorrectAnswersRepository = multipleChoiceQuestionWithMultipleCorrectAnswersRepository;
        _trueFalseQuestionRepository = trueFalseQuestionRepository;
        _shortAnswerQuestionRepository = shortAnswerQuestionRepository;
        _fillInBlankQuestionRepository = fillInBlankQuestionRepository;
    }  
    

    

    public async Task<bool> IsAuthorAsync(int questionId, int userId)
    {
        Expression<Func<Question, bool>> predicate = q => q.Id == questionId;
        var question = await _questionRepository.GetAsync(predicate, new List<string> { "Author" }) ?? throw new ArgumentException(null, nameof(questionId));
        return question.Author!.Id == userId;
    }

    public async Task UpdateAsync(int id, QuestionUpdateDto questionUpdateDto)
    {
        var question = await _questionRepository.GetByIdAsync(id) ?? throw new ArgumentException(null, nameof(id));          
        question.QuestionContent = questionUpdateDto.QuestionContent;
        _questionRepository.Update(question);
        await _questionRepository.SaveAsync();
        switch (question.QuestionTypeId)
        {
            case (int)QuestionTypeId.MultipleChoiceWithOneCorrectAnswer:
                var specificQuestion1 = await _multipleChoiceQuestionWithOneCorrectAnswerRepository.GetByIdAsync(id) ?? throw new ArgumentException(null, nameof(id));
                var temp1 = questionUpdateDto as MultipleChoiceQuestionWithOneCorrectAnswerUpdateDto;
                specificQuestion1.ChoiceA = temp1!.ChoiceA;
                specificQuestion1.ChoiceB = temp1.ChoiceB;
                specificQuestion1.ChoiceC = temp1.ChoiceC;
                specificQuestion1.ChoiceD = temp1.ChoiceD;
                specificQuestion1.CorrectAnswer = QuestionAnswerValueHelper.GetAnswerValueFromChar(temp1.CorrectAnswer);
                _multipleChoiceQuestionWithOneCorrectAnswerRepository.Update(specificQuestion1);
                await _multipleChoiceQuestionWithOneCorrectAnswerRepository.SaveAsync();
                break;
            case (int)QuestionTypeId.MultipleChoiceWithMultipleCorrectAnswers:
                var specificQueston2 = await _multipleChoiceQuestionWithMultipleCorrectAnswersRepository.GetByIdAsync(id) ?? throw new ArgumentException(null, nameof(id));
                var temp2 = questionUpdateDto as MultipleChoiceQuestionWithMultipleCorrectAnswersUpdateDto;
                specificQueston2.ChoiceA = temp2!.ChoiceA;
                specificQueston2.ChoiceB = temp2.ChoiceB;
                specificQueston2.ChoiceC = temp2.ChoiceC;
                specificQueston2.ChoiceD = temp2.ChoiceD;
                specificQueston2.CorrectAnswers = QuestionAnswerValueHelper.GetAnswerValuesFromListOfChar(temp2.CorrectAnswers.ToList());
                _multipleChoiceQuestionWithMultipleCorrectAnswersRepository.Update(specificQueston2);
                await _multipleChoiceQuestionWithMultipleCorrectAnswersRepository.SaveAsync();
                break;
            case (int)QuestionTypeId.TrueFalse:
                var specificQuestion3 = await _trueFalseQuestionRepository.GetByIdAsync(id) ?? throw new ArgumentException(null, nameof(id));
                var temp3 = questionUpdateDto as TrueFalseQuestionUpdateDto;
                specificQuestion3.CorrectAnswer = QuestionAnswerValueHelper.GetAnswerValueFromCharForTrueFalse(temp3!.CorrectAnswer);
                _trueFalseQuestionRepository.Update(specificQuestion3);
                await _trueFalseQuestionRepository.SaveAsync();
                break;
            case (int)QuestionTypeId.ShortAnswer:
                var specificQuestion4 = await _shortAnswerQuestionRepository.GetByIdAsync(id) ?? throw new ArgumentException(null, nameof(id));
                var temp4 = questionUpdateDto as ShortAnswerQuestionUpdateDto;
                specificQuestion4.CorrectAnswer = temp4!.CorrectAnswer;
                _shortAnswerQuestionRepository.Update(specificQuestion4);
                await _shortAnswerQuestionRepository.SaveAsync();
                break;
            case (int)QuestionTypeId.FillInBlank:
                var specificQuestion5 = await _fillInBlankQuestionRepository.GetByIdAsync(id) ?? throw new ArgumentException(null, nameof(id));
                var temp5 = questionUpdateDto as FillInBlankQuestionUpdateDto;
                specificQuestion5.CorrectAnswers = QuestionAnswerValueHelper.GetAnswerValuesFromListOfStringForBlanks(temp5!.CorrectAnswers.ToList());
                _fillInBlankQuestionRepository.Update(specificQuestion5);
                await _fillInBlankQuestionRepository.SaveAsync();
                break;
            default:
                throw new SwitchExpressionException(question.QuestionTypeId);
        };           
    }

    public async Task DeleteAsync(int id)
    {
        var question = await _questionRepository.GetByIdAsync(id) ?? throw new ArgumentException(null, nameof(id));
        _questionRepository.Delete(question);
        await _questionRepository.SaveAsync();
    }

    public async Task<IDictionary<int, int>> CountByCourseAsync()
    {
        return await _questionRepository.CountGroupByPropIdAsync(q => q.CourseId);
    }

    public async Task<PagedList<Question>> GetFullPagedList(RequestParamsForQuestion? requestParamsForQuestion)
    {
        var predicate = PredicateBuilder.New<Question>(true);
        if (requestParamsForQuestion is not null && requestParamsForQuestion.CourseId > 0)
            predicate = predicate.And(p => p.CourseId == requestParamsForQuestion.CourseId);
        if (requestParamsForQuestion is not null && requestParamsForQuestion.QuestionTypeId > 0)
            predicate = predicate.And(p => p.QuestionTypeId == requestParamsForQuestion.QuestionTypeId);
        var questions = await _questionRepository.GetPagedListAsync(requestParamsForQuestion, predicate);

        return questions;
    }

    public async Task<QuestionGetDto> GetFullQuestionDto(int questionTypeId, int questionId)
    {
        var includedProps = new List<string> { "Question.Course", "Question", "Question.QuestionType", "Question.Author" };
        switch (questionTypeId)
        {
            case (int)QuestionTypeId.MultipleChoiceWithOneCorrectAnswer:
                var q1 = await _multipleChoiceQuestionWithOneCorrectAnswerRepository.GetAsync(q => q.QuestionId == questionId, includedProps) ?? throw new ArgumentException(null, nameof(questionId));
                return new MultipleChoiceQuestionWithOneCorrectAnswerGetDto
                {
                    Id = questionId,
                    Course = q1.Question!.Course!.Name,
                    QuestionType = q1.Question.QuestionType!.Name,
                    QuestionContent = q1.Question.QuestionContent,
                    Author = q1.Question.Author!.FullName,
                    ChoiceA = q1.ChoiceA,
                    ChoiceB = q1.ChoiceB,
                    ChoiceC = q1.ChoiceC,
                    ChoiceD = q1.ChoiceD,
                    CorrectAnswer = _questionAnswerByteToCharMappings[q1.CorrectAnswer]
                };
            case (int)QuestionTypeId.MultipleChoiceWithMultipleCorrectAnswers:
                var q2 = await _multipleChoiceQuestionWithMultipleCorrectAnswersRepository.GetAsync(q => q.QuestionId == questionId, includedProps) ?? throw new ArgumentException(null, nameof(questionId));
                return new MultipleChoiceQuestionWithMultipleCorrectAnswersGetDto
                {
                    Id = questionId,
                    Course = q2.Question!.Course!.Name,
                    QuestionType = q2.Question!.QuestionType!.Name,
                    QuestionContent = q2.Question!.QuestionContent,
                    Author = q2.Question!.Author!.FullName,
                    ChoiceA = q2.ChoiceA,
                    ChoiceB = q2.ChoiceB,
                    ChoiceC = q2.ChoiceC,
                    ChoiceD = q2.ChoiceD,
                    CorrectAnswers = q2.CorrectAnswers.Split('|').Select(c => _questionAnswerByteToCharMappings[Convert.ToByte(c)]).ToList()
                };
            case (int)QuestionTypeId.TrueFalse:
                var q3 = await _trueFalseQuestionRepository.GetAsync(q => q.QuestionId == questionId, includedProps) ?? throw new ArgumentException(null, nameof(questionId));
                return new TrueFalseQuestionGetDto
                {
                    Id = questionId,
                    Course = q3.Question!.Course!.Name,
                    QuestionType = q3.Question!.QuestionType!.Name,
                    QuestionContent = q3.Question!.QuestionContent,
                    Author = q3.Question!.Author!.FullName,
                    CorrectAnswer = q3.CorrectAnswer ? 'Đ' : 'S'
                };
            case (int)QuestionTypeId.ShortAnswer:
                var q4 = await _shortAnswerQuestionRepository.GetAsync(q => q.QuestionId == questionId, includedProps) ?? throw new ArgumentException(null, nameof(questionId));
                return new ShortAnswerQuestionGetDto
                {
                    Id = questionId,
                    Course = q4.Question!.Course!.Name,
                    QuestionType = q4.Question!.QuestionType!.Name,
                    QuestionContent = q4.Question!.QuestionContent,
                    Author = q4.Question!.Author!.FullName,
                    CorrectAnswer = q4.CorrectAnswer
                };
            case (int)QuestionTypeId.FillInBlank:
                var q5 = await _fillInBlankQuestionRepository.GetAsync(q => q.QuestionId == questionId, includedProps) ?? throw new ArgumentException(null, nameof(questionId));
                return new FillInBlankQuestionGetDto
                {
                    Id = questionId,
                    Course = q5.Question!.Course!.Name,
                    QuestionType = q5.Question!.QuestionType!.Name,
                    QuestionContent = q5.Question!.QuestionContent,
                    Author = q5.Question!.Author!.FullName,
                    CorrectAnswers = q5.CorrectAnswers.Split('|').ToList()
                };
            default:
                throw new SwitchExpressionException(questionTypeId);
        }
    }

    public async Task<Question?> GetByIdAsync(int id)
    {
        return await _questionRepository.GetByIdAsync(id);
    }

    public async Task CreateAsync(Question q)
    {
        await _questionRepository.InsertAsync(q);
        await _questionRepository.SaveAsync();
    }

    public async Task CreateAsync(MultipleChoiceQuestionWithOneCorrectAnswer q)
    {
        await _multipleChoiceQuestionWithOneCorrectAnswerRepository.InsertAsync(q);
        await _multipleChoiceQuestionWithOneCorrectAnswerRepository.SaveAsync();
    }

    public async Task CreateAsync(MultipleChoiceQuestionWithMultipleCorrectAnswers q)
    {
        await _multipleChoiceQuestionWithMultipleCorrectAnswersRepository.InsertAsync(q);
        await _multipleChoiceQuestionWithMultipleCorrectAnswersRepository.SaveAsync();
    }

    public async Task CreateAsync(TrueFalseQuestion q)
    {
        await _trueFalseQuestionRepository.InsertAsync(q);
        await _trueFalseQuestionRepository.SaveAsync();
    }

    public async Task CreateAsync(ShortAnswerQuestion q)
    {
        await _shortAnswerQuestionRepository.InsertAsync(q);
        await _shortAnswerQuestionRepository.SaveAsync();
    }

    public async Task CreateAsync(FillInBlankQuestion q)
    {
        await _fillInBlankQuestionRepository.InsertAsync(q);
        await _fillInBlankQuestionRepository.SaveAsync();
    }
}
