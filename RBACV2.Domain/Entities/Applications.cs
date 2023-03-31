using RBACV2.Domain.Base;
using RBACV2.Domain.Entities.PermissionsEntity;
using System.ComponentModel.DataAnnotations;

namespace RBACV2.Domain.Entities
{
    public class Applications : BaseEntity
    {
        public string? AppId { get; set; }
        [Required(ErrorMessage = $"El campo DisplayName es requerido")]
        public string? DisplayName { get; set; }
        public string? Description { get; set; }
        public string? AppOid { get; set; }
        public ICollection<Permissions>? Permissions { get; set; }
    }
}