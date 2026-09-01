using AppInformation.Renderers;
using AppInformation.Tests.TestHelpers;


namespace AppInformation.Tests.Renderers;


public class ConsoleRendererTests
{
	private ConsoleRenderer _sut;

	[SetUp]
	public void PreRun() =>
		_sut = new ConsoleRenderer();

	[Test]
	public void Render_should_render_app_info()
	{
		// Arrange

		var appInfo = AppInfo.CreateDefaultBuilder().Build();
		string? result;

		// Act

		using (var capture = new StdOutCapture())
		{
			_sut.Render(appInfo);
			result = capture.Captured.ToString();
		}

		// Assert

		result.ShouldNotBeNull();

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
