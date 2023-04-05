using AutoMapper;
using RBACV2.Application.UsersEntity.Dtos;
using RBACV2.Application.UsersEntity.Handlers.Commands;
using RBACV2.Application.UsersEntity.Mappings;
using RBACV2.Domain.Entities.UserEntity;
using RBACV2.Testing.UserTest.FakeData;
using RBACV2.Testing.UserTest.Mocks;

namespace RBACV2.Testing.UserTest.Commands
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

            var command = UserData.DeleteCommand;

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
