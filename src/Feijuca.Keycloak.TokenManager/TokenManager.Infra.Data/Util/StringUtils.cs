using Flurl;

namespace TokenManager.Infra.Data.Util
{
    public static class StringUtils
    {
        public static string BuildUrl(this string url, string tenant, string resource)
        {
            return url
                    .AppendPathSegment("admin")
                    .AppendPathSegment("realms")
                    .AppendPathSegment(tenant)
                    .AppendPathSegment(resource);
        }
    }
}
