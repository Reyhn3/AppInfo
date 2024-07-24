using System.Globalization;
using AppInfo;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;


// Create a bootstrap logger that will write the app info to console
Log.Logger = new LoggerConfiguration()
	.MinimumLevel.Verbose()
	.WriteTo.Console()
	.CreateBootstrapLogger();

// Create a Microsoft-logger to write the app info to
var msLogger = LoggerFactory
	.Create(b => b.AddConsole())
	.CreateLogger<Program>();

// Configure and create app info
var appInfo = AppInfoBuilder
	.CreateDefaultBuilder()
//TODO: Create overload with int/long
	.WithIdentities("123456")
//TODO: #8: Add WithNames (service name, instance name)
	.AddTimestamp()
	.AddExtras(("Custom", "abc"))
	.AddExtras(
		("Custom 2", true),
		("Custom-three", short.MaxValue),
		("Feature", "Disabled"),
		("Feature AB 34", string.Empty))
	.AddAssembly(typeof(IAppInfo).Assembly)
	.AddAssembly(typeof(IAppInfo).Assembly, "Info", true)
	.UseCulture(CultureInfo.CurrentCulture)
	.WithOutput(output => output
		.ToConsole() // Write directly to console
		.ToTrace()   // Write to a trace listener (useful when running as a service)
//TODO: Use the same file name for all file outputs (unless customized)
		.ToLog(Log.Information)         // Demonstrates output can be directed to Serilog
		.ToLog(msLogger.LogInformation) // Demonstrates output can be directed to ILogger
		.ToTextFile()                   // Write to a plain text file (useful to include in bug reports)
		.ToJsonFile())                  // Write to structured JSON file (useful for automated processing)
//TODO: Consider SRP-refactoring to separate build-concern from output-concern
	.Build();

try
{
	await Host
		.CreateDefaultBuilder(args)
		.ConfigureServices(services => services.AddSingleton(appInfo))
		.UseSerilog((_, provider, logger) => logger
			.WriteTo.Console()
			// The following three lines demonstrate how the injected IAppInfo can be
			// used to retrieve information. Some of that information can be very
			// useful for log enrichment (as done here) or observability (for instance,
			// when using OpenTelemetry).
			.Enrich.WithProperty("ApplicationId", provider.GetService<IAppInfo>().ApplicationId())
			.Enrich.WithProperty("InstanceId", provider.GetService<IAppInfo>().InstanceId())
			.Enrich.WithProperty("ScopeId", provider.GetService<IAppInfo>().ScopeId()))
		.RunConsoleAsync(new CancellationTokenSource(TimeSpan.Zero).Token); // Exit demo immediately
}
catch (OperationCanceledException)
{
	// Ignore
}
