using AppInformation.Tests.TestHelpers;


namespace AppInformation.Tests.Definition;


public class DefinitionExtensionsTests
{
	private AppInfoBuilder _sut;

	[SetUp]
	public void PreRun() =>
		_sut = new AppInfoBuilder();

#region BuildAndWriteToDefault
	[Test]
	public void BuildAndWriteToDefault_should_create_default_builder_and_write_to_default_output()
	{
		// Arrange

		IAppInfo result;
		string? output;


		// Act

		using (var capture = new StdOutCapture())
		{
			result = _sut.BuildAndWriteToDefault();
			output = capture.Captured.ToString();
		}


		// Assert

		result.ShouldNotBeNull();
		TestHelpers.Helpers.PrintCapturedOutput(output);
		output.ShouldNotBeEmpty();
		output.ShouldContain("created with context:");
	}
#endregion

#region BuildAndWriteTo
	[Test]
	public void BuildAndWriteTo_should_create_default_builder_and_write_to_the_configured_renderers()
	{
		// Arrange

		var renderer = new TestRenderer();

		IAppInfo result;
		string? output;


		// Act

		using (var capture = new StdOutCapture())
		{
			result = _sut.BuildAndWriteTo(config => config
				.AddRenderer(renderer));
			output = capture.Captured.ToString();
		}


		// Assert

		result.ShouldNotBeNull();
		TestHelpers.Helpers.PrintCapturedOutput(output);
		renderer.HasRenderBeenCalled.ShouldBeTrue();
		renderer.AppInfo.ShouldNotBeNull();
		output.ShouldBeEmpty("Did not expect to find any standard output");
	}
#endregion
}
