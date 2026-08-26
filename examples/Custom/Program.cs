// This demonstration expands on the Basic example by adding custom properties,
// controlling the output and redirecting it.


using System.Globalization;
using AppInformation;

AppInfo
	.CreateDefaultBuilder()
//TODO: Create overload with int/long
	.WithIdentities("123456")
//TODO: #8: Add WithNames (service name, instance name)
	.AddTimestamp()
	.AddExtra("Custom", "abc")
	.AddExtra(
		("Custom 2", true),
		("Custom-three", short.MaxValue),
		("Feature AB 34", string.Empty))
	.AddExtra(("Random", () => Random.Shared.Next()))
	.AddExtra(
		("Guid", () => Guid.NewGuid()),
		("Enabled", () => true))
	.AddAssembly(typeof(IAppInfo).Assembly)
	.AddAssembly(typeof(IAppInfo).Assembly, "Info", true)
	.UseCulture(CultureInfo.CreateSpecificCulture("sv-SE"))
	.Build() // This builds the IAppInfo; the following steps configure and execute output
	.WithOutput(output => output
		.ToConsole() // Write directly to console
		.ToTrace())  // Write to a trace listener (useful when running as a service)
	.Write();

Console.WriteLine("Application has started");
