using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Rise.FeedbackService.Core.Interfaces.Services;

namespace TestConsole;

internal class Worker : BackgroundService
{
    private readonly IClientApplicationService _service;
    private readonly ILogger<Worker> _logger;

    public Worker(IClientApplicationService service, ILogger<Worker> logger)
    {
        _service = service;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            var result = await _service.GetByIdAsync(Guid.NewGuid());
            _logger.LogInformation($"{result}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}
