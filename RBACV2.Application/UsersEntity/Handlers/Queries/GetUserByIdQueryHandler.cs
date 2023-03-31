using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RBACV2.Application.Common.Interfaces.Repositories;
using RBACV2.Application.UsersEntity.Dtos;
using RBACV2.Application.UsersEntity.Queries;

namespace RBACV2.Application.UsersEntity.Handlers.Queries
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, GetUserByIdDto>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public GetUserByIdQueryHandler(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }
        public async Task<GetUserByIdDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _userRepository.Query()
                 .Include(x => x.Application)
                 .Include(x => x.Role)
                 .Where(x => x.Id == request.Id)
                 .FirstAsync(cancellationToken);

            if (entity == null) throw new ArgumentException($"User with id {request.Id} was not found");

            var dto = _mapper.Map<GetUserByIdDto>(entity);

            return dto;
        }
    }
}
