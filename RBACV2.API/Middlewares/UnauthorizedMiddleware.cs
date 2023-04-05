using Newtonsoft.Json;
using RBACV2.Domain.BaseResponse;
using System.Net;

namespace RBACV2.API.Middlewares
{
    public class UnauthorizedMiddleware
    {
        private readonly RequestDelegate _next;

        public UnauthorizedMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await _next(context);

            if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
            {
                var errorMessage = "Please use the login endpoint for authentication";
                var unauthorizedResponse = BaseResponse.Unauthorized(errorMessage);

                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(unauthorizedResponse));
            }
        }
    }
}
