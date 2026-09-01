using AppInformation.Renderers;


namespace AppInformation.Tests.Output;


public class OutputExtensionsTests
{
	private const string AppInfoFieldName = "_appInfo";
	private const string RenderersFieldName = "_renderers";

	private OutputBuilder _sut;

	[SetUp]
	public void PreRun() =>
		_sut = new OutputBuilder();

#region WithDefaultOutput
	[Test]
	public void WithDefaultOutput_shall_create_output_builder_with_default_renderer()
	{
		// Arrange

		var sut = A.Fake<IAppInfo>();

		// Act

		var result = sut.WithDefaultOutput();

		// Assert

		result.ShouldNotBeNull();

		var appInfo = TestHelpers.Helpers.GetFieldValue<IAppInfo>(result, AppInfoFieldName);
		appInfo.ShouldNotBeNull();
		appInfo.ShouldBe(sut);

		var renderers = TestHelpers.Helpers.GetFieldValue<List<IRenderer>>(result, RenderersFieldName);
		renderers.ShouldNotBeNull();
		renderers.Count.ShouldBe(1);
		renderers.Single().ShouldBeOfType<ConsoleRenderer>();
	}
#endregion

#region WithOutput
	[Test]
	public void WithOutput_shall_create_empty_output_builder_with_the_configured_renderers()
	{
		// Arrange

		var sut = A.Fake<IAppInfo>();

		// Act

		var result = sut
			.WithOutput(output => output
				.ToTrace()
				.ToJsonFile());

		// Assert

		result.ShouldNotBeNull();

		var appInfo = TestHelpers.Helpers.GetFieldValue<IAppInfo>(result, AppInfoFieldName);
		appInfo.ShouldNotBeNull();
		appInfo.ShouldBe(sut);

		var renderers = TestHelpers.Helpers.GetFieldValue<List<IRenderer>>(result, RenderersFieldName);
		renderers.ShouldNotBeNull();
		renderers.Count.ShouldBe(2);
		renderers[0].ShouldBeOfType<TraceRenderer>();
		renderers[1].ShouldBeOfType<JsonFileRenderer>();
	}
#endregion

#region ToConsole
	[Test]
	public void ToConsole_should_add_console_renderer_to_output_builder()
	{
		var result = _sut.ToConsole();

		result.ShouldNotBeNull();
		var renderers = TestHelpers.Helpers.GetFieldValue<List<IRenderer>>(result, RenderersFieldName);
		renderers.ShouldNotBeNull();
		renderers.Count.ShouldBe(1);
		renderers.Single().ShouldBeOfType<ConsoleRenderer>();
	}
#endregion

#region ToTrace
	[Test]
	public void ToTrace_should_add_trace_renderer_to_output_builder()
	{
		var result = _sut.ToTrace();

		result.ShouldNotBeNull();
		var renderers = TestHelpers.Helpers.GetFieldValue<List<IRenderer>>(result, RenderersFieldName);
		renderers.ShouldNotBeNull();
		renderers.Count.ShouldBe(1);
		renderers.Single().ShouldBeOfType<TraceRenderer>();
	}
#endregion

#region ToLog
	[Test]
	public void ToLog_should_add_log_renderer_to_output_builder()
	{
		var result = _sut.ToLog((_, _) => {});

		result.ShouldNotBeNull();
		var renderers = TestHelpers.Helpers.GetFieldValue<List<IRenderer>>(result, RenderersFieldName);
		renderers.ShouldNotBeNull();
		renderers.Count.ShouldBe(1);
		renderers.Single().ShouldBeOfType<LogRenderer>();
	}
#endregion

#region ToTextFile
	[Test]
	public void ToTextFile_should_add_text_file_renderer_to_output_builder()
	{
		var result = _sut.ToTextFile();

		result.ShouldNotBeNull();
		var renderers = TestHelpers.Helpers.GetFieldValue<List<IRenderer>>(result, RenderersFieldName);
		renderers.ShouldNotBeNull();
		renderers.Count.ShouldBe(1);
		renderers.Single().ShouldBeOfType<TextFileRenderer>();
	}
#endregion

#region ToJsonFile
	[Test]
	public void ToJsonFile_should_add_json_file_renderer_to_output_builder()
	{
		var result = _sut.ToJsonFile();

		result.ShouldNotBeNull();
		var renderers = TestHelpers.Helpers.GetFieldValue<List<IRenderer>>(result, RenderersFieldName);
		renderers.ShouldNotBeNull();
		renderers.Count.ShouldBe(1);
		renderers.Single().ShouldBeOfType<JsonFileRenderer>();
	}
#endregion
}
