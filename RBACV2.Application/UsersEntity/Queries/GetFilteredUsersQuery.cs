using MediatR;
using RBACV2.Application.Common.PaginationQuery;
using RBACV2.Application.Common.PaginationResponse;
using RBACV2.Application.UsersEntity.Dtos;

namespace RBACV2.Application.UsersEntity.Queries
{
    public class GetFilteredUsersQuery : IRequest<Paged<GetUsersDto>>
    {
        public string? Search { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public GetFilteredUsersQuery(PaginationQuery query)
        {
            Search = query.Search;
            PageSize = query.PageSize;
            PageNumber = query.PageNumber;
        }
    }
}
