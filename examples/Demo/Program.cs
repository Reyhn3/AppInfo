﻿using AppInfo;
using AppInfo.Builders.AppInfo;

//TODO: Change namespace
var appInfo = AppInfoBuilder
	.CreateDefaultBuilder()
	.WithIdentities()
	.AddTimestamp()
	.AddExtras(("Custom", "abc"))
	.AddExtras(
		("Custom", "def"),
		("Feature", "Disabled"))
	.WithOutput(output => output
		.ToConsole()
		.ToTrace()
//TODO: Replace this with Serilog and Microsoft Ilogger
//TODO: Remove prefix
		.ToLog(info => Console.WriteLine("Log: {0}", info))
		.ToTextFile()
		.ToJsonFile())
	.Build();

//TODO: Inject appInfo into Generic Host
