using Microsoft.Identity.Client;
using SDK.Models;

namespace SDK.Auth;

internal sealed class ApiFlowTokenRetreiver : IApiFlowTokenRetreiver
{
    private readonly Oauth2Settings _settings;

    IConfidentialClientApplication _confidentialClientApp;

    public ApiFlowTokenRetreiver(Oauth2Settings settings, IConfidentialClientApplication confidentialClientApp)
    {
        _settings = settings;
        _confidentialClientApp = confidentialClientApp ?? throw new ArgumentNullException(nameof(confidentialClientApp));
    }

    public async Task<string> GetTokenAsync(CancellationToken cancellationToken = default)
    {
        var authenticationResult = await _confidentialClientApp.AcquireTokenForClient(_settings.Scope).ExecuteAsync(cancellationToken);
        return authenticationResult.AccessToken;
    }
}
