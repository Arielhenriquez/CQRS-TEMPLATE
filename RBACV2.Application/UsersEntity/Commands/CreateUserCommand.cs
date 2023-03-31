using MediatR;
using RBACV2.Application.Common.GenericHandler;
using RBACV2.Application.UsersEntity.Dtos;
using RBACV2.Domain.Entities.UserEntity;
using System.ComponentModel;

namespace RBACV2.Application.UsersEntity.Commands
{
    public class CreateUserCommand : BaseCommand<UserResponseDto>
    {
        public string FirstName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        [DefaultValue(false)]
        public bool IsOrganizationAdmin { get; set; }
        [DefaultValue(true)]
        public bool IsEnabled { get; set; }
        public Guid? ApplicationId { get; set; }
        public Guid? RoleId { get; set; }
        public UserPasswordProfile? PasswordProfile { get; set; }
    }
}
