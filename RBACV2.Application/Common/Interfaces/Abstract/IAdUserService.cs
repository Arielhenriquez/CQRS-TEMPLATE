using RBACV2.Domain.Entities.UserEntity;

namespace RBACV2.Application.Common.Interfaces.Abstract
{
    public interface IAzureAdUserGetService
    {
        Task<Guid?> GetUserOganizationId(Guid id);
    }

    public interface IAdUserService
    {
        Task<AdUser> Current();
    }
}
