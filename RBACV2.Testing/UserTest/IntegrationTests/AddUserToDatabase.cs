using RBACV2.Test.UserTest.FakeData;

namespace RBACV2.Test.UserTest.IntegrationTests
{
    public class AddUserToDatabase : Fixtures
    {
        [Fact]
        public async Task ShouldAddUserToDatabase()
        {
            var user = UserData.CreateUserCommand;
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
