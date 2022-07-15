using SDK.Models;

namespace SDK.Services.ClientApplications;

internal abstract class UrlFactoryBase : IUrlFactoryBase
{
    protected readonly SdkSettings SdkSettings;

    public UrlFactoryBase(SdkSettings sdkSettings)
    {
        SdkSettings = sdkSettings;
    }

    public string GetUrlForWithIdParameter(Guid id) => $"{BaseUrl}/{id}";

    public abstract string BaseUrl { get; }
}
