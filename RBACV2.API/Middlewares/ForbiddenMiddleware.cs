using Newtonsoft.Json;
using RBACV2.Domain.BaseResponse;
using System.Net;

namespace RBACV2.API.Middlewares
{
    public class ForbiddenMiddleware
    {
        private readonly RequestDelegate _next;

        public ForbiddenMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await _next(context);

            if (context.Response.StatusCode == (int)HttpStatusCode.Forbidden)
            {
                var errorMessage = "You dont have permission to use this endpoint";
                var forbiddenResponse = BaseResponse.Forbidden(errorMessage);

                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(forbiddenResponse));
            }
        }
    }
}
