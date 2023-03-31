using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RBACV2.Application.Common.Extensions;
using RBACV2.Application.Common.Interfaces.Repositories;
using RBACV2.Application.Common.PaginationResponse;
using RBACV2.Application.UsersEntity.Dtos;
using RBACV2.Application.UsersEntity.Queries;

namespace RBACV2.Application.UsersEntity.Handlers.Queries
{
    public class GetFilteredUsersQueryHandler : IRequestHandler<GetFilteredUsersQuery, Paged<GetUsersDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public GetFilteredUsersQueryHandler(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public Task<Paged<GetUsersDto>> Handle(GetFilteredUsersQuery request, CancellationToken cancellationToken)
        {
            var query = _userRepository.Query()
               .Filter(request.Search!, cancellationToken)
               .Include(x => x.Role)
               .OrderByDescending(x => x.CreatedDate);

            var queryMapped = query
               .ProjectTo<GetUsersDto>(_mapper.ConfigurationProvider);

            var paginatedResult = queryMapped
               .Paginate(request.PageNumber, request.PageSize, cancellationToken);

            return paginatedResult;
        }
    }
}
