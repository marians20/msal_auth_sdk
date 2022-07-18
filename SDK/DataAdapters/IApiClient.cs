using CSharpFunctionalExtensions;

namespace SDK.DataAdapters
{
    public interface IApiClient
    {
        Task<Result> Authenticate();

        Task Authenticate(string token);

        void Logout();

        Task<string> Get(string url, CancellationToken cancellationToken = default);

        Task<TResponse?> Get<TResponse>(string url, CancellationToken cancellationToken = default);

        Task<string?> Post(string url, object payload, CancellationToken cancellationToken = default);

        Task<string?> Put(string url, object payload, CancellationToken cancellationToken = default);

        Task<TResponse?> Post<TResponse>(string url, object payload, CancellationToken cancellationToken = default);

        Task<TResponse?> Put<TResponse>(string url, object payload, CancellationToken cancellationToken = default);

        Task Delete(string url, CancellationToken cancellationToken = default);

        Task<TResponse> Delete<TResponse>(string url, CancellationToken cancellationToken = default);

        Task<Result<Result<T>>> ExecuteAuthenticated<T>(Func<Task<T>> func);

        Task<T> ExecuteAuthenticated<T>(Func<Task<T>> func, string token);
    }
}