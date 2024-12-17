namespace Feijuca.Auth.Domain.Interfaces
{
    public interface IBaseRepository
    {
        HttpClient CreateHttpClientWithHeaders(string accessToken);
    }
}
