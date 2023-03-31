using Microsoft.AspNetCore.Authorization;
using RBACV2.Domain.Constants;

namespace RBACV2.Infrastructure.Services.PermissionsHandler
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            bool hasPermission = context.User.Claims.Any(c => c.Type == Claims.RoleClaim
            && requirement.Permission.Split(",").Contains(c.Value));

            if (hasPermission)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
