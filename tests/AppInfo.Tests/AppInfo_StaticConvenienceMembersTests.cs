using System.Globalization;
using System.Reflection;
using AppInformation.Extractors;


namespace AppInformation.Tests;


public class AppInfo_StaticConvenienceMembersTests
{
	private static readonly Type s_type = typeof(AppInfoBuilder);
	private static readonly FieldInfo s_culture = s_type.GetField("_culture", BindingFlags.Instance | BindingFlags.NonPublic)!;
	private static readonly FieldInfo s_extractors = s_type.GetField("_extractors", BindingFlags.Instance | BindingFlags.NonPublic)!;


	[Test]
	public void DefaultBuilder_shall_return_new_builder() =>
		AppInfo.CreateDefaultBuilder()
			.ShouldNotBeNull();

	[Test]
	public void DefaultBuilder_shall_set_culture_to_CurrentUICulture() =>
		s_culture.GetValue(AppInfo.CreateDefaultBuilder())
			.ShouldNotBeNull()
			.ShouldBe(CultureInfo.CurrentUICulture);

	[Test]
	public void DefaultBuilder_shall_add_standard_extractor()
	{
		var result = s_extractors.GetValue(AppInfo.CreateDefaultBuilder());
		result.ShouldNotBeNull();
		result.ShouldBeOfType<List<IExtractor>>();

		var extractors = result as List<IExtractor>;
		extractors.ShouldNotBeEmpty();
		extractors.Count.ShouldBe(1);

		var extractor = extractors.Single();
		extractor.ShouldBeOfType<StandardExtractor>();
	}
}
