using SDK.Models;

namespace SDK.Services.ClientApplications;

internal sealed class ClientApplicationsUrlFactory : UrlFactoryBase, IClientApplicationUrlsFactory
{
    public ClientApplicationsUrlFactory(SdkSettings sdkSettings) : base(sdkSettings)
    {
    }

    public override string BaseUrl => $"{SdkSettings.BaseUrl}/ClientApplications";
}
