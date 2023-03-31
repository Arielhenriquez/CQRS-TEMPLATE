using RBACV2.Application.Common.GenericHandler;
using RBACV2.Application.UsersEntity.Dtos;
using RBACV2.Domain.Enums;

namespace RBACV2.Application.UsersEntity.Commands
{
    public class UpdateUserCommand : BaseCommand<UserResponseDto>
    {
        public string? FirstName { get; set; }
        public string? FullName { get; set; }
        public bool? IsOrganizationAdmin { get; set; }
        public bool? IsEnabled { get; set; }
        public UpdateUserCommand()
        {
            ActionType = ActionsTypes.Update;
        }
    }
}
