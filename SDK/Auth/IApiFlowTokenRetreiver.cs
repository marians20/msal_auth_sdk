namespace SDK.Auth
{
    public interface IApiFlowTokenRetreiver
    {
        Task<string> GetTokenAsync(CancellationToken cancellationToken = default);
    }
}