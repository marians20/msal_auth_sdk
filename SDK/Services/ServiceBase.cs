using SDK.DataAdapters;

namespace SDK.Services;

public abstract class ServiceBase
{
    protected readonly IApiClient _client;

    protected ServiceBase(IApiClient client)
    {
        _client = client;
    }
}
