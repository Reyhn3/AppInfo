using AppInfo.Fragments;


namespace AppInfo.Extractors;


public class IdentityExtractor : IExtractor
{
	internal const string ApplicationIdLabel = "ApplicationId";
	internal const string InstanceIdLabel = "InstanceId";
	internal const string ScopeIdLabel = "ScopeId";

	private readonly string _appId;
	private readonly string? _instanceId;
	private readonly string? _scopeId;

	public IdentityExtractor(string appId, string? instanceId = null, string? scopeId = null)
	{
		if (string.IsNullOrWhiteSpace(appId))
			throw new ArgumentNullException(nameof(appId));

		_appId = appId.Trim();

		var trimmedInstanceId = instanceId?.Trim();
		if (!string.IsNullOrWhiteSpace(trimmedInstanceId))
			_instanceId = trimmedInstanceId;

		var trimmedScopeId = scopeId?.Trim();
		if (!string.IsNullOrWhiteSpace(trimmedScopeId))
			_scopeId = trimmedScopeId;
	}

	public IEnumerable<Fragment> Extract()
	{
		yield return new Fragment(ApplicationIdLabel, _appId);
		yield return new Fragment(InstanceIdLabel, _instanceId ?? Constants.NA);
		yield return new Fragment(ScopeIdLabel, _scopeId ?? Constants.NA);
	}
}
