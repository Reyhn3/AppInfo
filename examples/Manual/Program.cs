// This demonstrates the manual and the fluent way to
// configure and execute AppInfo.

using System.Globalization;
using System.Reflection;
using AppInfo;
using AppInfo.Extractors;
using AppInfo.Renderers;


//
// MANUAL
// This is the manual way to configure and execute AppInfo.
// This clearly demonstrates that extracting information and
// rendering are two separate concerns. Outputting the info
// is optional.
//

// First, define what information to include
var appInfoBuilder = new AppInfoBuilder();
appInfoBuilder.UseCulture(CultureInfo.CreateSpecificCulture("sv-SE"));
appInfoBuilder.AddExtractor(new StandardExtractor(Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly()));
appInfoBuilder.AddExtractor<TimestampExtractor>();

// Second, build the AppInfo
var appInfo = appInfoBuilder.Build();

// Third, declare what to do with the AppInfo
var appInfoOutputBuilder = new AppInfoOutputBuilder();
appInfoOutputBuilder.UseAppInfo(appInfo);
appInfoOutputBuilder.AddRenderer<ConsoleRenderer>();

// Fourth, execute the output
appInfoOutputBuilder.Write();


//
//
//
Console.WriteLine("--------------------------------------------------");
//
//
//


//
// FLUENT
// This does the same way as the manual way,
// but easier.
//

AppInfo.Create
	.DefaultBuilder() // This adds the standard extractor
	.UseCulture(CultureInfo.CreateSpecificCulture("sv-SE"))
	.AddTimestamp()
	.BuildAndWriteTo(output => output
		.ToConsole());
