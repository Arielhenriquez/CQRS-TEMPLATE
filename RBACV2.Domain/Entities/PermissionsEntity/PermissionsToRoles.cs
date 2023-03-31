using RBACV2.Domain.Base;

namespace RBACV2.Domain.Entities.PermissionsEntity
{
    public class PermissionsToRoles : BaseEntity
    {
        public Guid PermissionId { get; set; }
        public Guid RoleId { get; set; }
        public virtual Permissions? Permission { get; set; }
        public virtual Roles? Role { get; set; }
    }
}
