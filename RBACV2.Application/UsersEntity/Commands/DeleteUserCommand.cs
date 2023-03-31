using RBACV2.Application.Common.GenericHandler;
using RBACV2.Application.UsersEntity.Dtos;
using RBACV2.Domain.Enums;

namespace RBACV2.Application.UsersEntity.Commands
{
    public class DeleteUserCommand : BaseCommand<UserResponseDto>
    {
        public new Guid Id { get; set; }
        public DeleteUserCommand(Guid id)
        {
            Id = id;
            ActionType = ActionsTypes.Delete;
        }
    }
}
