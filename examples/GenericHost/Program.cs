// This example is based on Microsoft's generic host using the HostBuilder API.
// https://learn.microsoft.com/en-us/dotnet/core/extensions/generic-host?tabs=appbuilder


using AppInfo;
using GenericHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Formatting.Compact;

var appInfo = AppInfo.Create
	.DefaultBuilder()
	.Build()
	.WithDefaultOutput()
	.Write();

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddSingleton(appInfo);
builder.Services.AddHostedService<DemoHostedService>();
builder.Services.AddSerilog((provider, logger) => logger
	.WriteTo.Console(new CompactJsonFormatter())

	// The following three lines demonstrate how the injected IAppInfo can be
	// used to retrieve information. Some of that information can be very
	// useful for log enrichment (as done here) or observability (for instance,
	// when using OpenTelemetry).
	.Enrich.WithProperty("ApplicationId", provider.GetService<IAppInfo>().ApplicationId())
	.Enrich.WithProperty("InstanceId", provider.GetService<IAppInfo>().InstanceId())
	.Enrich.WithProperty("ScopeId", provider.GetService<IAppInfo>().ScopeId()));

builder.Logging.AddSerilog();
builder.Logging.AddJsonConsole();

var host = builder.Build();
await host.RunAsync();
