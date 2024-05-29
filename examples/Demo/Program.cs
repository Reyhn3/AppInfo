using AppInfo;

Console.WriteLine("Hello, AppInfo!");

//TODO: Change namespace
var appInfo = AppInfo.AppInfo
	.CreateDefaultBuilder()
	.WithIdentities()
	.AddTimestamp()
	.WriteToConsole()
	.Build();
