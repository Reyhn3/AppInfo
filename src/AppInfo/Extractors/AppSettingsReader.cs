using System.Diagnostics;
using System.Text.Json;
using AppInformation.Helpers;


namespace AppInformation.Extractors;


internal static class AppSettingsReader
{
	private static readonly JsonSerializerOptions s_jsonSerializerOptions = new()
		{
			ReadCommentHandling = JsonCommentHandling.Skip
		};

	public static object? ReadTopLevelKeyFromAppSettings(string name)
	{
		// This is a best effort attempt to get a configured value for ApplicationInstanceId
		try
		{
			return GetAppSettingsFilePaths()
				.Where(SafelyCheckIfFileExists)
				.Select(filePath => TryReadFromAppConfig(name, filePath))
				.FirstOrDefault();
		}
		catch
		{
			// ignore
			return null;
		}
	}

	private static IEnumerable<string> GetAppSettingsFilePaths()
	{
		var environmentName = EnvironmentHelper.GetEnvironment();
		yield return $"appsettings.{environmentName}.json";
		yield return "appsettings.json";
	}

	private static bool SafelyCheckIfFileExists(string filePath)
	{
		try
		{
			return File.Exists(filePath);
		}
		catch (Exception ex)
		{
			InternalLogger.Log("Exception caught when trying to check if file '{0}' exists: {1}", filePath, ex);
			return false;
		}
	}

	private static object? TryReadFromAppConfig(string propertyName, string filePath)
	{
		try
		{
			using var file = File.OpenRead(filePath);
			var json = JsonSerializer.Deserialize<Dictionary<string, object>>(file, s_jsonSerializerOptions);
			if (json?.TryGetValue(propertyName, out var instanceIdObj) ?? false)
				return instanceIdObj;

			return null;
		}
		catch
		{
			return null;
		}
	}
}
