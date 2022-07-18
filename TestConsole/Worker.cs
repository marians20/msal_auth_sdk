using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Rise.FeedbackService.Core.Interfaces.Services;
using SDK.DataAdapters;

namespace TestConsole;

internal class Worker : BackgroundService
{
    private readonly IApiClient _client;
    private readonly IClientApplicationService _service;
    private readonly ILogger<Worker> _logger;

    public Worker(IClientApplicationService service, ILogger<Worker> logger, IApiClient client)
    {
        _service = service;
        _logger = logger;
        _client = client;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            await _client.ExecuteAuthenticated(async () => {
                var result = await _service.GetByIdAsync(Guid.NewGuid());
                _logger.LogInformation($"{result}");
                return result;
            });

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}
