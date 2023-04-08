using Bogus;
using RBACV2.Application.UsersEntity.Commands;
using RBACV2.Domain.Entities.UserEntity;

namespace RBACV2.Test.UserTest.FakeData
{
    public static class UserData
    {
        public static List<Users> ListUsers { get; } = new Faker<Users>()
         .RuleFor(u => u.Id, f => Guid.Empty)
         .RuleFor(u => u.UserName, f => f.Internet.UserName())
         .RuleFor(u => u.FullName, f => f.Name.FullName())
         .RuleFor(u => u.FullEmail, f => f.Internet.Email())
         .RuleFor(u => u.RoleId, f => Guid.Empty)
         .Generate(10);

        public static List<Users> CreateListUsers { get; } = new Faker<Users>()
        .RuleFor(u => u.Id, f => Guid.Empty)
        .RuleFor(u => u.UserName, f => f.Internet.UserName())
        .RuleFor(u => u.FullName, f => f.Name.FullName())
        .RuleFor(u => u.FullEmail, f => f.Internet.Email())
        .RuleFor(u => u.RoleId, f => Guid.Empty)
        .Generate(10);

        #region Commands
        public static CreateUserCommand CreateUserCommand { get; } = new Faker<CreateUserCommand>()
            .RuleFor(u => u.Id, f => f.Random.Guid())
            .RuleFor(u => u.FirstName, f => f.Name.FirstName())
            .RuleFor(u => u.FullName, f => f.Name.FullName())
            .RuleFor(u => u.UserName, f => f.Internet.UserName())
            .RuleFor(u => u.IsOrganizationAdmin, f => f.Random.Bool())
            .RuleFor(u => u.IsEnabled, f => f.Random.Bool())
            .RuleFor(u => u.ApplicationId, f => f.Random.Guid())
            .RuleFor(u => u.RoleId, f => f.Random.Guid())
            .RuleFor(u => u.PasswordProfile, f => new UserPasswordProfile()
            {
                Password = f.Internet.Password(),
                ForceChangePassword = f.Random.Bool()
            }).Generate();

        public static UpdateUserCommand UpdateUserCommand { get; } = new Faker<UpdateUserCommand>()
            .RuleFor(u => u.Id, f => Guid.Empty)
            .RuleFor(u => u.FirstName, f => f.Name.FirstName())
            .RuleFor(u => u.FullName, f => f.Name.FullName())
            .RuleFor(u => u.IsOrganizationAdmin, f => f.Random.Bool())
            .RuleFor(u => u.IsEnabled, f => f.Random.Bool())
            .Generate();


        public static DeleteUserCommand DeleteUserCommand { get; } = new Faker<DeleteUserCommand>()
        .CustomInstantiator(f => new DeleteUserCommand(Guid.Empty))
        .Generate();

        #endregion
    }
}
