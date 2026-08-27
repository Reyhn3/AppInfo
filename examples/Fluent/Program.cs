// Demonstrates the different ways to use the
// fluent syntax and convenience methods.
//
// Also see the Manual example for creating
// each component separately.


using System.Globalization;
using AppInformation;

#region Basic usage
// This is the simples way to use AppInfo.
// It includes the default set of information extractors, and outputs to
// the console.
AppInfo
	.BuildAndOutputDefault();

// This is equivalent to the previous example.
AppInfo
	.CreateDefaultBuilder()
	.BuildAndWriteToDefault();

// This is also equivalent to the previous examples.
AppInfo
	.CreateEmptyBuilder()
	.UseCulture(CultureInfo.CurrentUICulture)
	.AddStandard()
	.BuildAndWriteTo(output => output
		.ToConsole());
#endregion

#region Customization
// This is the simplest way to customize the information extractors,
// and output to the console.
AppInfo
	.CreateDefaultBuilder()
//TODO: Use the convenience methods Add* or UseCulture here
	.BuildAndWriteToDefault();

// This uses the default information extraction, but allows customizing
// the rendering.
AppInfo
	.CreateDefaultBuilder()
	.BuildAndWriteTo(output => output
//TODO: Use the convenience methods To* to add renderers and write to them
		.ToConsole());
#endregion

#region Complete control
// Use this to completely customize the pipeline.
AppInfo
	.CreateEmptyBuilder()
//TODO: Use the convenience methods Add* or UseCulture here
	.BuildAndWriteTo(output =>
		{
//TODO: Use the convenience methods To* to add renderers and write to them
		});
#endregion
