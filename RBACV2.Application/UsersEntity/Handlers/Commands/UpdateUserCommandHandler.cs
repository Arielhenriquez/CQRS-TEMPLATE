using AutoMapper;
using MediatR;
using Microsoft.Graph;
using RBACV2.Application.Common.Interfaces.Abstract;
using RBACV2.Application.Common.Interfaces.Repositories;
using RBACV2.Application.UsersEntity.Commands;
using RBACV2.Application.UsersEntity.Dtos;
using RBACV2.Domain.Entities.UserEntity;
using RBACV2.Domain.Enums;

namespace RBACV2.Application.UsersEntity.Handlers.Commands
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserResponseDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UpdateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserResponseDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            if (request.ActionType != ActionsTypes.Update)
                throw new ArgumentException($"Action Type '{request.ActionType}' is not supported, you can only Update");

            var user = _mapper.Map<Users>(request);
            _mapper.Map(request, user);
            var result = await _userRepository.Update(user);

            var dto = _mapper.Map<UserResponseDto>(result);
            return dto;
        }
    }
}
