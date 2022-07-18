using CSharpFunctionalExtensions;

namespace SDK.Auth
{
    public interface IApiFlowTokenRetreiver
    {
        Task<Result<string>> GetTokenAsync(CancellationToken cancellationToken = default);
    }
}