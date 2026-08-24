// This demonstrates the most basic, non-intrusive usage of AppInfo.


using AppInfo.Definition;
using AppInfo.Output;

//TODO: Add simpler extension
AppInfoBuilder
	.CreateDefaultBuilder()
	.Build()
	.WithDefaultOutput()
	.Write();

Console.WriteLine("Application has started");
