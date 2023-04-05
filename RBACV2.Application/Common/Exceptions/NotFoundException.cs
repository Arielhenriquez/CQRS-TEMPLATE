using Response = RBACV2.Domain.BaseResponse.BaseResponse;

namespace RBACV2.Application.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public Response Response { get; }

        public NotFoundException(string entityName, Guid id)
          : base($"{entityName} with id {id} not found")
        {
            Response = Response.NotFound(Message);
        }
    }
}
