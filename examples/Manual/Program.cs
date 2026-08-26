// This demonstrates the manual way to
// configure and execute AppInfo.
// It clearly demonstrates that extracting information and
// rendering are two separate concerns. Outputting the info
// is optional.

using System.Globalization;
using System.Reflection;
using AppInformation;
using AppInformation.Extractors;
using AppInformation.Renderers;


//
// MANUAL
// This is the manual way to configure and execute AppInfo.
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

Console.WriteLine("Application has started");
