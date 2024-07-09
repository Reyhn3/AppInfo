using System.Globalization;
using AppInfo;

//TODO: Change namespace
var appInfo = AppInfoBuilder
	.CreateDefaultBuilder()
//TODO: Create overload with int/long
	.WithIdentities("123456")
//TODO: #8: Add WithNames (service name, instance name)
	.AddTimestamp()
	.AddExtras(("Custom", "abc"))
	.AddExtras(
		("Custom", true),
		("Custom", false),
		("Feature", "Disabled"))
	.AddAssembly(typeof(IAppInfo).Assembly, "Info", stripSourceRevision: true)
	.AddAssembly(typeof(IAppInfo).Assembly, stripSourceRevision: true)
	.WithOutput(output => output
		.ToConsole()
		.ToTrace()
//TODO: Use the same file name for all file outputs (unless customized)
//TODO: #10: Replace this with Serilog and Microsoft Ilogger
//TODO: #10: Remove prefix
		.ToLog((structuredFormat, structuredArgs) => Console.WriteLine("Log: " + structuredFormat))
		.ToTextFile()
		.ToJsonFile())
	.UseCulture(CultureInfo.CurrentCulture)
	.Build();

//TODO: #9: Inject appInfo into Generic Host
