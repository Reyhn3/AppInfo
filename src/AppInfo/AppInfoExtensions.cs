using AppInfo.Extractors;


namespace AppInfo;


public static class AppInfoExtensions
{
	extension(IAppInfo? appInfo)
	{
		public object? ApplicationId(object? fallback = null) =>
			appInfo.GetFragmentValueOrFallback(IdentityExtractor.ApplicationIdLabel, fallback);

		public object? InstanceId(object? fallback = null) =>
			appInfo.GetFragmentValueOrFallback(IdentityExtractor.InstanceIdLabel, fallback);

		public object? ScopeId(object? fallback = null) =>
			appInfo.GetFragmentValueOrFallback(IdentityExtractor.ScopeIdLabel, fallback);

//TODO: Consider this strategy... It will return the fallback if the fragment doesn't exist or if the value is null. Is this intended?
		private object? GetFragmentValueOrFallback(string label, object? fallback) =>
			appInfo?.Fragments
				.FirstOrDefault(f => string.Equals(f.Label, label, StringComparison.OrdinalIgnoreCase))
				?.Value?.FirstOrDefault()
			?? fallback;
	}
}
