// This demonstrates how the IAppInfo can be used in Azure Functions,
// both during startup and resolved at a later time.


using AppInformation;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var appInfo = AppInfo
	.CreateDefaultBuilder()
	.Build()
	.WithDefaultOutput()
	.Write();

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();
builder.Services.AddSingleton(appInfo);

builder.Build().Run();
