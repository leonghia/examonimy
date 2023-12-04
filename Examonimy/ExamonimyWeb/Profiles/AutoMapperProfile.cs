using AutoMapper;
using ExamonimyWeb.DTOs.CourseDTO;
using ExamonimyWeb.DTOs.ExamDTO;
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
            CreateMap<Exam, ExamGetDto>();
            CreateMap<ExamCreateDto, Exam>();
            CreateMap<QuestionType, QuestionTypeGetDto>();
            CreateMap<QuestionLevel, QuestionLevelGetDto>();
        }
    }
}
