﻿using AutoMapper;
using ExamonimyWeb.DTOs.CourseDTO;
using ExamonimyWeb.DTOs.ExamPaperDTO;
using ExamonimyWeb.DTOs.QuestionDTO;
using ExamonimyWeb.DTOs.RoleDTO;
using ExamonimyWeb.DTOs.UserDTO;
using ExamonimyWeb.Entities;
using ExamonimyWeb.Utilities;

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
            CreateMap<MultipleChoiceQuestionWithOneCorrectAnswer, MultipleChoiceQuestionWithOneCorrectAnswerGetDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.QuestionId))
                .ForMember(dest => dest.Course, opt => opt.MapFrom(src => src.Question!.Course))
                .ForMember(dest => dest.QuestionType, opt => opt.MapFrom(src => src.Question!.QuestionType))
                .ForMember(dest => dest.QuestionLevel, opt => opt.MapFrom(src => src.Question!.QuestionLevel))
                .ForMember(dest => dest.QuestionContent, opt => opt.MapFrom(src => src.Question!.QuestionContent))
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Question!.Author))
                .ForMember(dest => dest.CorrectAnswer, opt => opt.MapFrom(src => QuestionAnswerValueHelper.GetAnswerValueFromOneCorrectAnswer(src.CorrectAnswer)));
            CreateMap<MultipleChoiceQuestionWithMultipleCorrectAnswers, MultipleChoiceQuestionWithMultipleCorrectAnswersGetDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.QuestionId))
                .ForMember(dest => dest.Course, opt => opt.MapFrom(src => src.Question!.Course))
                .ForMember(dest => dest.QuestionType, opt => opt.MapFrom(src => src.Question!.QuestionType))
                .ForMember(dest => dest.QuestionLevel, opt => opt.MapFrom(src => src.Question!.QuestionLevel))
                .ForMember(dest => dest.QuestionContent, opt => opt.MapFrom(src => src.Question!.QuestionContent))
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Question!.Author))
                .ForMember(dest => dest.CorrectAnswers, opt => opt.MapFrom(src => QuestionAnswerValueHelper.GetAnswerValuesFromMultipleCorrectAnswers(src.CorrectAnswers)));
            CreateMap<TrueFalseQuestion, TrueFalseQuestionGetDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.QuestionId))
                .ForMember(dest => dest.Course, opt => opt.MapFrom(src => src.Question!.Course))
                .ForMember(dest => dest.QuestionType, opt => opt.MapFrom(src => src.Question!.QuestionType))
                .ForMember(dest => dest.QuestionLevel, opt => opt.MapFrom(src => src.Question!.QuestionLevel))
                .ForMember(dest => dest.QuestionContent, opt => opt.MapFrom(src => src.Question!.QuestionContent))
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Question!.Author))
                .ForMember(dest => dest.CorrectAnswer, opt => opt.MapFrom(src => QuestionAnswerValueHelper.GetAnswerValueFromTrueFalse(src.CorrectAnswer)));
            CreateMap<ShortAnswerQuestion, ShortAnswerQuestionGetDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.QuestionId))
                .ForMember(dest => dest.Course, opt => opt.MapFrom(src => src.Question!.Course))
                .ForMember(dest => dest.QuestionType, opt => opt.MapFrom(src => src.Question!.QuestionType))
                .ForMember(dest => dest.QuestionLevel, opt => opt.MapFrom(src => src.Question!.QuestionLevel))
                .ForMember(dest => dest.QuestionContent, opt => opt.MapFrom(src => src.Question!.QuestionContent))
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Question!.Author));
            CreateMap<FillInBlankQuestion, FillInBlankQuestionGetDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.QuestionId))
                .ForMember(dest => dest.Course, opt => opt.MapFrom(src => src.Question!.Course))
                .ForMember(dest => dest.QuestionType, opt => opt.MapFrom(src => src.Question!.QuestionType))
                .ForMember(dest => dest.QuestionLevel, opt => opt.MapFrom(src => src.Question!.QuestionLevel))
                .ForMember(dest => dest.QuestionContent, opt => opt.MapFrom(src => src.Question!.QuestionContent))
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Question!.Author));
            CreateMap<ExamPaper, ExamPaperGetDto>();
            CreateMap<ExamPaperCreateDto, ExamPaper>();
            CreateMap<ExamPaperQuestionCreateDto, ExamPaperQuestion>();        
        }
    }
}
