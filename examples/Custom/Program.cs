// This demonstration expands on the Basic example by adding custom properties,
// controlling the output and redirecting it.


using System.Globalization;
using AppInfo;

AppInfoBuilder
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
	.AddExtras(("Random", () => Random.Shared.Next()))
	.AddExtras(
			("Guid", () => Guid.NewGuid()),
			("Double", () => Random.Shared.NextDouble()))
	.AddAssembly(typeof(IAppInfo).Assembly)
	.AddAssembly(typeof(IAppInfo).Assembly, "Info", true)
	.UseCulture(CultureInfo.CreateSpecificCulture("sv-SE"))
	.WithOutput(output => output
		.ToConsole() // Write directly to console
		.ToTrace())  // Write to a trace listener (useful when running as a service)
	.Build();

Console.WriteLine("Application has started");
