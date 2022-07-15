using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using SDK.Extensions;
using SDK.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace SDK;

public sealed class ApiClient : IApiClient
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new ()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    private readonly HttpClient _httpClient;
    private readonly IHttpClientFactory _clientFactory;
    private readonly IConfidentialClientApplication _confidentialClientApp;
    private readonly SdkContext _context;
    private readonly ILogger<ApiClient> _logger;
    public ApiClient(
        IHttpClientFactory clientFactory,
        IConfidentialClientApplication confidentialClientApp,
        SdkContext context,
        ILogger<ApiClient> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _clientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));
        _httpClient = _clientFactory.CreateClient("SDK") ?? throw new NullReferenceException(nameof(_httpClient));
        if(!string.IsNullOrEmpty(_context.Token))
        {
            _httpClient.Authenticate(_context.Token);
        }

        _confidentialClientApp = confidentialClientApp ?? throw new ArgumentNullException(nameof(confidentialClientApp));
        
    }

    public async Task<string> Get(string url, CancellationToken cancellationToken = default) => await _httpClient.GetStringAsync(url, cancellationToken);

    public async Task<TResponse?> Get<TResponse>(string url, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync(url, cancellationToken);
        EnsureResponseIsSuccess(response);

        using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
        return await JsonSerializer.DeserializeAsync<TResponse?>(stream, JsonSerializerOptions, cancellationToken);
    }

    private static void EnsureResponseIsSuccess(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"{response.StatusCode.ToString()}", null, response.StatusCode);
        }
    }

    public async Task<string?> Post(string url, object payload, CancellationToken cancellationToken = default) =>
        await (await _httpClient.PostAsync(url, CreateContent(payload))).Content.ReadAsStringAsync(cancellationToken);

    public async Task<TResponse?> Post<TResponse>(string url, object payload, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PostAsync(url, CreateContent(payload), cancellationToken);
        EnsureResponseIsSuccess(response);

        using var stream = await response.Content.ReadAsStreamAsync();
        return await JsonSerializer.DeserializeAsync<TResponse?>(stream, JsonSerializerOptions, cancellationToken);
    }

    public async Task<string?> Put(string url, object payload, CancellationToken cancellationToken = default) =>
        await (await _httpClient.PutAsync(url, CreateContent(payload), cancellationToken))
            .Content.ReadAsStringAsync(cancellationToken);

    public async Task<TResponse?> Put<TResponse>(string url, object payload, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PutAsync(url, CreateContent(payload), cancellationToken);
        EnsureResponseIsSuccess(response);

        using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
        return await JsonSerializer.DeserializeAsync<TResponse?>(stream, JsonSerializerOptions, cancellationToken);
    }

    public async Task Delete(string url, CancellationToken cancellationToken = default) => await _httpClient.DeleteAsync(url, cancellationToken);

    private static HttpContent CreateContent(object payload) => JsonContent.Create(payload);

    public async Task Authenticate()
    {
        var authenticationResult = await _confidentialClientApp.AcquireTokenForClient(_context.Scope).ExecuteAsync();
        _context.Token = authenticationResult.AccessToken;
        _logger.LogInformation(_context.Token);
        _httpClient.Authenticate(_context.Token);
    }

    public void Logout()
    {
        _context.Token = String.Empty;
        _httpClient.DefaultRequestHeaders.Remove(Constants.AuthorizationHeader);
    }

    public async Task<T> ExecuteAuthenticated<T>(Func<Task<T>> action)
    {
        await Authenticate();
        T result;
        try
        {
            result = await action();
        }
        finally
        {
            Logout();
        }

        return result;
    }
}
