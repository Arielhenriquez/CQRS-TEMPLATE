using MockQueryable.Moq;
using Moq;
using RBACV2.Application.Common.Interfaces.Repositories;
using RBACV2.Domain.Entities.UserEntity;
using RBACV2.Test.UserTest.FakeData;

namespace RBACV2.Test.UserTest.UnitTests.Mocks
{
    public class MockedUserRepository
    {
        public struct MockService
        {
            public IQueryable<Users> queryableEntity;
            public Mock<IUserRepository> mockRepository;
            public MockService(IQueryable<Users> entities)
            {
                queryableEntity = entities.AsQueryable().BuildMock();
                mockRepository = new Mock<IUserRepository>();
            }
        }

        private static MockService Mocks = new(UserData.ListUsers.AsQueryable().BuildMock());

        public static Mock<IUserRepository> GetUsersMockQuery()
        {
            Mocks.mockRepository.Setup(m => m.Query()).Returns(Mocks.queryableEntity);
            return Mocks.mockRepository;
        }

        public static Mock<IUserRepository> GetUserByIdMockQuery()
        {
            Guid guid = Guid.Empty;

            Mocks.mockRepository.Setup(x => x.Get(guid))
             .Returns(Task.FromResult(UserData.ListUsers.FirstOrDefault(find => find.Id == guid))!);

            Mocks.mockRepository.Setup(x => x.Query())
                .Returns(UserData.ListUsers.AsQueryable().BuildMock());

            return Mocks.mockRepository;
        }

        public static Mock<IUserRepository> CreateUserCommandMock()
        {
            Mocks.mockRepository.Setup(x => x.Add(It.IsAny<Users>()))
                .Callback((Users user) => UserData.CreateListUsers.Add(user))
                .Returns(Task.FromResult(UserData.CreateListUsers.LastOrDefault())!);

            Mocks.mockRepository.Setup(x => x.Query()).Returns(Mocks.queryableEntity);
            return Mocks.mockRepository;
        }
        public static Mock<IUserRepository> UpdateUserCommandMock()
        {
            var queryableUsersService = UserData.CreateListUsers.AsQueryable().BuildMock();
            Mocks.mockRepository.Setup(x => x.Update(It.IsAny<Users>()))
                .Callback((Users entity) =>
                {
                    var user = UserData.CreateListUsers.FirstOrDefault(find => find.Id == entity.Id);
                    user!.FullName = entity.FullName;
                })
                .Returns(Task.FromResult(UserData.CreateListUsers.LastOrDefault())!);

            Mocks.mockRepository.Setup(x => x.Query()).Returns(queryableUsersService);
            return Mocks.mockRepository;
        }

        public static Mock<IUserRepository> DeleteUserCommandMock()
        {
            Guid guid = Guid.Empty;
            Mocks.mockRepository.Setup(x => x.Delete(guid))
            .Returns(Task.FromResult(UserData.ListUsers.FirstOrDefault(find => find.Id == guid))!);

            Mocks.mockRepository.Setup(x => x.Query())
                .Returns(UserData.ListUsers.AsQueryable().BuildMock());

            return Mocks.mockRepository;
        }
    }
}

