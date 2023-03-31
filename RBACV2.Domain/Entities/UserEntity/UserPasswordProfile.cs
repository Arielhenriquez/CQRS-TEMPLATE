using System.ComponentModel;

namespace RBACV2.Domain.Entities.UserEntity
{
    public class UserPasswordProfile
    {
        public string? Password { get; set; }
        [DefaultValue(true)]
        public bool? ForceChangePassword { get; set; }
    }
}
