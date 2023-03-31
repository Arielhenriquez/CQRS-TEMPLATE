using RBACV2.Domain.Base;
using RBACV2.Domain.Entities.PermissionsEntity;

namespace RBACV2.Domain.Entities
{
    public class Roles : BaseEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool? Status { get; set; }
        public Guid? ApplicationId { get; set; }
        public virtual Applications? Application { get; set; }
        public virtual ICollection<PermissionsToRoles>? PermissionsToRoles { get; set; }
    }
}