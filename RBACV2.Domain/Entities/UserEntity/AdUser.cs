using RBACV2.Domain.Base;

namespace RBACV2.Domain.Entities.UserEntity
{
    public class AdUser : BaseEntity
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public ICollection<string>? Permissions { get; set; }
        public string? Oid { get; set; }
        public Guid? OrganizationId { get; set; }
        public string? OrganizationName { get; set; }
        public bool IsGlobalOrganization { get; set; }
    }
}
