using AppInfo.Builders.AppInfo;

//TODO: Change namespace
var appInfo = AppInfoBuilder
	.CreateDefaultBuilder()
	.WithIdentities()
	.AddTimestamp()
	.WithOutput(output => output
		.ToConsole()
		.ToTrace()
//TODO: Replace this with Serilog and Microsoft Ilogger
//TODO: Remove prefix
		.ToLog(info => Console.WriteLine("Log: {0}", info)))
	.Build();

//TODO: Inject appInfo into Generic Host
