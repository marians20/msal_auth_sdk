using SDK.Models;

namespace SDK.Services.ClientApplications;

internal sealed class ClientApplicationsUrlsFactory : IClientApplicationsUrlsFactory
{
    private readonly SdkSettings _sdkSettings;

    public ClientApplicationsUrlsFactory(SdkSettings sdkSettings)
    {
        _sdkSettings = sdkSettings;
    }

    public string GetUrlForGetAllAsync() => BaseUrl;

    private string BaseUrl => $"{_sdkSettings.BaseUrl}/ClientApplications";
}
