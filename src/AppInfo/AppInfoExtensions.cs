using AppInfo.Extractors;


namespace AppInfo;


public static class AppInfoExtensions
{
	public static object? ApplicationId(this IAppInfo? appInfo, object? fallback = null) =>
		appInfo.GetFragmentValueOrFallback(IdentityExtractor.ApplicationIdLabel, fallback);

	public static object? InstanceId(this IAppInfo? appInfo, object? fallback = null) =>
		appInfo.GetFragmentValueOrFallback(IdentityExtractor.InstanceIdLabel, fallback);

	public static object? ScopeId(this IAppInfo? appInfo, object? fallback = null) =>
		appInfo.GetFragmentValueOrFallback(IdentityExtractor.ScopeIdLabel, fallback);

//TODO: Consider this strategy... It will return the fallback if the fragment doesn't exist or if the value is null. Is this intended?
	private static object? GetFragmentValueOrFallback(this IAppInfo? appInfo, string label, object? fallback) =>
		appInfo?.Fragments.FirstOrDefault(f => string.Equals(f.Label, label, StringComparison.OrdinalIgnoreCase))?
			.Value?.FirstOrDefault()
		?? fallback;
}
