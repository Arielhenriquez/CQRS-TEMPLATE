namespace RBACV2.Application.Common.PaginationQuery
{
    public class PaginationQuery
    {
        public string? Search { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
