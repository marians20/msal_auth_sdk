using CSharpFunctionalExtensions;

namespace SDK.DataAdapters;

public interface IApiClientContext
{
    string? Token { get; set; }

    HttpClient CreateClient();
    Task<Result<string>> RetreiveTokenAsync(CancellationToken cancellationToken = default);
}