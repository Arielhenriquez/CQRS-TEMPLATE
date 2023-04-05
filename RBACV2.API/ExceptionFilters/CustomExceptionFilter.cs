using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using RBACV2.Application.Common.Exceptions;

namespace RBACV2.API.ExceptionFilters
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is NotFoundException notFoundException)
            {
                context.Result = new ObjectResult(notFoundException.Response)
                {
                    StatusCode = (int)notFoundException.Response.StatusCode
                };
                context.ExceptionHandled = true;
            }
            else if (context.Exception is BadRequestException badRequestException)
            {
                context.Result = new ObjectResult(badRequestException.Response)
                {
                    StatusCode = (int)badRequestException.Response.StatusCode
                };
                context.ExceptionHandled = true;
            }
        }
    }

}
