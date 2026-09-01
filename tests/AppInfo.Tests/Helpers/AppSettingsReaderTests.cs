using AppInformation.Helpers;


namespace AppInformation.Tests.Helpers;


public class AppSettingsReaderTests
{
	private const string AppSettingsFileName = "appsettings.json";

	[Test]
	public void ReadTopLevelKeyFromAppSettings_should_return_value_from_appsettings_json()
	{
		var json = "{\"test-property\":\"test-value\"}";
		File.WriteAllText(AppSettingsFileName, json);

		var result = AppSettingsReader.ReadTopLevelKeyFromAppSettings("test-property");

		File.Delete(AppSettingsFileName);

		result.ShouldNotBeNull();
		result.ToString().ShouldBe("test-value");
	}
}
