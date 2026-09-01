using AppInformation.Extractors;


namespace AppInformation.Tests.Extractors;


public class AppSettingsReaderTests
{
	private const string AppsettingsFileName = "appsettings.json";

	[Test]
	public void ReadTopLevelKeyFromAppSettings_should_return_value_from_appsettings_json()
	{
		var json = "{\"test-property\":\"test-value\"}";
		File.WriteAllText(AppsettingsFileName, json);

		var result = AppSettingsReader.ReadTopLevelKeyFromAppSettings("test-property");

		File.Delete(AppsettingsFileName);

		result.ShouldNotBeNull();
		result.ToString().ShouldBe("test-value");
	}
}
