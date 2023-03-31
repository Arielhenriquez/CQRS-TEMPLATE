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

  
    private readonly IAzureADUserProvider _azureProvider;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    public DeleteUserCommandHandler(IAzureADUserProvider azureProvider, IUserRepository userRepository, IMapper mapper)
    {
        _azureProvider = azureProvider;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UserResponseDto> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var result = await _userRepository.Delete(request.Id);

        if (!string.IsNullOrEmpty(result.UserOid))
        {
            await _azureProvider.Delete(result.UserOid);
        }

        var dto = _mapper.Map<UserResponseDto>(result);

        return dto;
    }
    }
}
