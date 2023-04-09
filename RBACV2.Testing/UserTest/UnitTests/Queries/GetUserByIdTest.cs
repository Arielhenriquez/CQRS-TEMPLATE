using AutoMapper;
using RBACV2.Application.UsersEntity.Dtos;
using RBACV2.Application.UsersEntity.Handlers.Queries;
using RBACV2.Application.UsersEntity.Mappings;
using RBACV2.Application.UsersEntity.Queries;
using RBACV2.Test.UserTest.UnitTests.Mocks;

namespace RBACV2.Test.UserTest.UnitTests.Queries
{
    public class GetUserByIdTest
    {
        [Fact]
        public async Task ShouldGetUserById()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<UsersMappingProfile>();
            });

            var mapper = new Mapper(config);
            var mockedRepository = MockedUserRepository.GetUserByIdMockQuery();
            var query = new GetUserByIdQuery() { Id = Guid.Empty };
            var handler = new GetUserByIdQueryHandler(mapper, mockedRepository.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
            Assert.IsType<GetUserByIdDto>(result);
            Assert.Equal(result.Id, Guid.Empty);
        }
    }
}
