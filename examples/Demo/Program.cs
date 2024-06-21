using AppInfo.Builders.AppInfo;

//TODO: Change namespace
var appInfo = AppInfoBuilder
	.CreateDefaultBuilder()
	.WithIdentities()
	.AddTimestamp()
	.WithOutput(output => output
		.ToConsole()
		.ToTrace())
	.Build();

//TODO: Inject appInfo into Generic Host
