using System.Globalization;
using AppInformation.Extractors;


namespace AppInformation.Tests;


public class AppInfo_StaticConvenienceMembersTests
{
	[Test]
	public void DefaultBuilder_shall_return_new_builder() =>
		AppInfo.CreateDefaultBuilder()
			.ShouldNotBeNull();

	[Test]
	public void DefaultBuilder_shall_set_culture_to_CurrentUICulture() =>
		Helpers.GetFieldValue(AppInfo.CreateDefaultBuilder(), "_culture")
			.ShouldNotBeNull()
			.ShouldBe(CultureInfo.CurrentUICulture);

	[Test]
	public void DefaultBuilder_shall_add_standard_extractor()
	{
		var result = Helpers.GetFieldValue(AppInfo.CreateDefaultBuilder(), "_extractors");
		result.ShouldNotBeNull();
		result.ShouldBeOfType<List<IExtractor>>();

		var extractors = result as List<IExtractor>;
		extractors.ShouldNotBeEmpty();
		extractors.Count.ShouldBe(1);

		var extractor = extractors.Single();
		extractor.ShouldBeOfType<StandardExtractor>();
	}
}
