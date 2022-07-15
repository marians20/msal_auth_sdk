using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;
using SDK.Extensions;
using SDK.Models;
using SDK.Services.ClientApplications;

namespace SDK;

public static class IoC
{
    public static IServiceCollection RegisterSDK(this IServiceCollection services)
    {
        IConfiguration config = BuildConfiguration();
        var sdkSettings = config.GetSection("Api").Get<SdkSettings>();
        var oauth2Settings = config.GetSection("OAuth2").Get<Oauth2Settings>();

        services
            .AddSingleton(sdkSettings)
            .AddSingleton(new SdkContext(oauth2Settings.FinalScope))
            .AddHttpClient("SDK", httpClient =>
            {
                var serviceProvider = services.BuildServiceProvider();
                var sdkContext = serviceProvider.GetService<SdkContext>();
                httpClient.DefaultRequestHeaders.Add(Constants.AcceptHeader, Constants.AcceptHeaderValues.Json);

                if (!string.IsNullOrEmpty(sdkContext?.Token))
                {
                    httpClient.Authenticate(sdkContext.Token);
                }
            });

        services
            .RegisterMsal(oauth2Settings)
            .AddScoped<IApiClient, ApiClient>()
            .RegisterServices();
        return services;
    }

    private static IConfiguration BuildConfiguration() => new ConfigurationBuilder()
            .AddJsonFile("rise_feedback_settings.json")
            .AddJsonFile("rise_feedback_settings.local.json")
            .AddEnvironmentVariables()
            .Build();

    /// <summary>
    /// https://docs.microsoft.com/en-us/azure/active-directory/develop/console-app-quickstart?pivots=devlang-dotnet-core
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    private static IServiceCollection RegisterMsal(this IServiceCollection services, Oauth2Settings oauth2Settings)
    {
        services.AddScoped(_ => ConfidentialClientApplicationBuilder
            .Create(oauth2Settings.ClientId)
            .WithClientSecret(oauth2Settings.ClientSecret)
            .WithAuthority(new Uri(oauth2Settings.AuthUrl))
            .Build());
        return services;
    }

    private static IServiceCollection RegisterServices(this IServiceCollection services) =>
        services
        .AddScoped<IClientApplicationsUrlsFactory, ClientApplicationsUrlsFactory>()
        .AddScoped<ICLientApplicationsService, CLientApplicationsService>();
}