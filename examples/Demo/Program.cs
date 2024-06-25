using System.Globalization;
using AppInfo;

//TODO: Change namespace
var appInfo = AppInfoBuilder
	.CreateDefaultBuilder()
	.WithIdentities("MyAppId")
	.AddTimestamp()
	.AddExtras(("Custom", "abc"))
	.AddExtras(
		("Custom", "def"),
		("Feature", "Disabled"))
	.AddAssembly(typeof(IAppInfo).Assembly, "AppInfo", stripSourceRevision: true)
	.AddAssembly(typeof(IAppInfo).Assembly, stripSourceRevision: true)
	.WithOutput(output => output
		.ToConsole()
		.ToTrace()
//TODO: Replace this with Serilog and Microsoft Ilogger
//TODO: Remove prefix
		.ToLog(info => Console.WriteLine("Log: {0}", info))
		.ToTextFile()
		.ToJsonFile())
	.UseCulture(CultureInfo.CurrentCulture)
	.Build();

//TODO: Inject appInfo into Generic Host
