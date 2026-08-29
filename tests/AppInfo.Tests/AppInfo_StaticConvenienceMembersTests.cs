using System.Globalization;
using AppInformation.Extractors;
using AppInformation.Renderers;
using AppInformation.Tests.TestHelpers;


namespace AppInformation.Tests;


public class AppInfo_StaticConvenienceMembersTests
{
	private const string AppInfoFieldName = "_appInfo";

#region BuildAndOutputDefault
	[Test]
	public void BuildAndOutputDefault_should_create_the_default_builder_and_default_output_then_write()
	{
		// Arrange

		IAppInfo result;
		string? output;

		// Act

		using (var capture = new StdOutCapture())
		{
			result = AppInfo.BuildAndOutputDefault();
			output = capture.Captured.ToString();
		}

		// Assert

		result.ShouldNotBeNull();
		result.Culture.ShouldBe(Constants.DefaultCulture);
		result.Fragments.ShouldNotBeEmpty();

		TestHelpers.Helpers.PrintCapturedOutput(output);
		output.ShouldNotBeEmpty();
		output.ShouldStartWith("Application");
		output.ShouldContain("ReSharperTestRunner");
	}
#endregion

#region CreateEmptyBuilder
	[Test]
	public void CreateEmptyBuilder_shall_create_new_builder_without_culture_or_extractors()
	{
		var result = AppInfo.CreateEmptyBuilder();

		result.ShouldNotBeNull();

		TestHelpers.Helpers.GetFieldValue(result, "_culture")
			.ShouldNotBeNull()
			.ShouldBe(Constants.DefaultCulture);

		var extractors = TestHelpers.Helpers.GetFieldValue<List<IExtractor>>(result, "_extractors");
		extractors.ShouldNotBeNull();
		extractors.ShouldBeEmpty();
	}
#endregion

#region CreateDefaultBuilder
	[Test]
	public void CreateDefaultBuilder_shall_return_new_builder() =>
		AppInfo.CreateDefaultBuilder()
			.ShouldNotBeNull();

	[Test]
	public void CreateDefaultBuilder_shall_set_culture_to_CurrentUICulture() =>
		TestHelpers.Helpers.GetFieldValue(AppInfo.CreateDefaultBuilder(), "_culture")
			.ShouldNotBeNull()
			.ShouldBe(CultureInfo.CurrentUICulture);

	[Test]
	public void CreateDefaultBuilder_shall_add_standard_extractor()
	{
		var result = TestHelpers.Helpers.GetFieldValue(AppInfo.CreateDefaultBuilder(), "_extractors");
		result.ShouldNotBeNull();
		result.ShouldBeOfType<List<IExtractor>>();

		var extractors = result as List<IExtractor>;
		extractors.ShouldNotBeEmpty();
		extractors.Count.ShouldBe(1);

		var extractor = extractors.Single();
		extractor.ShouldBeOfType<StandardExtractor>();
	}
#endregion

#region CreateEmptyOutputBuilder
	[Test]
	public void CreateEmptyOutputBuilder_should_create_output_builder_with_empty_renderers()
	{
		var appInfo = A.Dummy<IAppInfo>();

		var result = AppInfo.CreateEmptyOutputBuilder(appInfo);
		result.ShouldNotBeNull();

		TestHelpers.Helpers.GetFieldValue(result, AppInfoFieldName)
			.ShouldNotBeNull()
			.ShouldBe(appInfo);

		var renderers = TestHelpers.Helpers.GetFieldValue<List<IRenderer>>(result, "_renderers");
		renderers.ShouldNotBeNull();
		renderers.ShouldBeOfType<List<IRenderer>>();
		renderers.ShouldBeEmpty();
	}
#endregion

#region CreateDefaultOutputBuilder
	[Test]
	public void CreateDefaultOutputBuilder_should_create_output_builder_with_console_renderer()
	{
		var appInfo = A.Dummy<IAppInfo>();

		var result = AppInfo.CreateDefaultOutputBuilder(appInfo);
		result.ShouldNotBeNull();

		TestHelpers.Helpers.GetFieldValue(result, AppInfoFieldName)
			.ShouldNotBeNull()
			.ShouldBe(appInfo);

		var renderers = TestHelpers.Helpers.GetFieldValue<List<IRenderer>>(result, "_renderers");
		renderers.ShouldNotBeNull();
		renderers.ShouldBeOfType<List<IRenderer>>();

		var extractor = renderers.Single();
		extractor.ShouldBeOfType<ConsoleRenderer>();
	}
#endregion
}
