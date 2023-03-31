using RBACV2.Application.Common.GenericHandler;
using RBACV2.Domain.Entities.UserEntity;
using System.ComponentModel;

namespace RBACV2.Application.UsersEntity.Commands
{
    public class AzureADUserCommand : BaseCommand<Users>
    {
        public string? FirstName { get; set; }
        public new Guid? Id { get; set; }
        public string? FullName { get; set; }
        public string? UserName { get; set; }
        public string? FullEmail { get; set; }
        [DefaultValue(false)]
        public bool? IsOrganizationAdmin { get; set; }
        [DefaultValue(true)]
        public bool? IsEnabled { get; set; }
        public UserPasswordProfile? PasswordProfile { get; set; }
        public string? UserOid { get; set; }
        //public OrganizationDto? Organization { get; set; }
        //public Guid? OrganizationId { get; set; }
        //public Guid? RoleId { get; set; }
        //public RoleDto? Role { get; set; }
    }
}
