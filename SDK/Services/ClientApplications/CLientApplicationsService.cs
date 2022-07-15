using Microsoft.Rise.FeedbackService.Contracts.Dto;

namespace SDK.Services.ClientApplications;

internal sealed class CLientApplicationsService : ICLientApplicationsService
{
    private readonly IApiClient _client;
    private readonly IClientApplicationsUrlsFactory _urlBuilder;

    public CLientApplicationsService(IApiClient client, IClientApplicationsUrlsFactory urlBuilder)
    {
        _client = client;
        _urlBuilder = urlBuilder;
    }

    public async Task<IEnumerable<ClientApplicationDto>?> GetAllAsync() =>
        await _client.Get<IEnumerable<ClientApplicationDto>>(_urlBuilder.GetUrlForGetAllAsync()).ConfigureAwait(false);
}
