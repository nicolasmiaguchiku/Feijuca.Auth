namespace Feijuca.Auth.Application.Requests.Client
{
    public record AddClientScopeToClientRequest(string ClientId, string ClientScopeId, bool IsOpticionalScope);
}
