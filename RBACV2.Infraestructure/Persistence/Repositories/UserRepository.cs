using RBACV2.Application.Common.Interfaces.Abstract;
using RBACV2.Application.Common.Interfaces.Repositories;
using RBACV2.Domain.Entities.UserEntity;
using RBACV2.Infrastructure.Persistence.Context;

namespace RBACV2.Infrastructure.Persistence.Repositories
{
    public class UserRepository : BaseRepository<Users>, IUserRepository
    {
        public UserRepository(IApplicationDbContext context, IAdUserService userService) : base(context, userService)
        {
        }
    }
}
