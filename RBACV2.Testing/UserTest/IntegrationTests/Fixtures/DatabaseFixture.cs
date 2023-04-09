using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RBACV2.Application;
using RBACV2.Application.Common.Interfaces.Abstract;
using RBACV2.Application.Common.Interfaces.Repositories;
using RBACV2.Infrastructure.Persistence.Context;
using RBACV2.Infrastructure.Persistence.Repositories;
using RBACV2.Infrastructure.Services.GraphProviders;

namespace RBACV2.Test.UserTest.IntegrationTests.Fixtures
{
    public class DatabaseFixture : IDisposable
    {
        protected readonly ApplicationDbContext _context;
        protected readonly IMediator _mediator;

        public DatabaseFixture()
        {
            var services = new ServiceCollection()
                .AddApplication()
                .AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("TestDb"))
                .AddTransient<IApplicationDbContext, ApplicationDbContext>()
                .AddTransient<IAdUserService, AdUserService>()
                .AddTransient<IUserRepository, UserRepository>()
                .AddTransient<IAzureADUserProvider, AzureADUserProvider>()
                .AddHttpContextAccessor();

            var serviceProvider = services.BuildServiceProvider();

            _context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            _mediator = serviceProvider.GetRequiredService<IMediator>();
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }

}
