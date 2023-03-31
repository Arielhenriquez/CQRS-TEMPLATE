using AutoMapper;
using MediatR;
using Microsoft.Graph;
using RBACV2.Application.Common.Interfaces.Abstract;
using RBACV2.Application.Common.Interfaces.Repositories;
using RBACV2.Application.UsersEntity.Commands;
using RBACV2.Application.UsersEntity.Dtos;
using RBACV2.Domain.Entities.UserEntity;
using RBACV2.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace RBACV2.Application.UsersEntity.Handlers.Commands
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserResponseDto>
    {
        private readonly IAzureADUserProvider _azureProvider;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(IAzureADUserProvider azureProvider, IUserRepository userRepository, IMapper mapper)
        {
            _azureProvider = azureProvider;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserResponseDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            bool duplicatedUserName = _userRepository.Query()
                .Any(x => x.FirstName == request.FirstName);

            if (duplicatedUserName)
                throw new ValidationException($"El nombre de usuario: {request.FirstName} ya existe");

            if (request.ActionType != ActionsTypes.Create)
                throw new ArgumentException($"Action Type '{request.ActionType}' is not supported, you can only Create.");

            var userAd = _mapper.Map<User>(request);
            userAd.PasswordProfile = new PasswordProfile()
            {
                Password = request.PasswordProfile!.Password,
                ForceChangePasswordNextSignIn = true
            };

            var validUserPrincipal = await _azureProvider.UserPrincipalExists(userAd.UserPrincipalName);

            if (!validUserPrincipal)
                throw new ValidationException("Este nombre de usuario ya existe");

            var result = await _azureProvider.Create(userAd);

            var baseRequest = _mapper.Map<AzureADUserCommand>(request);

            baseRequest.UserOid = result.Id;
            baseRequest.FullEmail = result.UserPrincipalName;

            var userEntity = _mapper.Map<Users>(baseRequest);
            _mapper.Map(baseRequest, userEntity);
            var user = await _userRepository.Add(userEntity);

            var dto = _mapper.Map<UserResponseDto>(user);

            return dto;
        }
    }
}
