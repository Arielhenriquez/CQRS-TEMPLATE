using AutoMapper;
using RBACV2.Application.UsersEntity.Dtos;
using RBACV2.Application.UsersEntity.Handlers.Commands;
using RBACV2.Application.UsersEntity.Mappings;
using RBACV2.Test.UserTest.FakeData;
using RBACV2.Test.UserTest.Mocks;

namespace RBACV2.Test.UserTest.Commands
{
    public class CreateUserTest
    {
        [Fact]
        public async Task ShouldCreateUser()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<UsersMappingProfile>();
            });

            var mapper = new Mapper(config);
            var command = UserData.CreateUserCommand;
            var mockedRepository = MockedUserRepository.CreateUserCommandMock();

            var handler = new CreateUserCommandHandler(mockedRepository.Object, mapper);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.NotNull(result);
            Assert.IsType<UserResponseDto>(result);
            Assert.Equal(result.Id, Guid.Empty);
        }
    }
}
