using Microsoft.AspNetCore.Authorization;

namespace RBACV2.Infrastructure.Services.PermissionsHandler
{
    public class PermissionAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="permission"></param>
        public PermissionAttribute(string permission)
        {
            Permission = permission;
        }

        /// <summary>
        /// 
        /// </summary>
        private string Permission
        {
            init => Policy = value;
        }
    }
}
