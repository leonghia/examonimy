using ExamonimyWeb.DatabaseContexts;
using ExamonimyWeb.Entities;
using ExamonimyWeb.Profiles;
using ExamonimyWeb.Repositories.GenericRepository;
using ExamonimyWeb.Repositories.UserRepository;
using ExamonimyWeb.Services.AuthService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ExamonimyWeb.Extensions
{
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
                        ValidIssuer = configuration["JwtSettings:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]!)),
                        ValidateIssuer = true,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true
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
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuthService, AuthService>();
        }
    }
}
