using AutoMapper;
using ExamonimyWeb.Entities;
using ExamonimyWeb.Models.DTOs.CourseDTO;
using ExamonimyWeb.Models.DTOs.ExamDTO;
using ExamonimyWeb.Models.DTOs.RoleDTO;
using ExamonimyWeb.Models.DTOs.UserDTO;

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
        }
    }
}
