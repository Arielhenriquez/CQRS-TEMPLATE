using RBACV2.Application.Common.PaginationQuery;
using RBACV2.Application.UsersEntity.Queries;
using RBACV2.Test.UserTest.FakeData;
using Xunit;

namespace RBACV2.Test.UserTest.IntegrationTests
{
    public class GetUsersFromTheDatabase : Fixtures
    {
        [Fact]
        public async Task ShouldGetUsersFromTheDatabase()
        {
            var users = UserData.ListUsers;
            _context.Users.AddRange(users);
            await _context.SaveChangesAsync();


            var paginationQuery = new PaginationQuery() { PageNumber = 1, PageSize = 10, Search = "Test" };
            var query = new GetFilteredUsersQuery(paginationQuery);
            var result = await _mediator!.Send(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(users.Count, result.TotalRecords);

            //foreach (var user in users)
            //{
            //    Assert.NotNull(savedUser);
            //    Assert.Equal(user.Id, savedUser.Id);
            //    Assert.Equal(user.FirstName, savedUser.FirstName);
            //    Assert.Equal(user.FullName, savedUser.FullName);
            //    Assert.Equal(user.UserName, savedUser.UserName);
            //    Assert.Equal(user.IsEnabled, savedUser.IsEnabled);
            //    Assert.Equal(user.ApplicationId, savedUser.ApplicationId);
            //    Assert.Equal(user.RoleId, savedUser.RoleId);
            //}
        }
    }
}

