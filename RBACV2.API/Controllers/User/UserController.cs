using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RBACV2.Application.Common.Exceptions;
using RBACV2.Application.Common.PaginationQuery;
using RBACV2.Application.UsersEntity.Commands;
using RBACV2.Application.UsersEntity.Queries;
using RBACV2.Domain.BaseResponse;
using RBACV2.Infrastructure.Services.PermissionsHandler;
using Swashbuckle.AspNetCore.Annotations;

namespace RBACV2.API.Controllers.User
{
    [Route("api/user")]
    [ApiController]
    public class UserController : BaseController
    {
        [Permission("users.read")]
        [HttpGet]
        [SwaggerOperation(
            Summary = "Gets users in the database")]
        public async Task<IActionResult> Get([FromQuery] PaginationQuery paginationQuery)
        {
            try
            {
                var query = new GetFilteredUsersQuery(paginationQuery);
                var result = await Mediator.Send(query);
                return Ok(BaseResponse.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(BaseResponse.BadRequest(ex.Message));
            }
        }

        //[Permission("users.read")]
        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Gets users in the database by id")]
        public async Task<IActionResult> FindById([FromRoute] Guid id)
        {
            var query = new GetUserByIdQuery { Id = id };
            var result = await Mediator.Send(query);
            return Ok(BaseResponse.Ok(result));
        }

        //[Permission("users.write")]
        [HttpPost]
        [SwaggerOperation(
           Summary = "Creates a new user")]
        public async Task<IActionResult> Post([FromBody] CreateUserCommand command)
        {
            var result = await Mediator.Send(command);
            return CreatedAtRoute(new { id = result.Id }, BaseResponse.Created(result));
        }

        //[Permission("users.write")]
        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Updates existing user")]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] UpdateUserCommand command)
        {
            try
            {
                command.Id = id;
                var result = await Mediator.Send(command);
                return Ok(BaseResponse.Updated(result));
            }
            catch (NotFoundException) { throw; }

            catch (Exception ex)
            {
                return BadRequest(BaseResponse.BadRequest(ex.Message));
            }
        }

        //[Permission("users.write")]
        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Deletes user")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var command = new DeleteUserCommand(id)
            {
                Id = id
            };

            var result = await Mediator.Send(command);
            return Ok(BaseResponse.Deleted(result));
        }
    }
}
