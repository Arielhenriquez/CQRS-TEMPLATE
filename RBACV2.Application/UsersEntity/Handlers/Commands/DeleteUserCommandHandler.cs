using AutoMapper;
using MediatR;
using RBACV2.Application.Common.Interfaces.Abstract;
using RBACV2.Application.Common.Interfaces.Repositories;
using RBACV2.Application.UsersEntity.Commands;
using RBACV2.Application.UsersEntity.Dtos;

namespace RBACV2.Application.UsersEntity.Handlers.Commands
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, UserResponseDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public DeleteUserCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserResponseDto> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _userRepository.Delete(request.Id);

            var dto = _mapper.Map<UserResponseDto>(result);

            return dto;
        }
    }
}
