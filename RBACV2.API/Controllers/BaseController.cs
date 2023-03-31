using MediatR;
using Microsoft.AspNetCore.Mvc;
using RBACV2.Application.Common.Interfaces.Abstract;
using System.Net;

namespace RBACV2.API.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class BaseController : ControllerBase
    {
        private IMediator _mediator;
        protected IAdUserService _adUserService;

        //Uncomment this when Authentication is ready
        //protected IAdUserService AdUserService => _adUserService ??= HttpContext.RequestServices.GetService<IAdUserService>()!;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>()!;

        protected new IActionResult Response(object data)
        {
            return Ok(new
            {
                Status = HttpStatusCode.OK,
                Message = "Success",
                Data = data,
            });
        }

        protected IActionResult ErrorResponse(Exception exception)
        {
            return BadRequest(new
            {
                Status = HttpStatusCode.BadRequest,
                exception.Message,
            });
        }
    }
}
