using Microsoft.AspNetCore.Authorization;

namespace RBACV2.Infrastructure.Services.PermissionsHandler
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        /// <summary>
        /// 
        /// </summary>
        public string Permission { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="permission"></param>
        public PermissionRequirement(string permission)
        {
            Permission = permission;
        }
    }
}