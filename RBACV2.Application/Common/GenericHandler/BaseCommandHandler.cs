using AutoMapper;
using MediatR;
using RBACV2.Application.Common.Interfaces.Repositories;
using RBACV2.Domain.Base;
using RBACV2.Domain.Enums;

namespace RBACV2.Application.Common.GenericHandler
{
    public class BaseCommandHandler<TCommand, TEntity, TResponse>
     : IRequestHandler<TCommand, TResponse>
     where TCommand : BaseCommand<TResponse>
     where TEntity : BaseEntity
     where TResponse : class
    {
        protected readonly IBaseRepository<TEntity> _baseRepository;
        protected readonly IMapper _mapper;

        public BaseCommandHandler(IBaseRepository<TEntity> baseRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }

        public virtual async Task<TResponse> Handle(TCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<TEntity>(request);

            var operationResult = request.ActionType switch
            {
                ActionsTypes.Create => await _baseRepository.Add(entity),
                ActionsTypes.Update => await _baseRepository.Update(entity),
                ActionsTypes.Delete => await _baseRepository.Delete(entity.Id),
                _ => throw new Exception($"Action Type '{request.ActionType}' is not supported, you can only Create, Update or Delete")
            };

            return _mapper.Map<TResponse>(operationResult);
        }
    }
}