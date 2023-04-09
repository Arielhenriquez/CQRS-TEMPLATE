using RBACV2.Test.UserTest.FakeData;
using RBACV2.Test.UserTest.IntegrationTests.Fixtures;

namespace RBACV2.Test.UserTest.IntegrationTests.DatabaseTests
{
    [Collection("Database collection")]
    public class UpdateUserInDatabaseTest : DatabaseFixture
    {
        [Fact]
        public async Task ShouldUpdateUserInDatabase()
        {
            var user = UserData.SingleUser;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var updatedUser = UserData.GetUpdateUserCommand(user.Id);

            await _mediator.Send(updatedUser, CancellationToken.None);

            var savedUser = await _context.Users.FindAsync(user.Id);
            Assert.NotNull(savedUser);
            Assert.Equal(user.Id, updatedUser.Id);
            Assert.NotEqual(updatedUser.FirstName, savedUser.FirstName);
            Assert.NotEqual(updatedUser.FullName, savedUser.FullName);
            Assert.Equal(user.IsEnabled, savedUser.IsEnabled);
        }
    }

}
