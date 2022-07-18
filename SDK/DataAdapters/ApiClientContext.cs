using SDK.Auth;
using System.Net.Http;

namespace SDK.DataAdapters
{
    public sealed class ApiClientContext : IApiClientContext
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IApiFlowTokenRetreiver _tokenRetreiver;

        public ApiClientContext(
            IApiFlowTokenRetreiver tokenRetreiver,
            IHttpClientFactory clientFactory)
        {
            _tokenRetreiver = tokenRetreiver ?? throw new ArgumentNullException(nameof(tokenRetreiver));
            _clientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));
        }

        public string? Token { get; set; }

        public async Task<string> RetreiveTokenAsync(CancellationToken cancellationToken = default) =>
            Token = await _tokenRetreiver.GetTokenAsync(cancellationToken);

        public HttpClient CreateClient() => _clientFactory.CreateClient("SDK");
    }
}
