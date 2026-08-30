using System.Reflection;
using AppInformation.Extractors;
using AppInformation.Renderers;
using AppInformation.Tests.TestHelpers;


namespace AppInformation.Tests.Definition;


public class DefinitionExtensionsTests
{
	private const string ExtractorsFieldName = "_extractors";
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

		var renderer = A.Fake<IRenderer>();

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
		output.ShouldBeEmpty("Did not expect to find any standard output");

		A.CallTo(() => renderer.Render(A<IAppInfo>.Ignored))
			.MustHaveHappenedOnceExactly();
	}
#endregion

#region AddStandard
	[Test]
	public void AddStandard_shall_add_the_standard_extractor()
	{
		var result = _sut.AddStandard();
		result.ShouldNotBeNull();

		var extractors = TestHelpers.Helpers.GetFieldValue<List<IExtractor>>(_sut, ExtractorsFieldName);
		extractors.ShouldNotBeNull();
		extractors.ShouldNotBeEmpty();
		extractors.ShouldContain(e => e is StandardExtractor);
	}
#endregion

#region WithIdentities
	[Test]
	public void WithIdentities_shall_do_nothing_if_trying_to_add_the_identity_extractor_throws_exception()
	{
		var result = _sut.WithIdentities(null);
		result.ShouldNotBeNull();

		var extractors = TestHelpers.Helpers.GetFieldValue<List<IExtractor>>(_sut, ExtractorsFieldName);
		extractors.ShouldNotBeNull();
		extractors.ShouldBeEmpty();
	}

	[Test]
	public void WithIdentities_shall_add_the_identity_extractor()
	{
		var result = _sut.WithIdentities("test-app-id");
		result.ShouldNotBeNull();

		var extractors = TestHelpers.Helpers.GetFieldValue<List<IExtractor>>(_sut, ExtractorsFieldName);
		extractors.ShouldNotBeNull();
		extractors.ShouldNotBeEmpty();
		extractors.ShouldContain(e => e is IdentityExtractor);
	}
#endregion

#region AddTimestamp
	[Test]
	public void AddTimestamp_shall_add_the_timestamp_extractor()
	{
		var result = _sut.AddTimestamp();
		result.ShouldNotBeNull();

		var extractors = TestHelpers.Helpers.GetFieldValue<List<IExtractor>>(_sut, ExtractorsFieldName);
		extractors.ShouldNotBeNull();
		extractors.ShouldNotBeEmpty();
		extractors.ShouldContain(e => e is TimestampExtractor);
	}
#endregion

#region AddExtra
	[Test]
	public void AddExtra_with_single_KVP_shall_add_the_timestamp_extractor()
	{
		var result = _sut.AddExtra("test-label", "test-value");
		result.ShouldNotBeNull();

		var extractors = TestHelpers.Helpers.GetFieldValue<List<IExtractor>>(_sut, ExtractorsFieldName);
		extractors.ShouldNotBeNull();
		extractors.ShouldNotBeEmpty();
		extractors.ShouldContain(e => e is ExtrasExtractor);
	}

	[Test]
	public void AddExtra_with_single_value_factory_shall_add_the_timestamp_extractor()
	{
		var result = _sut.AddExtra("test-label", () => "test-value");
		result.ShouldNotBeNull();

		var extractors = TestHelpers.Helpers.GetFieldValue<List<IExtractor>>(_sut, ExtractorsFieldName);
		extractors.ShouldNotBeNull();
		extractors.ShouldNotBeEmpty();
		extractors.ShouldContain(e => e is ExtrasExtractor);
	}

	[Test]
	public void AddExtra_with_multiple_KVP_shall_add_the_timestamp_extractor()
	{
		var result = _sut.AddExtra(
			("test-label-1", "test-value-1"),
			("test-label-2", "test-value-2"));
		result.ShouldNotBeNull();

		var extractors = TestHelpers.Helpers.GetFieldValue<List<IExtractor>>(_sut, ExtractorsFieldName);
		extractors.ShouldNotBeNull();
		extractors.ShouldNotBeEmpty();
		extractors.ShouldContain(e => e is ExtrasExtractor);
	}

	[Test]
	public void AddExtra_with_multiple_value_factory_shall_add_the_timestamp_extractor()
	{
		var result = _sut.AddExtra(
			("test-label-1", () => "test-value-1"),
			("test-label-2", () => "test-value-2"));
		result.ShouldNotBeNull();

		var extractors = TestHelpers.Helpers.GetFieldValue<List<IExtractor>>(_sut, ExtractorsFieldName);
		extractors.ShouldNotBeNull();
		extractors.ShouldNotBeEmpty();
		extractors.ShouldContain(e => e is ExtrasExtractor);
	}
#endregion

#region AddAssembly
	[Test]
	public void AddAssembly_shall_add_the_assembly_extractor()
	{
		var result = _sut.AddAssembly(A.Dummy<Assembly>());
		result.ShouldNotBeNull();

		var extractors = TestHelpers.Helpers.GetFieldValue<List<IExtractor>>(_sut, ExtractorsFieldName);
		extractors.ShouldNotBeNull();
		extractors.ShouldNotBeEmpty();
		extractors.ShouldContain(e => e is AssemblyExtractor);
	}
#endregion
}
