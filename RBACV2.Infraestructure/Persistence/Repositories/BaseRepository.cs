using Microsoft.EntityFrameworkCore;
using RBACV2.Application.Common.Exceptions;
using RBACV2.Application.Common.Interfaces.Abstract;
using RBACV2.Application.Common.Interfaces.Repositories;
using RBACV2.Domain.Base;
using RBACV2.Domain.Entities;
using RBACV2.Domain.Entities.UserEntity;
using RBACV2.Infrastructure.Persistence.Context;
using System.Linq.Dynamic.Core;
using System.Reflection;

namespace RBACV2.Infrastructure.Persistence.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : class, IBase
    {
        protected readonly IDbContext _context;
        protected readonly DbSet<TEntity> _db;
        protected readonly AdUser _adUser;

        public BaseRepository(IDbContext context, IAdUserService userService)
        {
            _context = context;
            _db = context.Set<TEntity>();
            // _adUser = userService.Current().Result;
        }

        //TODO UNCOMMENT WHEN AUTHENTICATION IS READY
        //protected IQueryable<TEntity> GetQuery()
        //{
        //    if (!_adUser.IsGlobalOrganization && _adUser.OrganizationId != null)
        //    {
        //        if (typeof(Applications).IsAssignableFrom(typeof(TEntity)))
        //        {
        //            return _db.Where(x => x.Id == _adUser.OrganizationId);
        //        }
        //        else if (typeof(BaseEntity).IsAssignableFrom(typeof(TEntity)))
        //        {
        //            return _db.AsQueryable().Where($"np(as(\"{typeof(BaseEntity).FullName}\").OrganizationId) = \"{_adUser.OrganizationId}\"");
        //        }
        //    }

        //    return _db.AsQueryable();
        //}

        public virtual IQueryable<TEntity> Query()
        {
            return _db.AsQueryable().OrderByDescending(c => c.CreatedDate);
        }
        public virtual async Task<TEntity> Get(Guid id)
        {
            var entity = await Query().Where(x => x.Id.Equals(id)).FirstOrDefaultAsync();

            if (entity is null) throw new NotFoundException(typeof(TEntity).Name, id);

            return entity;
        }

        public async Task<TEntity> Add(TEntity entity)
        {
            var result = await _db.AddAsync(entity);
            await _context.SaveChangesAsync();

            return result.Entity;
        }
        public async Task<TEntity> Update(TEntity entity)
        {
            var _entity = await Get(entity.Id);
            Type type = typeof(TEntity);
            PropertyInfo[] propertyInfo = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var item in propertyInfo)
            {
                var fieldValue = item.GetValue(entity);
                if (fieldValue != null)
                {
                    item.SetValue(_entity, fieldValue);
                }
            }
            await _context.SaveChangesAsync();
            return _entity;
        }

        public async Task<TEntity> Delete(Guid id)
        {
            var entity = await Get(id);

            var result = _db.Remove(entity);
            await _context.SaveChangesAsync();

            return result.Entity;
        }
    }
}
