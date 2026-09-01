using System.Text;
using AppInformation.Renderers;


namespace AppInformation.Tests.Renderers;


public class LogRendererTests
{
	private LogRenderer _sut;
	private StringBuilder _output;

	[SetUp]
	public void PreRun()
	{
		_output = new StringBuilder();
		_sut = new LogRenderer((format, args) =>
			_output.AppendLine(format));
	}

#region Ctor
	[Test]
	public void Ctor_shall_use_noop_logger_if_logger_is_null() =>
		Should.NotThrow(() => new LogRenderer(null));
#endregion

#region Render
	[Test]
	public void Render_should_render_app_info()
	{
		// Arrange

		var appInfo = AppInfo.CreateDefaultBuilder().Build();

		// Act

		_sut.Render(appInfo);

		// Assert

		var result = _output.ToString();
		result.ShouldNotBeNull();
		TestHelpers.Helpers.PrintCapturedOutput(result);

		var lines = result.Split(Environment.NewLine);
		lines.ShouldContain(line => line.EndsWith(" created with context:"));
		lines.ShouldContain(line => line.StartsWith("{Product}"));
		lines.ShouldContain(line => line.StartsWith("{Version}"));
		lines.ShouldContain(line => line.StartsWith("{Assembly}"));
		lines.ShouldContain(line => line.StartsWith("{FileName}"));
		lines.ShouldContain(line => line.StartsWith("{IsRelease}"));
		lines.ShouldContain(line => line.StartsWith("{Culture}"));
		lines.ShouldContain(line => line.StartsWith("{64Bit}"));
		lines.ShouldContain(line => line.StartsWith("{Location}"));
		lines.ShouldContain(line => line.StartsWith("{Base}"));
		lines.ShouldContain(line => line.StartsWith("{Environment}"));
		lines.ShouldContain(line => line.StartsWith("{MachineName}"));
		lines.ShouldContain(line => line.StartsWith("{OSVersion}"));
		lines.ShouldContain(line => line.StartsWith("{ClrVersion}"));
		lines.ShouldContain(line => line.StartsWith("{ProcessId}"));
	}
#endregion

#region IsScalar
	[Test]
	public void IsScalar_shall_return_true_for_null() =>
		LogRenderer.IsScalar(null).ShouldBeTrue();

	[Test]
	public void IsScalar_shall_return_true_for_single_element_enumerable() =>
		LogRenderer.IsScalar(Enumerable.Repeat((object?)null, 1)).ShouldBeTrue();

	[Test]
	public void IsScalar_shall_return_true_for_single_string() =>
		LogRenderer.IsScalar(new object?[]
				{
					"test"
				})
			.ShouldBeTrue();

	[Test]
	public void IsScalar_shall_return_false_for_multiple_strings() =>
		LogRenderer.IsScalar(Enumerable.Repeat((object?)"test", 2)).ShouldBeFalse();

	[Test]
	public void IsScalar_shall_return_false_for_multiple_element_enumerable() =>
		LogRenderer.IsScalar(Enumerable.Repeat((object?)null, 2)).ShouldBeFalse();

	[Test]
	public void IsScalar_shall_return_false_for_empty_enumerable() =>
		LogRenderer.IsScalar([]).ShouldBeFalse();
#endregion

#region FormatName
	[TestCase("myValue", "MyValue")]
	[TestCase("my Value", "MyValue")]
	[TestCase("my value", "MyValue")]
	public void FormatName_shall_convert_input_to_pascal_case(string actual, string expected) =>
		LogRenderer.FormatName(actual).ShouldBe(expected);

	[TestCase("my value", "MyValue")]
	[TestCase(" my value ", "MyValue")]
	[TestCase("\u2002my\tvalue\r\n", "MyValue")]
	public void FormatName_shall_remove_whitespace(string actual, string expected) =>
		LogRenderer.FormatName(actual).ShouldBe(expected);

	[TestCase("64-bit", "64Bit")]
	public void FormatName_shall_remove_non_alphanumerics(string actual, string expected) =>
		LogRenderer.FormatName(actual).ShouldBe(expected);
#endregion

#region CalculateSuffix
	[Test]
	public void CalculateSuffix_shall_return_null_for_unique_labels()
	{
		var fragments = new Fragment[]
			{
				new("a", A.Dummy<object?>()),
				new("b", A.Dummy<object?>())
			};
		LogRenderer.CalculateSuffix(fragments, fragments.First()).ShouldBeNull();
		LogRenderer.CalculateSuffix(fragments, fragments.Last()).ShouldBeNull();
	}

	[Test]
	public void CalculateSuffix_shall_return_a_zero_based_index_for_non_unique_labels()
	{
		var fragments = new Fragment[]
			{
				new("a", A.Dummy<object?>()),
				new("b", A.Dummy<object?>()),
				new("c", A.Dummy<object?>()),
				new("b", A.Dummy<object?>()),
				new("a", A.Dummy<object?>())
			};

		LogRenderer.CalculateSuffix(fragments, fragments[0]).ShouldBe(0);
		LogRenderer.CalculateSuffix(fragments, fragments[4]).ShouldBe(1);
		LogRenderer.CalculateSuffix(fragments, fragments[1]).ShouldBe(0);
		LogRenderer.CalculateSuffix(fragments, fragments[3]).ShouldBe(1);
		LogRenderer.CalculateSuffix(fragments, fragments[2]).ShouldBeNull();
	}
#endregion
}
