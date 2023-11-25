using ExamonimyWeb.DatabaseContexts;
using ExamonimyWeb.Profiles;
using Microsoft.EntityFrameworkCore;

namespace ExamonimyWeb.Extensions
{
    public static class BuilderServicesExtensions
    {
        public static void Configure(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddControllersWithViews();

            services
                .AddDbContext<ExamonimyContext>(optionsAction =>
            {
                optionsAction.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            })
                .AddAutoMapper(typeof(AutoMapperProfile));
        }
    }
}
