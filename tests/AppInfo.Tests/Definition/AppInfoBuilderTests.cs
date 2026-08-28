using System.Globalization;
using AppInformation.Extractors;


namespace AppInformation.Tests.Definition;


public class AppInfoBuilderTests
{
	private AppInfoBuilder _sut;
	private string _extractorsFieldName = "_extractors";

	[SetUp]
	public void PreRun() =>
		_sut = new AppInfoBuilder();

	[Test]
	public void Ctor_should_populate_culture_field() =>
		Helpers.GetFieldValue(new AppInfoBuilder(), "_culture")
			.ShouldNotBeNull()
			.ShouldBeOfType<CultureInfo>()
			.ShouldBe(Constants.DefaultCulture);

	[Test]
	public void Ctor_should_populate_extractors_field() =>
		Helpers.GetFieldValue(new AppInfoBuilder(), _extractorsFieldName)
			.ShouldNotBeNull()
			.ShouldBeOfType<List<IExtractor>>()
			.ShouldBeEmpty();

	[Test]
	public void UseCulture_shall_do_nothing_if_culture_is_null()
	{
		_sut.UseCulture(null);
		Helpers.GetFieldValue(_sut, "_culture")
			.ShouldBe(Constants.DefaultCulture);
	}

	[Test]
	public void AddExtractor_with_instance_shall_do_nothing_if_instance_is_null()
	{
		_sut.AddExtractor((IExtractor)null!);
		Helpers.GetFieldValue<List<IExtractor>>(_sut, _extractorsFieldName)
			.ShouldBeEmpty();
	}

	[Test]
	public void AddExtractor_with_instance_shall_do_nothing_if_instance_is_already_added()
	{
		var extractor = A.Fake<IExtractor>();
		Helpers.GetFieldValue<List<IExtractor>>(_sut, _extractorsFieldName)
			.ShouldBeEmpty();
		_sut.AddExtractor(extractor);
		Helpers.GetFieldValue<List<IExtractor>>(_sut, _extractorsFieldName)!
			.Count.ShouldBe(1);
		_sut.AddExtractor(extractor);
		Helpers.GetFieldValue<List<IExtractor>>(_sut, _extractorsFieldName)!
			.Count.ShouldBe(1);
	}

	[Test]
	public void AddExtractor_with_instance_shall_add_the_instance_to_the_collection()
	{
		var extractor = A.Fake<IExtractor>();
		Helpers.GetFieldValue<List<IExtractor>>(_sut, _extractorsFieldName)
			.ShouldBeEmpty();
		_sut.AddExtractor(extractor);
		Helpers.GetFieldValue<List<IExtractor>>(_sut, _extractorsFieldName)!
			.Count.ShouldBe(1);
	}
}
