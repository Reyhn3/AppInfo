using System.Globalization;
using AppInfo;

//TODO: Change namespace
var appInfo = AppInfoBuilder
	.CreateDefaultBuilder()
	.WithIdentities("MyAppId")
//TODO: #8: Add WithNames (service name, instance name)
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
//TODO: #10: Replace this with Serilog and Microsoft Ilogger
//TODO: #10: Remove prefix
		.ToLog(info => Console.WriteLine("Log: {0}", info))
		.ToTextFile()
		.ToJsonFile())
	.UseCulture(CultureInfo.CurrentCulture)
	.Build();

//TODO: #9: Inject appInfo into Generic Host
