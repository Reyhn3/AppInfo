using System.Globalization;
using AppInformation.Renderers;


namespace AppInformation.Tests.Renderers;


public class UnstructuredTextRendererTests
{
	private StubUnstructuredTextRenderer _sut;

	[SetUp]
	public void PreRun() =>
		_sut = new StubUnstructuredTextRenderer();

#region CalculateLabelMaxWidth
	[TestCase(1, 1)]
	[TestCase(UnstructuredTextRenderer.MaxLabelWidth + 1, UnstructuredTextRenderer.MaxLabelWidth)]
	public void CalculateLabelMaxWidth_should_return_the_maximum_width_of_all_labels(int actualWidth, int expectedWidth)
	{
		var info = CreateFakeAppInfo([new Fragment(new string('*', actualWidth))]);
		var result = StubUnstructuredTextRenderer.CalculateLabelMaxWidth(info);
		result.ShouldBe(expectedWidth);
	}

	[Test]
	public void CalculateLabelMaxWidth_should_return_zero_if_there_are_no_labels()
	{
		var info = CreateFakeAppInfo([]);
		var result = StubUnstructuredTextRenderer.CalculateLabelMaxWidth(info);
		result.ShouldBe(0);
	}

	[Test]
	public void CalculateLabelMaxWidth_should_use_trimmed_length_of_labels()
	{
		var info = CreateFakeAppInfo([new Fragment("\t* ")]);
		var result = StubUnstructuredTextRenderer.CalculateLabelMaxWidth(info);
		result.ShouldBe(1);
	}
#endregion

#region PadLabel
	[Test]
	public void PadLabel_shall_suffix_label_with_separator() =>
		StubUnstructuredTextRenderer.PadLabel("test", 10)
			.ShouldBe("test:       ");

	[Test]
	public void PadLabel_shall_shorten_label_to_fit_width() =>
		StubUnstructuredTextRenderer.PadLabel("abcdefghijkl", 8)
			.ShouldBe("abcdefg…: ");

	[TestCase(null)]
	[TestCase("")]
	[TestCase("\t")]
	public void PadLabel_shall_return_empty_string_if_label_is_empty(string? actual) =>
		StubUnstructuredTextRenderer.PadLabel(actual, 8)
			.ShouldBe(string.Empty);
#endregion

#region FormatValue
	[Test]
	public void FormatValue_shall_return_null_string_for_null() =>
		_sut.FormatValue(null)
			.ShouldBe("<null>");

	[TestCase("")]
	[TestCase("\t")]
	public void FormatValue_shall_return_empty_string_for_empty_strings(string? actual) =>
		_sut.FormatValue(actual)
			.ShouldBe("<empty>");

	[TestCase(false, "false")]
	[TestCase(true, "true")]
	public void FormatValue_shall_return_lowercase_string_for_booleans(bool actual, string expected) =>
		_sut.FormatValue(actual)
			.ShouldBe(expected);

	[Test]
	public void FormatValue_shall_return_value_formatted_with_culture_for_type_decimal()
	{
		var appInfo = CreateFakeAppInfo([]);
		A.CallTo(() => appInfo.Culture)
			.Returns(CultureInfo.CreateSpecificCulture("sv-SE"));
		_sut.Render(appInfo); // This is required to initialize the renderer with the CultureInfo

		var result = _sut.FormatValue(1.2345m);
		result.ShouldBe("1,2345");
	}

	[Test]
	public void FormatValue_shall_return_value_formatted_with_culture_for_type_integer()
	{
		var appInfo = CreateFakeAppInfo([]);
		A.CallTo(() => appInfo.Culture)
			.Returns(CultureInfo.CreateSpecificCulture("sv-SE"));
		_sut.Render(appInfo); // This is required to initialize the renderer with the CultureInfo

		var result = _sut.FormatValue(1234567);
		result.ShouldBe("1234567");
	}

	[Test]
	public void FormatValue_shall_return_value_formatted_with_culture_for_type_DateTime()
	{
		var appInfo = CreateFakeAppInfo([]);
		A.CallTo(() => appInfo.Culture)
			.Returns(CultureInfo.CreateSpecificCulture("sv-SE"));
		_sut.Render(appInfo); // This is required to initialize the renderer with the CultureInfo

		var result = _sut.FormatValue(new DateTime(2026, 08, 31, 13, 24, 35));
		result.ShouldBe("2026-08-31 13:24:35");
	}
#endregion

	private static IAppInfo CreateFakeAppInfo(IEnumerable<Fragment> fragments) =>
		A.Fake<IAppInfo>(f => f.ConfigureFake(ff =>
			A.CallTo(() => ff.Fragments)
				.Returns(fragments)));


	private class StubUnstructuredTextRenderer : UnstructuredTextRenderer
	{
		protected override void RenderAppInfo(IAppInfo info) =>
			throw new NotImplementedException();

		public static new int CalculateLabelMaxWidth(IAppInfo info) =>
			UnstructuredTextRenderer.CalculateLabelMaxWidth(info);

		public static new string PadLabel(string label, int width) =>
			UnstructuredTextRenderer.PadLabel(label, width);

		public new string FormatValue(object? value) =>
			base.FormatValue(value);
	}
}
