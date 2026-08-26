// Demonstrates the different ways to use the
// fluent syntax and convenience methods.


using System.Globalization;
using AppInformation;

AppInfo
	.CreateDefaultBuilder() // This adds the standard extractor
	.UseCulture(CultureInfo.CreateSpecificCulture("sv-SE"))
	.AddTimestamp()
	.BuildAndWriteTo(output => output
		.ToConsole());

Console.WriteLine("Application has started");
