﻿using ExamonimyWeb.DatabaseContexts;
using ExamonimyWeb.Entities;
using ExamonimyWeb.Managers.ExamManager;
using ExamonimyWeb.Managers.ExamPaperManager;
using ExamonimyWeb.Managers.QuestionManager;
using ExamonimyWeb.Managers.UserManager;
using ExamonimyWeb.Profiles;
using ExamonimyWeb.Repositories;
using ExamonimyWeb.Services.AuthService;
using ExamonimyWeb.Services.NotificationService;
using ExamonimyWeb.Services.TokenService;
using ExamonimyWeb.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ExamonimyWeb.Extensions;

public static class BuilderServicesExtensions
{
    public static void Configure(this IServiceCollection services, IConfiguration configuration)
    {
      
        services
            .AddAuthentication(configureOptions =>
            {
                configureOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                configureOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;                   
            })
            .AddJwtBearer(configureOptions =>
            {
                configureOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = configuration["JwtConfigurations:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtConfigurations:Key"]!)),
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true
                };

                configureOptions.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies[configuration["JwtConfigurations:AccessTokenName"]!];
                        return Task.CompletedTask;
                    }
                };
            });

        services
            .AddAuthorization()
            .AddDbContext<ExamonimyContext>(optionsAction =>
            {
                optionsAction.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            })
            .AddAutoMapper(typeof(AutoMapperProfile));
            

        services              
            .AddControllersWithViews()
            .AddJsonOptions(configure =>
            {
                configure.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                configure.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                configure.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
            })
            .ConfigureApiBehaviorOptions(setupAction =>
            {
                setupAction.InvalidModelStateResponseFactory = context =>
                {
                    // Create a validation problem details object
                    var problemDetailsFactory = context.HttpContext.RequestServices.GetRequiredService<ProblemDetailsFactory>();

                    var validationProblemDetails = problemDetailsFactory.CreateValidationProblemDetails(context.HttpContext, context.ModelState);

                    // Add additional info
                    validationProblemDetails.Detail = "See the errors field for details.";
                    validationProblemDetails.Instance = context.HttpContext.Request.Path;

                    // Report invalid modelstate
                    validationProblemDetails.Type = "https://datatracker.ietf.org/doc/html/rfc9110#section-15.5.21";
                    validationProblemDetails.Status = StatusCodes.Status422UnprocessableEntity;
                    validationProblemDetails.Title = "One or more validation errors occured.";

                    return new UnprocessableEntityObjectResult(validationProblemDetails)
                    {
                        ContentTypes = { "application/problem+json" }
                    };
                };

                
            });              

        services.AddTransient<IGenericRepository<User>, GenericRepository<User>>();
        services.AddTransient<IGenericRepository<Course>, GenericRepository<Course>>();
        services.AddTransient<IGenericRepository<Question>, GenericRepository<Question>>();
        services.AddTransient<IGenericRepository<QuestionType>, GenericRepository<QuestionType>>();
        services.AddTransient<IGenericRepository<QuestionLevel>, GenericRepository<QuestionLevel>>();
        services.AddTransient<IGenericRepository<MultipleChoiceQuestionWithOneCorrectAnswer>, GenericRepository<MultipleChoiceQuestionWithOneCorrectAnswer>>();
        services.AddTransient<IGenericRepository<MultipleChoiceQuestionWithMultipleCorrectAnswers>, GenericRepository<MultipleChoiceQuestionWithMultipleCorrectAnswers>>();
        services.AddTransient<IGenericRepository<TrueFalseQuestion>, GenericRepository<TrueFalseQuestion>>();
        services.AddTransient<IGenericRepository<ShortAnswerQuestion>, GenericRepository<ShortAnswerQuestion>>();
        services.AddTransient<IGenericRepository<FillInBlankQuestion>, GenericRepository<FillInBlankQuestion>>();
        services.AddTransient<IGenericRepository<ExamPaper>, GenericRepository<ExamPaper>>();
        services.AddTransient<IGenericRepository<ExamPaperQuestion>, GenericRepository<ExamPaperQuestion>>();
        services.AddTransient<IGenericRepository<ExamPaperReviewer>, GenericRepository<ExamPaperReviewer>>();
        services.AddTransient<IGenericRepository<Notification>, GenericRepository<Notification>>();
        services.AddTransient<IGenericRepository<NotificationReceiver>, GenericRepository<NotificationReceiver>>();       
        services.AddTransient<IGenericRepository<ExamPaperComment>, GenericRepository<ExamPaperComment>>();
        services.AddTransient<IGenericRepository<ExamPaperReviewHistory>, GenericRepository<ExamPaperReviewHistory>>();
        services.AddTransient<IGenericRepository<ExamPaperCommit>, GenericRepository<ExamPaperCommit>>();
        services.AddTransient<IGenericRepository<Exam>, GenericRepository<Exam>>();
        services.AddTransient<IGenericRepository<MainClass>, GenericRepository<MainClass>>();
        services.AddTransient<IGenericRepository<ExamMainClass>, GenericRepository<ExamMainClass>>();
        services.AddTransient<IGenericRepository<Student>, GenericRepository<Student>>();
        
        services.AddScoped<IUserManager, UserManager>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITokenService, JwtTokenService>();
        services.AddScoped<IQuestionManager, QuestionManager>();
        services.AddScoped<IExamPaperManager, ExamPaperManager>();
        services.AddScoped<INotificationService, InAppNotificationService>();
        services.AddScoped<IExamManager, ExamManager>();

        services.AddSingleton<IUserIdProvider, UsernameBasedUserIdProvider>();

        services.AddSignalR();
    }
}
