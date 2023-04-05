using Response = RBACV2.Domain.BaseResponse.BaseResponse;

namespace RBACV2.Application.Common.Exceptions
{
    public class BadRequestException : Exception
    {
        public Response Response { get; }

        public BadRequestException(string message)
            : base(message)
        {
            Response = Response.BadRequest(message);
        }
    }
}
