using AutoMapper;
using RBACV2.Application.Common.PaginationQuery;
using RBACV2.Application.UsersEntity.Handlers.Queries;
using RBACV2.Application.UsersEntity.Mappings;
using RBACV2.Application.UsersEntity.Queries;
using RBACV2.Test.UserTest.Mocks;

namespace RBACV2.Test.UserTest.Queries
{
    public class GetFilteredUsersTest
    {
        [Fact]
        public async Task ShouldGetAllUsers()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<UsersMappingProfile>();
            });

            var mapper = new Mapper(config);
            var mockedRepository = MockedUserRepository.GetUsersMockQuery();
            var paginationQuery = new PaginationQuery() { PageNumber = 1, PageSize = 10, Search = "Test" };
            var query = new GetFilteredUsersQuery(paginationQuery);
            var handler = new GetFilteredUsersQueryHandler(mapper, mockedRepository.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
            Assert.True(result.Items.Any());
        }
    }
}
