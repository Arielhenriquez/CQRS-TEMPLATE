using Microsoft.Graph;

namespace RBACV2.Application.Common.Interfaces.Abstract
{
    public interface IAzureADUserProvider
    {
        Task<IList<User>> Get();
        Task<User> FindById(string userOid);
        Task<User> FindByName(string userPrincipalName);
        Task<User> Create(User user);
        Task<User> Update(string userOid, User user);
        Task<bool> UserPrincipalExists(string userPrincipalName);
        Task Delete(string userOid);
        Task<Stream> GetProfilePhoto(string userOid);
    }
}
