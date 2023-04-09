using Microsoft.EntityFrameworkCore;
using RBACV2.Application.UsersEntity.Commands;
using RBACV2.Application.UsersEntity.Queries;
using RBACV2.Test.UserTest.FakeData;
using RBACV2.Test.UserTest.IntegrationTests.Fixtures;

namespace RBACV2.Test.UserTest.IntegrationTests.DatabaseTests
{
    [Collection("Database collection")]
    public class DeleteUserFromDatabaseTest : DatabaseFixture
    {
        [Fact]
        public async Task ShouldDeleteUserFromDatabase()
        {
            var user = UserData.SingleUser;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var deleteUser = new DeleteUserCommand(user.Id);
            await _mediator.Send(deleteUser, CancellationToken.None);

            var savedUser = await _context.Users.FindAsync(user.Id);
            Assert.NotNull(savedUser);
        }
    }

}
