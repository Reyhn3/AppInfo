// This demonstrates how to use the WithOutput to target standard logging
// APIs. In this example, both Microsoft and Serilog are used.


using AppInformation;
using Microsoft.Extensions.Logging;
using Serilog;


// Create a Serilog logger that will write the app info to console
//
// TIP: See the GenericHost example for an example of how to enrich
// logs with IAppInfo properties.
Log.Logger = new LoggerConfiguration()
	.MinimumLevel.Verbose()
	.WriteTo.Console()
	.CreateLogger();

// Create a Microsoft-logger to write the app info to
var msLogger = LoggerFactory
	.Create(b => b.AddConsole())
	.CreateLogger<Program>();

// Configure and create app info
AppInfo
	.CreateDefaultBuilder()
	.Build()
	.WithOutput(output => output
		.ToConsole()                    // Write directly to console
		.ToTrace()                      // Write to a trace listener (useful when running as a service)
		.ToLog(Log.Information)         // Demonstrates output can be directed to Serilog
		.ToLog(msLogger.LogInformation) // Demonstrates output can be directed to ILogger
//TODO: #30: Use the same file name for all file outputs (unless customized)
//TODO: Add overload to specify file name
		.ToTextFile()                   // Write to a plain text file (useful to include in bug reports)
		.ToJsonFile())                  // Write to structured JSON file (useful for automated processing)
	.Write();

Console.WriteLine("Application has started");
