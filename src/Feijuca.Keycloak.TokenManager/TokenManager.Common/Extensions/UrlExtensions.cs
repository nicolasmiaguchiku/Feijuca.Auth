using Flurl;

namespace Common.Extensions
{
    public static class UrlExtensions
    {
        public static Url SetCollectionQueryParam<T>(this Url url, string name, IEnumerable<T>? value)
        {
            if (value?.Any() ?? false)
            {
                var filter = string.Join(",", value);
                url.SetQueryParam(name, filter);
            }

            return url;
        }
    }
}
