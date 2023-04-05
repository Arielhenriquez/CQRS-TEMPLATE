﻿using MockQueryable.Moq;
using Moq;
using RBACV2.Application.Common.Interfaces.Repositories;
using RBACV2.Domain.Entities.UserEntity;
using RBACV2.Testing.UserTest.FakeData;

namespace RBACV2.Testing.UserTest.Mocks
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

        private static MockService Mocks = new(UserData.Users.AsQueryable().BuildMock());

        public static Mock<IUserRepository> GetUsersMockQuery()
        {
            Mocks.mockRepository.Setup(m => m.Query()).Returns(Mocks.queryableEntity);
            return Mocks.mockRepository;
        }

        public static Mock<IUserRepository> GetUserByIdMockQuery()
        {
            Guid guid = Guid.Empty;

            Mocks.mockRepository.Setup(x => x.Get(guid))
             .Returns(Task.FromResult(UserData.Users.FirstOrDefault(find => find.Id == guid))!);

            Mocks.mockRepository.Setup(x => x.Query())
                .Returns(UserData.Users.AsQueryable().BuildMock());

            return Mocks.mockRepository;
        }

        public static Mock<IUserRepository> CreateUserCommandMock()
        {
            Mocks.mockRepository.Setup(x => x.Add(It.IsAny<Users>()))
                .Callback((Users user) => UserData.CreateUsers.Add(user))
                .Returns(Task.FromResult(UserData.CreateUsers.LastOrDefault())!);

            Mocks.mockRepository.Setup(x => x.Query()).Returns(Mocks.queryableEntity);
            return Mocks.mockRepository;
        }
        public static Mock<IUserRepository> UpdateUserCommandMock()
        {
            var queryableUsersService = UserData.CreateUsers.AsQueryable().BuildMock();
            Mocks.mockRepository.Setup(x => x.Update(It.IsAny<Users>()))
                .Callback((Users entity) =>
                {
                    var user = UserData.CreateUsers.FirstOrDefault(find => find.Id == entity.Id);
                    user!.FullName = entity.FullName;
                })
                .Returns(Task.FromResult(UserData.CreateUsers.LastOrDefault())!);

            Mocks.mockRepository.Setup(x => x.Query()).Returns(queryableUsersService);
            return Mocks.mockRepository;
        }

        public static Mock<IUserRepository> DeleteUserCommandMock()
        {
            Guid guid = Guid.Empty;
            Mocks.mockRepository.Setup(x => x.Delete(guid))
            .Returns(Task.FromResult(UserData.Users.FirstOrDefault(find => find.Id == guid))!);

            Mocks.mockRepository.Setup(x => x.Query())
                .Returns(UserData.Users.AsQueryable().BuildMock());

            return Mocks.mockRepository;
        }
    }
}

