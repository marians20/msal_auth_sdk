// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SDK;
using TestConsole;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
    {
        services
            .RegisterSDK()
            .AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();