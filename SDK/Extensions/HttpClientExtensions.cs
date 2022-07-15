namespace SDK.Extensions;

internal static class HttpClientExtensions
{
    public static HttpClient Authenticate(this HttpClient httpClient, string token)
    {
        httpClient.Logout();
        httpClient.DefaultRequestHeaders.Add(Constants.AuthorizationHeader, $"{Constants.Bearer} {token ?? throw new ArgumentNullException(nameof(token))}");
        return httpClient;
    }

    public static HttpClient Logout(this HttpClient httpClient)
    {
        if (httpClient.DefaultRequestHeaders.Contains(Constants.AuthorizationHeader))
        {
            httpClient.DefaultRequestHeaders.Remove(Constants.AuthorizationHeader);
        }

        return httpClient;
    }
}
