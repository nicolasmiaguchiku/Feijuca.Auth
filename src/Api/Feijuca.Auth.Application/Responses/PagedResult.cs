namespace Feijuca.Auth.Application.Responses
{
    public class PagedResult<T>
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int TotalPages => (TotalResults + PageSize - 1) / PageSize;
        public int TotalResults { get; set; }
        public IEnumerable<T> Results { get; set; } = null!;
    }
}
