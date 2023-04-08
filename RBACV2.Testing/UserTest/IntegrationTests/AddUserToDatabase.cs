using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RBACV2.Application;
using RBACV2.Application.Common.Interfaces.Abstract;
using RBACV2.Application.Common.Interfaces.Repositories;
using RBACV2.Infrastructure.Persistence.Context;
using RBACV2.Infrastructure.Persistence.Repositories;
using RBACV2.Infrastructure.Services.GraphProviders;
using RBACV2.Test.UserTest.FakeData;

namespace RBACV2.Test.UserTest.IntegrationTests
{
    public class AddUserToDatabase : Fixtures
    {
        [Fact]
        public async Task ShouldAddUserToDatabase()
        {
            var user = UserData.CreateCommand;
            await _mediator!.Send(user, CancellationToken.None);

            var savedUser = await _context.Users.FindAsync(user.Id);
            Assert.NotNull(savedUser);
            Assert.NotNull(user);
            Assert.Equal(user.Id, savedUser.Id);
            Assert.Equal(user.FirstName, savedUser.FirstName);
            Assert.Equal(user.FullName, savedUser.FullName);
            Assert.Equal(user.UserName, savedUser.UserName);
            Assert.Equal(user.IsEnabled, savedUser.IsEnabled);
            Assert.Equal(user.ApplicationId, savedUser.ApplicationId);
            Assert.Equal(user.RoleId, savedUser.RoleId);
        }
    }


}
