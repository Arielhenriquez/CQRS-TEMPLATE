using RBACV2.Domain.Base;

namespace RBACV2.Domain.Entities.PermissionsEntity
{
    public class Permissions : BaseEntity
    {
        public Guid? AppPermissionOid { get; set; }
        public string? AppId { get; set; }
        public string? AppOid { get; set; }
        public Guid? AzureADAppId { get; set; }
        public string? DisplayName { get; set; }
        public string? Description { get; set; }
        public string? Value { get; set; }
        public bool? IsEnabled { get; set; }
        public virtual Applications? Applications { get; set; }
        public virtual ICollection<PermissionsToRoles>? PermissionsToRoles { get; set; }
    }
}
