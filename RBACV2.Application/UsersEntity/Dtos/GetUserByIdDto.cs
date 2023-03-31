using RBACV2.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBACV2.Application.UsersEntity.Dtos
{
    public class GetUserByIdDto
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
        public Applications? Application { get; set; }
        public Guid? ApplicationId { get; set; }
        public Guid? RoleId { get; set; }
        public Roles? Role { get; set; }
    }
}
