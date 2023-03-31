using MediatR;
using RBACV2.Domain.Enums;
using System.Text.Json.Serialization;

namespace RBACV2.Application.Common.GenericHandler
{
    public class BaseCommand<TResponse> : IRequest<TResponse> where TResponse : class
    {
        [JsonIgnore]

        public virtual ActionsTypes ActionType { get; set; }
        [JsonIgnore]
        public Guid Id { get; set; }
    }
}
