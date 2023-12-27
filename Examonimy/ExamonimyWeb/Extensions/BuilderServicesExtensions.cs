using ExamonimyWeb.DatabaseContexts;
using ExamonimyWeb.Entities;
using ExamonimyWeb.Managers.ExamPaperManager;
using ExamonimyWeb.Managers.QuestionManager;
using ExamonimyWeb.Managers.UserManager;
using ExamonimyWeb.Profiles;
using ExamonimyWeb.Repositories.GenericRepository;
using ExamonimyWeb.Services.AuthService;
using ExamonimyWeb.Services.NotificationService;
using ExamonimyWeb.Services.TokenService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
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

        services.AddScoped<IGenericRepository<User>, GenericRepository<User>>();
        services.AddScoped<IGenericRepository<Course>, GenericRepository<Course>>();
        services.AddScoped<IGenericRepository<Question>, GenericRepository<Question>>();
        services.AddScoped<IGenericRepository<QuestionType>, GenericRepository<QuestionType>>();
        services.AddScoped<IGenericRepository<QuestionLevel>, GenericRepository<QuestionLevel>>();
        services.AddScoped<IGenericRepository<MultipleChoiceQuestionWithOneCorrectAnswer>, GenericRepository<MultipleChoiceQuestionWithOneCorrectAnswer>>();
        services.AddScoped<IGenericRepository<MultipleChoiceQuestionWithMultipleCorrectAnswers>, GenericRepository<MultipleChoiceQuestionWithMultipleCorrectAnswers>>();
        services.AddScoped<IGenericRepository<TrueFalseQuestion>, GenericRepository<TrueFalseQuestion>>();
        services.AddScoped<IGenericRepository<ShortAnswerQuestion>, GenericRepository<ShortAnswerQuestion>>();
        services.AddScoped<IGenericRepository<FillInBlankQuestion>, GenericRepository<FillInBlankQuestion>>();
        services.AddScoped<IGenericRepository<ExamPaper>, GenericRepository<ExamPaper>>();
        services.AddScoped<IGenericRepository<ExamPaperQuestion>, GenericRepository<ExamPaperQuestion>>();
        services.AddScoped<IGenericRepository<ExamPaperReviewer>, GenericRepository<ExamPaperReviewer>>();
        services.AddScoped<IGenericRepository<Notification>, GenericRepository<Notification>>();
        services.AddScoped<IGenericRepository<NotificationReceiver>, GenericRepository<NotificationReceiver>>();

        
        services.AddScoped<IUserManager, UserManager>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITokenService, JwtTokenService>();
        services.AddScoped<IQuestionManager, QuestionManager>();
        services.AddScoped<IExamPaperManager, ExamPaperManager>();
        services.AddScoped<INotificationService, InAppNotificationService>();

        services.AddSignalR();
    }
}
