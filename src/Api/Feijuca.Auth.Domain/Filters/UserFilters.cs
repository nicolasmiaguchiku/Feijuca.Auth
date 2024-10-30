namespace Feijuca.Auth.Domain.Filters
{
    public class UserFilters(PageFilter pageFilter, IEnumerable<Guid>? ids, IEnumerable<string>? usernames)
    {
        public PageFilter PageFilter { get; set; } = pageFilter;
        public IEnumerable<Guid>? Ids { get; set; } = ids;
        public IEnumerable<string>? Usernames { get; set; } = usernames;
    }
}
