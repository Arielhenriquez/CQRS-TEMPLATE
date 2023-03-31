using RBACV2.Domain.Base;
using System.ComponentModel;

namespace RBACV2.Domain.Entities.UserEntity
{
    public class Users : BaseEntity
    {
        public string? FirstName { get; set; }
        public string? FullName { get; set; }
        public string? UserName { get; set; }
        public string? FullEmail { get; set; }

        [DefaultValue(true)]
        public bool? IsEnabled { get; set; }
        public string? UserOid { get; set; }
        public Applications? Application { get; set; }
        public Guid? ApplicationId { get; set; }
        public Guid? RoleId { get; set; }
        public Roles? Role { get; set; }
    }
}
