namespace RBACV2.Application.UsersEntity.Dtos
{
    public class GetUsersDto
    {
        public string? FirstName { get; set; }
        public Guid Id { get; set; }
        public string? FullName { get; set; }
        public string? UserName { get; set; }
        public string? FullEmail { get; set; }
        public bool? IsOrganizationAdmin { get; set; }
        public bool? IsEnabled { get; set; }
        public Guid ApplicationId { get; set; }
        public string? UserOid { get; set; }
        //public RoleDto? Role { get; set; }
        //public Guid RoleId { get; set; }
    }
}
