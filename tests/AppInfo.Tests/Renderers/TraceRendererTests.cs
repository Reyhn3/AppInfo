using System.Diagnostics;
using AppInformation.Renderers;


namespace AppInformation.Tests.Renderers;


public class TraceRendererTests
{
	private TraceRenderer _sut;
	private StringWriter _writer;

	[SetUp]
	public void PreRun()
	{
		Trace.Listeners.Clear();
		Trace.Listeners.Add(
			new TextWriterTraceListener(
				_writer = new StringWriter()));

		_sut = new TraceRenderer();
	}

	[TearDown]
	public void PostRun() =>
		_writer.Dispose();

	[Test]
	public void Render_should_render_app_info()
	{
		// Arrange

		var appInfo = AppInfo.CreateDefaultBuilder().Build();

		// Act

		_sut.Render(appInfo);

		// Assert

		var result = _writer.ToString();
		result.ShouldNotBeNull();
		TestHelpers.Helpers.PrintCapturedOutput(result);

		// This is a close approximation of the expected output.
		// Most values depend on the environment.
		var lines = result.Split(Environment.NewLine);
		lines.ShouldContain(line => line.EndsWith(" created with context:"));
		lines.ShouldContain(line => line.StartsWith("  Product:     "));
		lines.ShouldContain(line => line.StartsWith("  Version:     "));
		lines.ShouldContain(line => line.StartsWith("  Assembly:    "));
		lines.ShouldContain(line => line.StartsWith("  File Name:   "));
		lines.ShouldContain(line => line.StartsWith("  Is Release:  "));
		lines.ShouldContain(line => line.StartsWith("  Culture:     "));
		lines.ShouldContain(line => line.StartsWith("  64-bit:      "));
		lines.ShouldContain(line => line.StartsWith("  Location:    "));
		lines.ShouldContain(line => line.StartsWith("  Base:        "));
		lines.ShouldContain(line => line.StartsWith("  Environment: "));
		lines.ShouldContain(line => line.StartsWith("  MachineName: "));
		lines.ShouldContain(line => line.StartsWith("  OSVersion:   "));
		lines.ShouldContain(line => line.StartsWith("  ClrVersion:  "));
		lines.ShouldContain(line => line.StartsWith("  ProcessId:   "));
	}
}
