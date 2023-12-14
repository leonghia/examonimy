using AutoMapper;
using ExamonimyWeb.DTOs.CourseDTO;
using ExamonimyWeb.DTOs.ExamPaperDTO;
using ExamonimyWeb.DTOs.QuestionDTO;
using ExamonimyWeb.DTOs.RoleDTO;
using ExamonimyWeb.DTOs.UserDTO;
using ExamonimyWeb.Entities;

namespace ExamonimyWeb.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserRegisterDto, User>()
                .ForMember(dest => dest.NormalizedUsername, opt => opt.MapFrom(src => src.Username.ToUpperInvariant()))
                .ForMember(dest => dest.NormalizedEmail, opt => opt.MapFrom(src => src.Email.ToUpperInvariant()));
            CreateMap<User, UserGetDto>();
            CreateMap<Role, RoleGetDto>();
            CreateMap<Course, CourseGetDto>();          
            CreateMap<QuestionType, QuestionTypeGetDto>();
            CreateMap<QuestionLevel, QuestionLevelGetDto>();
            CreateMap<MultipleChoiceQuestionWithOneCorrectAnswerCreateDto, MultipleChoiceQuestionWithOneCorrectAnswer>();
            CreateMap<MultipleChoiceQuestionWithMultipleCorrectAnswersCreateDto, MultipleChoiceQuestionWithMultipleCorrectAnswers>();
            CreateMap<TrueFalseQuestionCreateDto, TrueFalseQuestion>();
            CreateMap<ShortAnswerQuestionCreateDto, ShortAnswerQuestion>();
            CreateMap<FillInBlankQuestionCreateDto, FillInBlankQuestion>();
            CreateMap<Question, QuestionGetDto>();
            CreateMap<MultipleChoiceQuestionWithOneCorrectAnswer, MultipleChoiceQuestionWithOneCorrectAnswerGetDto>();
            CreateMap<MultipleChoiceQuestionWithMultipleCorrectAnswers, MultipleChoiceQuestionWithMultipleCorrectAnswersGetDto>();
            CreateMap<TrueFalseQuestion, TrueFalseQuestionGetDto>();
            CreateMap<ShortAnswerQuestion, ShortAnswerQuestionGetDto>();
            CreateMap<FillInBlankQuestion, FillInBlankQuestionGetDto>();
            CreateMap<ExamPaper, ExamPaperGetDto>();
            CreateMap<ExamPaperCreateDto, ExamPaper>();
            CreateMap<ExamPaperQuestionCreateDto, ExamPaperQuestion>();
        }
    }
}
