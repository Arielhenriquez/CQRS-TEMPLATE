using Microsoft.AspNetCore.Mvc;
using RBACV2.Application.Common.Interfaces.Abstract;
using RBACV2.Domain.Entities.UserEntity;
using Swashbuckle.AspNetCore.Annotations;

namespace RBACV2.API.Controllers.User
{
    [ApiController]
    [Route("api/users")]
    public class AdUserController : BaseController
    {
        public AdUserController(IAdUserService adUserService)
        {
            _adUserService = adUserService;
        }

        [HttpGet("current")]
        [SwaggerOperation(
                Summary = "Gets the current user logged in",
            Description = "This should be the first endpoint to hit when calling this API.")]
        public async Task<AdUser> GetCurrentUser()
        {
            var currentUser = await _adUserService.Current();
            return currentUser;
        }
    }
}
