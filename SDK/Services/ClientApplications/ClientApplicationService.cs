using Microsoft.Rise.FeedbackService.Contracts.Dto;
using Microsoft.Rise.FeedbackService.Core.Interfaces.Services;
using SDK.DataAdapters;

namespace SDK.Services.ClientApplications;

internal sealed class ClientApplicationService : ServiceBase, IClientApplicationService
{
    private readonly IClientApplicationUrlsFactory _urlBuilder;

    public ClientApplicationService(IApiClient client, IClientApplicationUrlsFactory urlBuilder) : base(client)
    {
        _urlBuilder = urlBuilder;
    }

#pragma warning disable CS8603 // Possible null reference return.
    public async Task<ClientApplicationDto> CreateAsync(ClientApplicationDto clientApplication) =>
        await _client.Post<ClientApplicationDto>(_urlBuilder.BaseUrl, clientApplication).ConfigureAwait(false);

    public async Task<ClientApplicationDto> DeleteAsync(Guid id) =>
        await _client.Delete<ClientApplicationDto>(_urlBuilder.GetUrlForWithIdParameter(id)).ConfigureAwait(false);

    public async Task<IEnumerable<ClientApplicationDto>> GetAllAsync() =>
        await _client.Get<IEnumerable<ClientApplicationDto>>(_urlBuilder.BaseUrl).ConfigureAwait(false);

    public async Task<ClientApplicationDto> GetByIdAsync(Guid id) =>
        await _client.Get<ClientApplicationDto>(_urlBuilder.GetUrlForWithIdParameter(id)).ConfigureAwait(false);

    public async Task<ClientApplicationDto> UpdateAsync(Guid id, ClientApplicationDto clientApplication) =>
        await _client.Put<ClientApplicationDto>(_urlBuilder.GetUrlForWithIdParameter(id), clientApplication).ConfigureAwait(false);

#pragma warning restore CS8603 // Possible null reference return.
}
