using RBACV2.Application.Common.PaginationQuery;
using RBACV2.Application.UsersEntity.Queries;
using RBACV2.Test.UserTest.FakeData;
using RBACV2.Test.UserTest.IntegrationTests.Fixtures;
using Xunit;

namespace RBACV2.Test.UserTest.IntegrationTests.DatabaseTests
{
    [Collection("Database collection")]
    public class GetUsersFromDbTest : DatabaseFixture
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

            Assert.NotNull(result);
            Assert.Equal(users.Count, result.TotalRecords);
        }
    }
}

