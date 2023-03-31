using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RBACV2.Application.Common.Interfaces.Abstract;
using RBACV2.Domain.Constants;
using RBACV2.Domain.Entities;
using RBACV2.Domain.Entities.UserEntity;
using RBACV2.Infrastructure.Persistence.Context;
using System.Security.Claims;

namespace RBACV2.Infrastructure.Services.GraphProviders
{
    public class AdUserService : IAdUserService
    {
        private readonly DbSet<Users> _db;
        private readonly IHttpContextAccessor _accessor;

        public AdUserService(IHttpContextAccessor accessor, IApplicationDbContext dbContext)
        {
            _db = dbContext.Set<Users>();
            _accessor = accessor;
        }

        private async Task<Applications> GetUserOganizationId(Guid id)
        {
            var Id = id.ToString();
            var application = await _db.Where(x => x.UserOid!.Equals(Id)).Select(x => x.Application)
                .FirstOrDefaultAsync();

            return application!;
        }

        public async Task<AdUser> Current()
        {
            var userDto = new AdUser();
            if (_accessor.HttpContext == null) return userDto;

            var user = _accessor.HttpContext.User;
            var claims = user.Claims.ToList();

            var adUser = new AdUser()
            {
                Oid = user.Claims
                    .FirstOrDefault(x => x.Type == Claims.ObjectIdentifier)?.Value!,
                Name = user.FindFirst(ClaimTypes.GivenName)?.Value!,
                Email = user.FindFirst(ClaimTypes.Email)?.Value!,
                Permissions = user.FindAll(ClaimTypes.Role)
                       .Select(claim => claim.Value)
                       .ToList(),
            };

            if (adUser.Oid is null)
                throw new ArgumentException("User is not autheticated");

            var organization = await GetUserOganizationId(Guid.Parse(adUser.Oid));

            if (organization != null)
            {
                adUser.OrganizationId = organization.Id;
                adUser.OrganizationName = organization.DisplayName;
                //adUser.IsGlobalOrganization = organization.IsGlobalOrganization;
            }

            return adUser;
        }
    }
}
