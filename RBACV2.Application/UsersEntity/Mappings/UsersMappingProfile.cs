using AutoMapper;
using Microsoft.Graph;
using RBACV2.Application.UsersEntity.Commands;
using RBACV2.Application.UsersEntity.Dtos;
using RBACV2.Domain.Constants;
using RBACV2.Domain.Entities.UserEntity;

namespace RBACV2.Application.UsersEntity.Mappings
{
    public class UsersMappingProfile : Profile
    {
        public UsersMappingProfile()
        {
            CreateMap<UserPasswordProfile, PasswordProfile>().ReverseMap();
            CreateMap<AzureADUserCommand, Users>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Users, AzureADUserCommand>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<CreateUserCommand, AzureADUserCommand>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));


            CreateMap<Users, UserResponseDto>().ReverseMap();
            CreateMap<UpdateUserCommand, User>().ReverseMap();
            CreateMap<UpdateUserCommand, Users>().ReverseMap();

            //CreateMap<AzureADUserEntity, AzureADUserRoleResponseDto>();
            //CreateMap<AzureADUserRoleResponseDto, AzureADUserEntity>();
            CreateMap<GetUsersDto, Users>().ReverseMap();
            CreateMap<GetUserByIdDto, Users>().ReverseMap();

            CreateMap<AzureADUserCommand, User>()
                .ForMember(destination => destination.DisplayName, op => op.MapFrom(source => source.FullName))
                .ForMember(destination => destination.MailNickname, op => op.MapFrom(source => source.UserName))
                .ForMember(destination => destination.AccountEnabled, op => op.MapFrom(source => source.IsEnabled))
                .ForMember(destination => destination.Id, op => op.MapFrom(source => source.UserOid))
                .ForMember(destination => destination.UserPrincipalName, op => op.MapFrom(source => source.UserName + AzureADDomain.Domain))
                .ForMember(destination => destination.GivenName, op => op.MapFrom(source => source.FirstName));

            CreateMap<CreateUserCommand, User>()
               .ForMember(destination => destination.DisplayName, op => op.MapFrom(source => source.FullName))
               .ForMember(destination => destination.MailNickname, op => op.MapFrom(source => source.UserName))
               .ForMember(destination => destination.AccountEnabled, op => op.MapFrom(source => source.IsEnabled))
               .ForMember(destination => destination.UserPrincipalName, op => op.MapFrom(source => source.UserName + AzureADDomain.Domain))
               .ForMember(destination => destination.GivenName, op => op.MapFrom(source => source.FirstName));

            CreateMap<User, Users>()
                .ForMember(destination => destination.FullName, op => op.MapFrom(source => source.DisplayName))
                .ForMember(destination => destination.UserName, op => op.MapFrom(source => source.MailNickname))
                .ForMember(destination => destination.IsEnabled, op => op.MapFrom(source => source.AccountEnabled))
                .ForMember(destination => destination.FullEmail, op => op.MapFrom(source => source.UserPrincipalName))
                .ForMember(destination => destination.UserOid, op => op.MapFrom(source => source.Id))
                .ForMember(destination => destination.FirstName, op => op.MapFrom(source => source.GivenName));

            CreateMap<User, UserResponseDto>()
               .ForMember(destination => destination.FullName, op => op.MapFrom(source => source.DisplayName))
               .ForMember(destination => destination.UserName, op => op.MapFrom(source => source.MailNickname))
               .ForMember(destination => destination.IsEnabled, op => op.MapFrom(source => source.AccountEnabled))
               .ForMember(destination => destination.FullEmail, op => op.MapFrom(source => source.UserPrincipalName))
               .ForMember(destination => destination.UserOid, op => op.MapFrom(source => source.Id))
               .ForMember(destination => destination.FirstName, op => op.MapFrom(source => source.GivenName));

            CreateMap<Users, User>()
                .ForMember(destination => destination.DisplayName, op => op.MapFrom(source => source.FullName))
                .ForMember(destination => destination.MailNickname, op => op.MapFrom(source => source.UserName))
                .ForMember(destination => destination.AccountEnabled, op => op.MapFrom(source => source.IsEnabled))
                .ForMember(destination => destination.UserPrincipalName, op => op.MapFrom(source => source.FullEmail))
                .ForMember(destination => destination.Id, op => op.MapFrom(source => source.UserOid))
                .ForMember(destination => destination.GivenName, op => op.MapFrom(source => source.FirstName));

            CreateMap<User, UserResponseDto>().ReverseMap();
            //CreateMap<AzureADUserDto, RoleDto>().ReverseMap();
        }
    }
}
