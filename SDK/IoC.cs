using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;
using Microsoft.Rise.FeedbackService.Core.Interfaces.Services;
using SDK.Auth;
using SDK.DataAdapters;
using SDK.Extensions;
using SDK.Models;
using SDK.Services.ClientApplications;

namespace SDK;

public static class IoC
{
    public static IServiceCollection RegisterSDK(this IServiceCollection services)
    {
        //services.AddValidatedOptions<SmtpOptions, SmtpOptionsValidator>().Bind(this.configuration.GetSection(nameof(SmtpOptions)));
        IConfiguration config = BuildConfiguration();
        var sdkSettings = config.GetSection("Api").Get<SdkSettings>();
        var oauth2Settings = config.GetSection("OAuth2").Get<Oauth2Settings>();

        services
            .AddSingleton(sdkSettings)
            .AddSingleton(oauth2Settings)
            .AddScoped<IApiFlowTokenRetreiver, ApiFlowTokenRetreiver>()
            .AddScoped<IApiClientContext, ApiClientContext>()
            .AddHttpClient("SDK", httpClient =>
            {
                var serviceProvider = services.BuildServiceProvider();
                var sdkContext = serviceProvider.GetService<ApiClientContext>();
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
        .AddScoped<IClientApplicationUrlsFactory, ClientApplicationsUrlFactory>()
        .AddScoped<IClientApplicationService, ClientApplicationService>();
}