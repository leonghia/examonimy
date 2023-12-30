using AutoMapper;
using ExamonimyWeb.DTOs.CourseDTO;
using ExamonimyWeb.DTOs.QuestionDTO;
using ExamonimyWeb.DTOs.UserDTO;
using ExamonimyWeb.Entities;
using ExamonimyWeb.Enums;
using ExamonimyWeb.Helpers;
using ExamonimyWeb.Models;
using ExamonimyWeb.Repositories.GenericRepository;
using ExamonimyWeb.Utilities;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace ExamonimyWeb.Managers.QuestionManager
{
    public class QuestionManager : IQuestionManager
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Question> _questionRepository;
        private readonly IGenericRepository<MultipleChoiceQuestionWithOneCorrectAnswer> _multipleChoiceQuestionWithOneCorrectAnswerRepository;
        private readonly IGenericRepository<MultipleChoiceQuestionWithMultipleCorrectAnswers> _multipleChoiceQuestionWithMultipleCorrectAnswersRepository;
        private readonly IGenericRepository<TrueFalseQuestion> _trueFalseQuestionRepository;
        private readonly IGenericRepository<ShortAnswerQuestion> _shortAnswerQuestionRepository;
        private readonly IGenericRepository<FillInBlankQuestion> _fillInBlankQuestionRepository;

        public IGenericRepository<MultipleChoiceQuestionWithOneCorrectAnswer> MultipleChoiceQuestionWithOneCorrectAnswerRepository => _multipleChoiceQuestionWithOneCorrectAnswerRepository;

        public IGenericRepository<MultipleChoiceQuestionWithMultipleCorrectAnswers> MultipleChoiceQuestionWithMultipleCorrectAnswersRepository => _multipleChoiceQuestionWithMultipleCorrectAnswersRepository;

        public IGenericRepository<TrueFalseQuestion> TrueFalseQuestionRepository => _trueFalseQuestionRepository;

        public IGenericRepository<ShortAnswerQuestion> ShortAnswerQuestionRepository => _shortAnswerQuestionRepository;

        public IGenericRepository<FillInBlankQuestion> FillInBlankQuestionRepository => _fillInBlankQuestionRepository;

        public QuestionManager(IMapper mapper, IGenericRepository<Question> questionRepository, IGenericRepository<MultipleChoiceQuestionWithOneCorrectAnswer> multipleChoiceQuestionWithOneCorrectAnswerRepository, IGenericRepository<MultipleChoiceQuestionWithMultipleCorrectAnswers> multipleChoiceQuestionWithMultipleCorrectAnswersRepository, IGenericRepository<TrueFalseQuestion> trueFalseQuestionRepository, IGenericRepository<ShortAnswerQuestion> shortAnswerQuestionRepository, IGenericRepository<FillInBlankQuestion> fillInBlankQuestionRepository)
        {
            _mapper = mapper;
            _questionRepository = questionRepository;
            _multipleChoiceQuestionWithOneCorrectAnswerRepository = multipleChoiceQuestionWithOneCorrectAnswerRepository;
            _multipleChoiceQuestionWithMultipleCorrectAnswersRepository = multipleChoiceQuestionWithMultipleCorrectAnswersRepository;
            _trueFalseQuestionRepository = trueFalseQuestionRepository;
            _shortAnswerQuestionRepository = shortAnswerQuestionRepository;
            _fillInBlankQuestionRepository = fillInBlankQuestionRepository;
        }

        public async Task<QuestionViewModel> GetQuestionViewModelAsync(int questionId, User contextUser)
        {
            var includedProperties = new List<string> { "Question.Course", "Question.QuestionType", "Question.QuestionLevel" };
            var question = await _questionRepository.GetSingleByIdAsync(questionId) ?? throw new ArgumentException(null, nameof(questionId));
            switch (question.QuestionTypeId)
            {
                case (int)QuestionTypeId.MultipleChoiceWithOneCorrectAnswer:
                    var multipleChoiceQuestionWithOneCorrectAnswer = await _multipleChoiceQuestionWithOneCorrectAnswerRepository.GetSingleAsync(q => q.QuestionId == question.Id, includedProperties);
                    return new MultipleChoiceQuestionWithOneCorrectAnswerViewModel
                    {                      
                        User = _mapper.Map<UserGetDto>(contextUser),                      
                        Question = _mapper.Map<MultipleChoiceQuestionWithOneCorrectAnswerGetDto>(multipleChoiceQuestionWithOneCorrectAnswer),
                        ViewName = QuestionTypeNames.MultipleChoiceWithOneCorrectAnswer
                    };
                case (int)QuestionTypeId.MultipleChoiceWithMultipleCorrectAnswers:
                    var multipleChoiceQuestionWithMultipleCorrectAnswers = await _multipleChoiceQuestionWithMultipleCorrectAnswersRepository.GetSingleAsync(q => q.QuestionId == question.Id, includedProperties);
                    return new MultipleChoiceQuestionWithMultipleCorrectAnswersViewModel
                    {
                        User = _mapper.Map<UserGetDto>(contextUser),                      
                        Question = _mapper.Map<MultipleChoiceQuestionWithMultipleCorrectAnswersGetDto>(multipleChoiceQuestionWithMultipleCorrectAnswers),
                        ViewName = QuestionTypeNames.MultipleChoiceWithMultipleCorrectAnswers
                    };
                case (int)QuestionTypeId.TrueFalse:
                    var trueFalseQuestion = await _trueFalseQuestionRepository.GetSingleAsync(q => q.QuestionId == question.Id, includedProperties);
                    return new TrueFalseQuestionViewModel
                    {
                        User = _mapper.Map<UserGetDto>(contextUser),                      
                        Question = _mapper.Map<TrueFalseQuestionGetDto>(trueFalseQuestion),
                        ViewName = QuestionTypeNames.TrueFalse
                    };
                case (int)QuestionTypeId.ShortAnswer:
                    var shortAnswerQuestion = await _shortAnswerQuestionRepository.GetSingleAsync(q => q.QuestionId == question.Id, includedProperties);
                    return new ShortAnswerQuestionViewModel
                    {
                        User = _mapper.Map<UserGetDto>(contextUser),                      
                        Question = _mapper.Map<ShortAnswerQuestionGetDto>(shortAnswerQuestion),
                        ViewName = QuestionTypeNames.ShortAnswer
                    };
                case (int)QuestionTypeId.FillInBlank:
                    var fillInBlankQuestion = await _fillInBlankQuestionRepository.GetSingleAsync(q => q.QuestionId == question.Id, includedProperties);
                    return new FillInBlankQuestionViewModel
                    {
                        User = _mapper.Map<UserGetDto>(contextUser),                        
                        Question = _mapper.Map<FillInBlankQuestionGetDto>(fillInBlankQuestion),
                        ViewName = QuestionTypeNames.FillInBlank
                    };
                default:
                    throw new SwitchExpressionException(question.QuestionTypeId);
            }
        }

        public async Task<IEnumerable<QuestionGetDto>> GetQuestionsAsync(List<Question> questions)
        {
            var questionsToReturn = new List<QuestionGetDto>();

            foreach (var question in questions)
            {

                switch (question.QuestionTypeId)
                {
                    case (int)QuestionTypeId.MultipleChoiceWithOneCorrectAnswer:
                        Expression<Func<MultipleChoiceQuestionWithOneCorrectAnswer, bool>> predicate1 = q => q.QuestionId == question.Id;
                        var specificQuestion1 = await _multipleChoiceQuestionWithOneCorrectAnswerRepository.GetSingleAsync(predicate1, null);
                        var questionToReturn1 = new MultipleChoiceQuestionWithOneCorrectAnswerGetDto
                        {
                            Id = question.Id,
                            Course = _mapper.Map<CourseGetDto>(question.Course),
                            QuestionType = _mapper.Map<QuestionTypeGetDto>(question.QuestionType),
                            QuestionLevel = _mapper.Map<QuestionLevelGetDto>(question.QuestionLevel),
                            QuestionContent = question.QuestionContent,
                            Author = _mapper.Map<UserGetDto>(question.Author),
                            ChoiceA = specificQuestion1!.ChoiceA,
                            ChoiceB = specificQuestion1!.ChoiceB,
                            ChoiceC = specificQuestion1!.ChoiceC,
                            ChoiceD = specificQuestion1!.ChoiceD,
                            CorrectAnswer = QuestionAnswerValueHelper.GetAnswerValueFromByte(specificQuestion1.CorrectAnswer),
                        };
                        questionsToReturn.Add(questionToReturn1);
                        break;
                    case (int)QuestionTypeId.MultipleChoiceWithMultipleCorrectAnswers:
                        Expression<Func<MultipleChoiceQuestionWithMultipleCorrectAnswers, bool>> predicate2 = q => q.QuestionId == question.Id;
                        var specificQuestion2 = await _multipleChoiceQuestionWithMultipleCorrectAnswersRepository.GetSingleAsync(predicate2, null);
                        var questionToReturn2 = new MultipleChoiceQuestionWithMultipleCorrectAnswersGetDto
                        {
                            Id = question.Id,
                            Course = _mapper.Map<CourseGetDto>(question.Course),
                            QuestionType = _mapper.Map<QuestionTypeGetDto>(question.QuestionType),
                            QuestionLevel = _mapper.Map<QuestionLevelGetDto>(question.QuestionLevel),
                            QuestionContent = question.QuestionContent,
                            Author = _mapper.Map<UserGetDto>(question.Author),
                            ChoiceA = specificQuestion2!.ChoiceA,
                            ChoiceB = specificQuestion2!.ChoiceB,
                            ChoiceC = specificQuestion2!.ChoiceC,
                            ChoiceD = specificQuestion2!.ChoiceD,
                            CorrectAnswers = QuestionAnswerValueHelper.GetAnswerValuesFromStringForChoices(specificQuestion2!.CorrectAnswers)
                        };
                        questionsToReturn.Add(questionToReturn2);
                        break;
                    case (int)QuestionTypeId.TrueFalse:
                        Expression<Func<TrueFalseQuestion, bool>> predicate3 = q => q.QuestionId == question.Id;
                        var specificQuestion3 = await _trueFalseQuestionRepository.GetSingleAsync(predicate3, null);
                        var questionToReturn3 = new TrueFalseQuestionGetDto
                        {
                            Id = question.Id,
                            Course = _mapper.Map<CourseGetDto>(question.Course),
                            QuestionType = _mapper.Map<QuestionTypeGetDto>(question.QuestionType),
                            QuestionLevel = _mapper.Map<QuestionLevelGetDto>(question.QuestionLevel),
                            QuestionContent = question.QuestionContent,
                            Author = _mapper.Map<UserGetDto>(question.Author),
                            CorrectAnswer = QuestionAnswerValueHelper.GetAnswerValueFromBool(specificQuestion3!.CorrectAnswer)
                        };
                        questionsToReturn.Add(questionToReturn3);
                        break;
                    case (int)QuestionTypeId.ShortAnswer:
                        Expression<Func<ShortAnswerQuestion, bool>> predicate4 = q => q.QuestionId == question.Id;
                        var specificQuestion4 = await _shortAnswerQuestionRepository.GetSingleAsync(predicate4, null);
                        var questionToReturn4 = new ShortAnswerQuestionGetDto
                        {
                            Id = question.Id,
                            Course = _mapper.Map<CourseGetDto>(question.Course),
                            QuestionType = _mapper.Map<QuestionTypeGetDto>(question.QuestionType),
                            QuestionLevel = _mapper.Map<QuestionLevelGetDto>(question.QuestionLevel),
                            QuestionContent = question.QuestionContent,
                            Author = _mapper.Map<UserGetDto>(question.Author),
                            CorrectAnswer = specificQuestion4!.CorrectAnswer
                        };
                        questionsToReturn.Add(questionToReturn4);
                        break;
                    case (int)QuestionTypeId.FillInBlank:
                        Expression<Func<FillInBlankQuestion, bool>> predicate5 = q => q.QuestionId == question.Id;
                        var specificQuestion5 = await _fillInBlankQuestionRepository.GetSingleAsync(predicate5, null);
                        var questionToReturn5 = new FillInBlankQuestionGetDto
                        {
                            Id = question.Id,
                            Course = _mapper.Map<CourseGetDto>(question.Course),
                            QuestionType = _mapper.Map<QuestionTypeGetDto>(question.QuestionType),
                            QuestionLevel = _mapper.Map<QuestionLevelGetDto>(question.QuestionLevel),
                            QuestionContent = question.QuestionContent,
                            Author = _mapper.Map<UserGetDto>(question.Author),
                            CorrectAnswers = QuestionAnswerValueHelper.GetAnswerValuesFromStringForBlanks(specificQuestion5!.CorrectAnswers)
                        };
                        questionsToReturn.Add(questionToReturn5);
                        break;
                    default:
                        break;
                }
            }

            return questionsToReturn;
        }

        public async Task<Tuple<int, T>> CreateQuestionAsync<T>(QuestionCreateDto questionCreateDto, IGenericRepository<T> specificQuestionRepository, int authorId) where T : class
        {

            var questionToCreate = new Question
            {
                CourseId = questionCreateDto.CourseId,
                QuestionTypeId = questionCreateDto.QuestionTypeId,
                QuestionLevelId = questionCreateDto.QuestionLevelId,
                QuestionContent = questionCreateDto.QuestionContent,
                AuthorId = authorId
            };



            await _questionRepository.InsertAsync(questionToCreate);
            await _questionRepository.SaveAsync();
            var specificQuestionToCreate = _mapper.Map<T>(questionCreateDto);
            specificQuestionToCreate.GetType().GetProperty("QuestionId")!.SetValue(specificQuestionToCreate, questionToCreate.Id);
            await specificQuestionRepository.InsertAsync(specificQuestionToCreate);
            await specificQuestionRepository.SaveAsync();
            return Tuple.Create(questionToCreate.Id, specificQuestionToCreate);
        }      

        public async Task<QuestionGetDto> GetSpecificQuestionWithAnswerDtoAsync(int id)
        {
            var includedProperties = new List<string> { "Question.Course", "Question.QuestionType", "Question.QuestionLevel" };
            var question = await _questionRepository.GetSingleByIdAsync(id) ?? throw new ArgumentException(null, nameof(id));
            switch (question.QuestionTypeId)
            {
                case (int)QuestionTypeId.MultipleChoiceWithOneCorrectAnswer:
                    Expression<Func<MultipleChoiceQuestionWithOneCorrectAnswer, bool>> predicate1 = q => q.QuestionId == id;
                    var specificQuestion1 = await _multipleChoiceQuestionWithOneCorrectAnswerRepository.GetSingleAsync(predicate1, includedProperties);
                    return _mapper.Map<MultipleChoiceQuestionWithOneCorrectAnswerGetDto>(specificQuestion1);
                case (int)QuestionTypeId.MultipleChoiceWithMultipleCorrectAnswers:
                    Expression<Func<MultipleChoiceQuestionWithMultipleCorrectAnswers, bool>> predicate2 = q => q.QuestionId == id;
                    var specificQuestion2 = await _multipleChoiceQuestionWithMultipleCorrectAnswersRepository.GetSingleAsync(predicate2, includedProperties);
                    return _mapper.Map<MultipleChoiceQuestionWithMultipleCorrectAnswersGetDto>(specificQuestion2);
                case (int)QuestionTypeId.TrueFalse:
                    Expression<Func<TrueFalseQuestion, bool>> predicate3 = q => q.QuestionId == id;
                    var specificQuestion3 = await _trueFalseQuestionRepository.GetSingleAsync(predicate3, includedProperties);
                    return _mapper.Map<TrueFalseQuestionGetDto>(specificQuestion3);
                case (int)QuestionTypeId.ShortAnswer:
                    Expression<Func<ShortAnswerQuestion, bool>> predicate4 = q => q.QuestionId == id;
                    var specificQuestion4 = await _shortAnswerQuestionRepository.GetSingleAsync(predicate4, includedProperties);
                    return _mapper.Map<ShortAnswerQuestionGetDto>(specificQuestion4);
                case (int)QuestionTypeId.FillInBlank:
                    Expression<Func<FillInBlankQuestion, bool>> predicate5 = q => q.QuestionId == id;
                    var specificQuestion5 = await _fillInBlankQuestionRepository.GetSingleAsync(predicate5, includedProperties);
                    return _mapper.Map<FillInBlankQuestionGetDto>(specificQuestion5);
                default:
                    throw new SwitchExpressionException(question.QuestionTypeId);
            }
        }

        public async Task<QuestionWithoutAnswerGetDto> GetSpecificQuestionWithoutAnswerDtoAsync(int questionId)
        {
            var question = await _questionRepository.GetSingleByIdAsync(questionId) ?? throw new ArgumentException(null, nameof(questionId));
            var includedProps = new List<string> { "Question.QuestionType" };
            switch (question.QuestionTypeId)
            {
                case (int)QuestionTypeId.MultipleChoiceWithOneCorrectAnswer:
                    Expression<Func<MultipleChoiceQuestionWithOneCorrectAnswer, bool>> predicate1 = q => q.QuestionId == questionId;
                    var specificQuestion1 = await _multipleChoiceQuestionWithOneCorrectAnswerRepository.GetSingleAsync(predicate1, includedProps);
                    return _mapper.Map<MultipleChoiceQuestionWithOneCorrectAnswerWithoutAnswerGetDto>(specificQuestion1);
                case (int)QuestionTypeId.MultipleChoiceWithMultipleCorrectAnswers:
                    Expression<Func<MultipleChoiceQuestionWithMultipleCorrectAnswers, bool>> predicate2 = q => q.QuestionId == questionId;
                    var specificQuestion2 = await _multipleChoiceQuestionWithMultipleCorrectAnswersRepository.GetSingleAsync(predicate2, includedProps);
                    return _mapper.Map<MultipleChoiceQuestionWithMultipleCorrectAnswersWithoutAnswerGetDto>(specificQuestion2);
                case (int)QuestionTypeId.TrueFalse:
                    Expression<Func<TrueFalseQuestion, bool>> predicate3 = q => q.QuestionId == questionId;
                    var specificQuestion3 = await _trueFalseQuestionRepository.GetSingleAsync(predicate3, includedProps);
                    return _mapper.Map<TrueFalseQuestionWithoutAnswerGetDto>(specificQuestion3);
                case (int)QuestionTypeId.ShortAnswer:
                    Expression<Func<ShortAnswerQuestion, bool>> predicate4 = q => q.QuestionId == questionId;
                    var specificQuestion4 = await _shortAnswerQuestionRepository.GetSingleAsync(predicate4, includedProps);
                    return _mapper.Map<ShortAnswerQuestionWithoutAnswerGetDto>(specificQuestion4);
                case (int)QuestionTypeId.FillInBlank:
                    Expression<Func<FillInBlankQuestion, bool>> predicate5 = q => q.QuestionId == questionId;
                    var specificQuestion5 = await _fillInBlankQuestionRepository.GetSingleAsync(predicate5, includedProps);
                    return _mapper.Map<FillInBlankQuestionWithoutAnswerGetDto>(specificQuestion5);
                default:
                    throw new SwitchExpressionException(question.QuestionTypeId);
            }
        }

        public async Task<bool> DoesQuestionExistAsync(int questionId)
        {
            return (await _questionRepository.GetSingleByIdAsync(questionId)) is not null;
        }

        public async Task<bool> IsAuthorAsync(int questionId, int userId)
        {
            Expression<Func<Question, bool>> predicate = q => q.Id == questionId;
            var question = await _questionRepository.GetSingleAsync(predicate, new List<string> { "Author" }) ?? throw new ArgumentException(null, nameof(questionId));
            return question.Author!.Id == userId;
        }

        public async Task UpdateThenSaveAsync(int id, QuestionUpdateDto questionUpdateDto)
        {
            var question = await _questionRepository.GetSingleByIdAsync(id) ?? throw new ArgumentException(null, nameof(id));
            question.QuestionLevelId = questionUpdateDto.QuestionLevelId;
            question.QuestionContent = questionUpdateDto.QuestionContent;
            _questionRepository.Update(question);
            await _questionRepository.SaveAsync();
            switch (question.QuestionTypeId)
            {
                case (int)QuestionTypeId.MultipleChoiceWithOneCorrectAnswer:
                    var specificQuestion1 = await _multipleChoiceQuestionWithOneCorrectAnswerRepository.GetSingleByIdAsync(id) ?? throw new ArgumentException(null, nameof(id));
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
                    var specificQueston2 = await _multipleChoiceQuestionWithMultipleCorrectAnswersRepository.GetSingleByIdAsync(id) ?? throw new ArgumentException(null, nameof(id));
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
                    var specificQuestion3 = await _trueFalseQuestionRepository.GetSingleByIdAsync(id) ?? throw new ArgumentException(null, nameof(id));
                    var temp3 = questionUpdateDto as TrueFalseQuestionUpdateDto;
                    specificQuestion3.CorrectAnswer = QuestionAnswerValueHelper.GetAnswerValueFromCharForTrueFalse(temp3!.CorrectAnswer);
                    _trueFalseQuestionRepository.Update(specificQuestion3);
                    await _trueFalseQuestionRepository.SaveAsync();
                    break;
                case (int)QuestionTypeId.ShortAnswer:
                    var specificQuestion4 = await _shortAnswerQuestionRepository.GetSingleByIdAsync(id) ?? throw new ArgumentException(null, nameof(id));
                    var temp4 = questionUpdateDto as ShortAnswerQuestionUpdateDto;
                    specificQuestion4.CorrectAnswer = temp4!.CorrectAnswer;
                    _shortAnswerQuestionRepository.Update(specificQuestion4);
                    await _shortAnswerQuestionRepository.SaveAsync();
                    break;
                case (int)QuestionTypeId.FillInBlank:
                    var specificQuestion5 = await _fillInBlankQuestionRepository.GetSingleByIdAsync(id) ?? throw new ArgumentException(null, nameof(id));
                    var temp5 = questionUpdateDto as FillInBlankQuestionUpdateDto;
                    specificQuestion5.CorrectAnswers = QuestionAnswerValueHelper.GetAnswerValuesFromListOfStringForBlanks(temp5!.CorrectAnswers.ToList());
                    _fillInBlankQuestionRepository.Update(specificQuestion5);
                    await _fillInBlankQuestionRepository.SaveAsync();
                    break;
                default:
                    throw new SwitchExpressionException(question.QuestionTypeId);
            };           
        }

        public async Task DeleteThenSaveAsync(int id)
        {
            var question = await _questionRepository.GetSingleByIdAsync(id) ?? throw new ArgumentException(null, nameof(id));
            _questionRepository.Delete(question);
            await _questionRepository.SaveAsync();
        }

        public async Task<Question?> GetSingleByIdAsync(object id)
        {
            return await _questionRepository.GetSingleByIdAsync(id);
        }
    }
}
