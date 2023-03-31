using MediatR;
using RBACV2.Application.UsersEntity.Dtos;

namespace RBACV2.Application.UsersEntity.Queries
{
    public class GetUserByIdQuery : IRequest<GetUserByIdDto>
    {
        public Guid Id { get; set; }
    }
}
