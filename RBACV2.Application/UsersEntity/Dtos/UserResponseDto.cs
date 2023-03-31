using RBACV2.Domain.Entities.UserEntity;
using System.ComponentModel;

namespace RBACV2.Application.UsersEntity.Dtos
{
    public class UserResponseDto
    {
        public string? FirstName { get; set; }
        public Guid Id { get; set; }
        public string? FullName { get; set; }
        public string? UserName { get; set; }
        public string? FullEmail { get; set; }
        [DefaultValue(false)]
        public bool? IsOrganizationAdmin { get; set; }
        [DefaultValue(true)]
        public bool? IsEnabled { get; set; }
        public UserPasswordProfile? PasswordProfile { get; set; }
        public string? UserOid { get; set; }
        //public OrganizationDto Organization { get; set; }
        //public Guid OrganizationId { get; set; }
        //public RoleDto? Role { get; set; }
        //public Guid RoleId { get; set; }
    }
}
