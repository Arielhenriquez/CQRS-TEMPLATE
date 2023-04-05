using AutoMapper;
using Microsoft.Graph;
using RBACV2.Application.UsersEntity.Commands;
using RBACV2.Application.UsersEntity.Dtos;
using RBACV2.Domain.Entities.UserEntity;

namespace RBACV2.Application.UsersEntity.Mappings
{
    public class UsersMappingProfile : Profile
    {
        public UsersMappingProfile()
        {
            CreateMap<UserPasswordProfile, PasswordProfile>().ReverseMap();
            CreateMap<GetUsersDto, Users>().ReverseMap();
            // CreateMap<GetUserByIdDto, Users>().ReverseMap();
            CreateMap<Users, UserResponseDto>().ReverseMap();
            CreateMap<CreateUserCommand, Users>().ReverseMap();
            CreateMap<UpdateUserCommand, Users>().ReverseMap();

            //CreateMap<AzureADUserEntity, AzureADUserRoleResponseDto>();
            //CreateMap<AzureADUserRoleResponseDto, AzureADUserEntity>();


            //CreateMap<AzureADUserDto, RoleDto>().ReverseMap();
        }
    }
}
