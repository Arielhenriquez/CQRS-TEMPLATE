using AutoMapper;
using RBACV2.Application.UsersEntity.Dtos;
using RBACV2.Application.UsersEntity.Handlers.Commands;
using RBACV2.Application.UsersEntity.Mappings;
using RBACV2.Domain.Entities.UserEntity;
using RBACV2.Test.UserTest.FakeData;
using RBACV2.Test.UserTest.UnitTests.Mocks;

namespace RBACV2.Test.UserTest.UnitTests.Commands
{
    public class DeleteUserTest
    {
        [Fact]
        public async Task ShouldDeleteUserById()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<UsersMappingProfile>();
            });

            var command = UserData.DeleteUserCommand;

            var mapper = new Mapper(config);
            var mockedRepository = MockedUserRepository.DeleteUserCommandMock();

            var handler = new DeleteUserCommandHandler(mockedRepository.Object, mapper);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.NotNull(result);
            Assert.IsType<UserResponseDto>(result);
            Assert.Equal(result.Id, Guid.Empty);
        }
    }
}
