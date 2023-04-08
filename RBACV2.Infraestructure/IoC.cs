using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RBACV2.Application.Common.Interfaces.Abstract;
using RBACV2.Application.Common.Interfaces.Repositories;
using RBACV2.Infrastructure.Persistence.Context;
using RBACV2.Infrastructure.Persistence.Repositories;
using RBACV2.Infrastructure.Services.GraphProviders;

namespace RBACV2.Infrastructure
{
    public static class IoC
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IApplicationDbContext, ApplicationDbContext>();
            services.AddTransient<IAdUserService, AdUserService>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IAzureADUserProvider, AzureADUserProvider>();

            services.AddDbContext<ApplicationDbContext>(options
                 => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}
