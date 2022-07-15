using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SDK;
using SDK.Models;
using SDK.Services.ClientApplications;

namespace TestConsole;

internal class Worker : BackgroundService
{
    private readonly ICLientApplicationsService _service;
    private readonly ILogger<Worker> _logger;

    public Worker(ICLientApplicationsService service, ILogger<Worker> logger)
    {
        _service = service;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            var result = await _service.GetAllAsync();
            _logger.LogInformation($"{result?.Count()}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}
