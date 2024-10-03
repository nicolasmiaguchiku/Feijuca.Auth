namespace TokenManager.Domain.Filters
{
    public class UserFilters(PageFilter pageFilter, IEnumerable<Guid>? ids, IEnumerable<string>? emails)
    {
        public PageFilter PageFilter { get; set; } = pageFilter;
        public IEnumerable<Guid>? Ids { get; set; } = ids;
        public IEnumerable<string>? Emails { get; set; } = emails;
    }
}
