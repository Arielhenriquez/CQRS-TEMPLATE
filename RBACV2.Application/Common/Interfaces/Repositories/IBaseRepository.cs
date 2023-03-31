using RBACV2.Domain.Base;

namespace RBACV2.Application.Common.Interfaces.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class, IBase
    {
        IQueryable<TEntity> Query();
        Task<TEntity> Get(Guid id);
        Task<TEntity> Add(TEntity entity);
        Task<TEntity> Update(TEntity entity);
        Task<TEntity> Delete(Guid id);
    }
}
