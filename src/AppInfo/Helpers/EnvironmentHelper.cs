namespace AppInformation.Helpers;


internal static class EnvironmentHelper
{
	public static string GetEnvironment()
	{
		var dotnet = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");
		if (!string.IsNullOrWhiteSpace(dotnet))
			return dotnet;

		var aspnet = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
		if (!string.IsNullOrWhiteSpace(aspnet))
			return aspnet;

		// Fallback to Production just to be on the safe side
		return "Production";
	}
}
