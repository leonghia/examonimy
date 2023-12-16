using AutoMapper;
using ExamonimyWeb.DTOs.CourseDTO;
using ExamonimyWeb.DTOs.QuestionDTO;
using ExamonimyWeb.DTOs.UserDTO;
using ExamonimyWeb.Entities;
using ExamonimyWeb.Models;
using ExamonimyWeb.Repositories.GenericRepository;
using ExamonimyWeb.Utilities;
using System.Linq.Expressions;

namespace ExamonimyWeb.Services.QuestionService
{
    public class QuestionService : IQuestionService
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

        public QuestionService(IMapper mapper, IGenericRepository<Question> questionRepository, IGenericRepository<MultipleChoiceQuestionWithOneCorrectAnswer> multipleChoiceQuestionWithOneCorrectAnswerRepository, IGenericRepository<MultipleChoiceQuestionWithMultipleCorrectAnswers> multipleChoiceQuestionWithMultipleCorrectAnswersRepository, IGenericRepository<TrueFalseQuestion> trueFalseQuestionRepository, IGenericRepository<ShortAnswerQuestion> shortAnswerQuestionRepository, IGenericRepository<FillInBlankQuestion> fillInBlankQuestionRepository)
        {
            _mapper = mapper;
            _questionRepository = questionRepository;
            _multipleChoiceQuestionWithOneCorrectAnswerRepository = multipleChoiceQuestionWithOneCorrectAnswerRepository;
            _multipleChoiceQuestionWithMultipleCorrectAnswersRepository = multipleChoiceQuestionWithMultipleCorrectAnswersRepository;
            _trueFalseQuestionRepository = trueFalseQuestionRepository;
            _shortAnswerQuestionRepository = shortAnswerQuestionRepository;
            _fillInBlankQuestionRepository = fillInBlankQuestionRepository;         
        }

        public async Task<QuestionViewModel?> GetQuestionViewModelAsync(Question question, User user)
        {
            var includedProperties = new List<string> { "Question.Course", "Question.QuestionType", "Question.QuestionLevel" };
            switch (question.QuestionTypeId)
            {
                case (int)QuestionTypeId.MultipleChoiceWithOneCorrectAnswer:
                    var multipleChoiceQuestionWithOneCorrectAnswer = await _multipleChoiceQuestionWithOneCorrectAnswerRepository.GetAsync(q => q.QuestionId == question.Id, includedProperties);                   
                    return new MultipleChoiceQuestionWithOneCorrectAnswerViewModel
                    {
                        User = _mapper.Map<UserGetDto>(user),
                        GeneralDetail = _mapper.Map<QuestionGetDto>(question),
                        SpecificDetail = _mapper.Map<MultipleChoiceQuestionWithOneCorrectAnswerGetDto>(multipleChoiceQuestionWithOneCorrectAnswer),
                        ViewName = QuestionTypeNames.MultipleChoiceWithOneCorrectAnswer
                    };
                case (int)QuestionTypeId.MultipleChoiceWithMultipleCorrectAnswers:
                    var multipleChoiceQuestionWithMultipleCorrectAnswers = await _multipleChoiceQuestionWithMultipleCorrectAnswersRepository.GetAsync(q => q.QuestionId == question.Id, includedProperties);
                    return new MultipleChoiceQuestionWithMultipleCorrectAnswersViewModel
                    {
                        User = _mapper.Map<UserGetDto>(user),
                        GeneralDetail = _mapper.Map<QuestionGetDto>(question),
                        SpecificDetail = _mapper.Map<MultipleChoiceQuestionWithMultipleCorrectAnswersGetDto>(multipleChoiceQuestionWithMultipleCorrectAnswers),
                        ViewName = QuestionTypeNames.MultipleChoiceWithMultipleCorrectAnswers
                    };
                case (int)QuestionTypeId.TrueFalse:
                    var trueFalseQuestion = await _trueFalseQuestionRepository.GetAsync(q => q.QuestionId == question.Id, includedProperties);                
                    return new TrueFalseQuestionViewModel
                    {
                        User = _mapper.Map<UserGetDto>(user),
                        GeneralDetail = _mapper.Map<QuestionGetDto>(question),
                        SpecificDetail = _mapper.Map<TrueFalseQuestionGetDto>(trueFalseQuestion),
                        ViewName = QuestionTypeNames.TrueFalse
                    };
                case (int)QuestionTypeId.ShortAnswer:
                    var shortAnswerQuestion = await _shortAnswerQuestionRepository.GetAsync(q => q.QuestionId == question.Id, includedProperties);
                    return new ShortAnswerQuestionViewModel
                    {
                        User = _mapper.Map<UserGetDto>(user),
                        GeneralDetail = _mapper.Map<QuestionGetDto>(question),
                        SpecificDetail = _mapper.Map<ShortAnswerQuestionGetDto>(shortAnswerQuestion),
                        ViewName = QuestionTypeNames.ShortAnswer
                    };
                case (int)QuestionTypeId.FillInBlank:
                    var fillInBlankQuestion = await _fillInBlankQuestionRepository.GetAsync(q => q.QuestionId == question.Id, includedProperties);
                    return new FillInBlankQuestionViewModel
                    {
                        User = _mapper.Map<UserGetDto>(user),
                        GeneralDetail = _mapper.Map<QuestionGetDto>(question),
                        SpecificDetail = _mapper.Map<FillInBlankQuestionGetDto>(fillInBlankQuestion),
                        ViewName = QuestionTypeNames.FillInBlank
                    };
                default:
                    return null;
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
                        var specificQuestion1 = await _multipleChoiceQuestionWithOneCorrectAnswerRepository.GetAsync(predicate1, null);
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
                            CorrectAnswer = QuestionAnswerValueHelper.GetAnswerValueFromOneCorrectAnswer(specificQuestion1.CorrectAnswer),
                        };
                        questionsToReturn.Add(questionToReturn1);
                        break;
                    case (int)QuestionTypeId.MultipleChoiceWithMultipleCorrectAnswers:
                        Expression<Func<MultipleChoiceQuestionWithMultipleCorrectAnswers, bool>> predicate2 = q => q.QuestionId == question.Id;
                        var specificQuestion2 = await _multipleChoiceQuestionWithMultipleCorrectAnswersRepository.GetAsync(predicate2, null);
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
                            CorrectAnswers = QuestionAnswerValueHelper.GetAnswerValuesFromMultipleCorrectAnswers(specificQuestion2!.CorrectAnswers)
                        };
                        questionsToReturn.Add(questionToReturn2);
                        break;
                    case (int)QuestionTypeId.TrueFalse:
                        Expression<Func<TrueFalseQuestion, bool>> predicate3 = q => q.QuestionId == question.Id;
                        var specificQuestion3 = await _trueFalseQuestionRepository.GetAsync(predicate3, null);
                        var questionToReturn3 = new TrueFalseQuestionGetDto
                        {
                            Id = question.Id,
                            Course = _mapper.Map<CourseGetDto>(question.Course),
                            QuestionType = _mapper.Map<QuestionTypeGetDto>(question.QuestionType),
                            QuestionLevel = _mapper.Map<QuestionLevelGetDto>(question.QuestionLevel),
                            QuestionContent = question.QuestionContent,
                            Author = _mapper.Map<UserGetDto>(question.Author),
                            CorrectAnswer = QuestionAnswerValueHelper.GetAnswerValueFromTrueFalse(specificQuestion3!.CorrectAnswer)
                        };
                        questionsToReturn.Add(questionToReturn3);
                        break;
                    case (int)QuestionTypeId.ShortAnswer:
                        Expression<Func<ShortAnswerQuestion, bool>> predicate4 = q => q.QuestionId == question.Id;
                        var specificQuestion4 = await _shortAnswerQuestionRepository.GetAsync(predicate4, null);
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
                        var specificQuestion5 = await _fillInBlankQuestionRepository.GetAsync(predicate5, null);
                        var questionToReturn5 = new FillInBlankQuestionGetDto
                        {
                            Id = question.Id,
                            Course = _mapper.Map<CourseGetDto>(question.Course),
                            QuestionType = _mapper.Map<QuestionTypeGetDto>(question.QuestionType),
                            QuestionLevel = _mapper.Map<QuestionLevelGetDto>(question.QuestionLevel),
                            QuestionContent = question.QuestionContent,
                            Author = _mapper.Map<UserGetDto>(question.Author),
                            CorrectAnswers = specificQuestion5!.CorrectAnswers
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
    }
}
