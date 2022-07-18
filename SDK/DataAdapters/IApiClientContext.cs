namespace SDK.DataAdapters
{
    public interface IApiClientContext
    {
        string? Token { get; set; }

        HttpClient CreateClient();
        Task<string> RetreiveTokenAsync(CancellationToken cancellationToken = default);
    }
}