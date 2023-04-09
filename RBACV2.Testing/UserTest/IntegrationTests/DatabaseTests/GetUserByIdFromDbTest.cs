using RBACV2.Application.UsersEntity.Queries;
using RBACV2.Test.UserTest.FakeData;
using RBACV2.Test.UserTest.IntegrationTests.Fixtures;

namespace RBACV2.Test.UserTest.IntegrationTests.DatabaseTests
{
    [Collection("Database collection")]
    public class GetUserByIdFromDbTest : DatabaseFixture
    {
        [Fact]
        public async Task ShouldGetUsersFromTheDatabase()
        {
            var user = UserData.SingleUser;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var query = new GetUserByIdQuery() { Id = user.Id };
            var result = await _mediator!.Send(query, CancellationToken.None);
            var savedUser = await _context.Users.FindAsync(user.Id);

            Assert.NotNull(savedUser);
            Assert.NotNull(result);
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
