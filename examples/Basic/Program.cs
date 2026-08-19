// This demonstrates the most basic, non-intrusive usage of AppInfo.


using AppInfo;

AppInfoBuilder
	.CreateDefaultBuilder()
//TODO: Create overload with int/long
	.WithIdentities("123456")
//TODO: #8: Add WithNames (service name, instance name)
	.AddTimestamp()
	.Build();

Console.WriteLine("Application has started");
