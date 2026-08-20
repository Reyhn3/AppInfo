using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


namespace GenericHost;


public class DemoHostedService(ILogger<DemoHostedService> logger)
	: IHostedService
{
	public Task StartAsync(CancellationToken cancellationToken)
	{
		logger.LogInformation("GenericHost demo application started");
		return Task.CompletedTask;
	}

	public Task StopAsync(CancellationToken cancellationToken)
	{
		logger.LogInformation("GenericHost demo application stopped");
		return Task.CompletedTask;
	}
}
