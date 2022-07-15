namespace SDK.Models;

public sealed class Oauth2Settings
{
    public string AuthServer { get; set; } = default!;

    public string ClientId { get; set; } = default!;

    public string ClientSecret { get; set; } = default!;

    public string TenantId { get; set; } = default!;

    public string[] Scope { get; set; } = default!;

    public string AuthUrl => $"{AuthServer}/{TenantId}/oauth2/v2.0/authorize";

    public string AccessTokenUrl => $"{AuthServer}/{TenantId}/oauth2/v2.0/token";

    public string[] FinalScope => Scope.Select(s => $"api://{ClientId}/{s}").ToArray();
}
