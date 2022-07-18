using CSharpFunctionalExtensions;
using Microsoft.Identity.Client;
using SDK.Extensions;
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

    public async Task<Result<string>> GetTokenAsync(CancellationToken cancellationToken = default) =>
        await TryCatch.ExecuteAsync(() => _confidentialClientApp.AcquireTokenForClient(_settings.Scope).ExecuteAsync(cancellationToken))
            .OnSuccessTry(authenticationResult => authenticationResult.AccessToken)
            .OnFailure(error => Result.Failure<string>(error));
}
