using System.Globalization;
using System.Text.Json;
using AppInformation.Serializers;
using AppInformation.Tests.Helpers;


namespace AppInformation.Tests.Serializers;


public class AppInfoConverterTests
{
	private static readonly JsonWriterOptions s_jsonWriterOptions = new JsonWriterOptions
		{
			Indented = true
		};

	private AppInfoConverter _sut;

	[SetUp]
	public void PreRun() =>
		_sut = new AppInfoConverter();

	[Explicit("Intended for manual verification")]
	[Test]
	public void Build_AppInfo_and_serialize_to_JSON()
	{
		var appInfo = AppInfo.CreateDefaultBuilder().Build();
		var result = _sut.Write((AppInfo)appInfo, writerOptions: s_jsonWriterOptions);
		result.ShouldNotBeNullOrWhiteSpace();
		TestHelpers.Helpers.PrintCapturedOutput(result);
	}

	[Description("Deserialization is not allowed")]
	[Test]
	public void Read_should_return_null()
	{
		const string json = """
							[
							  {
							    "Label": "test-label-1",
							    "Value": null
							  }
							]
							""";
		var result = JsonSerializer.Deserialize<AppInfo>(json);
		result.ShouldBeNull();
	}

	[Test]
	public void Write_should_serialize_the_whole_object_as_a_fragment_array()
	{
		// Arrange

		var expected = """
								[
								  {
								    "Label": "test-label-1",
								    "Value": null
								  },
								  {
								    "Label": "test-label-2",
								    "Value": []
								  },
								  {
								    "Label": "test-label-3",
								    "Value": [
								      true
								    ]
								  },
								  {
								    "Label": "test-label-4",
								    "Value": [
								      "test-value-4"
								    ]
								  },
								  {
								    "Label": "test-label-5",
								    "Value": [
								      2147483647
								    ]
								  },
								  {
								    "Label": "test-label-6",
								    "Value": [
								      1234.5678
								    ]
								  },
								  {
								    "Label": "test-label-7",
								    "Value": [
								      null,
								      [],
								      true,
								      "test-value-7",
								      2147483647,
								      1234.5678
								    ]
								  }
								]
								""".ReplaceLineEndings();
		var fragments = new Fragment[]
			{
				new("test-label-1", null),
				new("test-label-2", Array.Empty<object?>()),
				new("test-label-3", true),
				new("test-label-4", "test-value-4"),
				new("test-label-5", int.MaxValue),
				new("test-label-6", 1234.5678m),
				new("test-label-7", null, Array.Empty<object?>(), true, "test-value-7", int.MaxValue, 1234.5678m)
			};
		var appInfo = new AppInfo(A.Dummy<CultureInfo>(), fragments);

		// Act

		var result = _sut.Write(appInfo, writerOptions: s_jsonWriterOptions);

		// Assert

		result.ShouldNotBeNullOrWhiteSpace();
		result.ReplaceLineEndings().ShouldBe(expected);
	}
}
