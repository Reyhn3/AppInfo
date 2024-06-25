using System.Diagnostics;
using AppInfo.Fragments;


namespace AppInfo.Extractors;


public class IdentityExtractor : IExtractor
{
	internal const string ApplicationIdLabel = "ApplicationId";
	internal const string InstanceIdLabel = "InstanceId";
	internal const string ScopeIdLabel = "ScopeId";

	private readonly string _appId;
	private readonly string? _instanceId;
	private readonly Func<object?>? _scopeIdFactory;

	public IdentityExtractor(
		string appId,
		string? instanceId = null,
		Func<object?>? scopeIdFactory = null)
	{
		if (string.IsNullOrWhiteSpace(appId))
			throw new ArgumentNullException(nameof(appId));

		_appId = appId.Trim();
		_scopeIdFactory = scopeIdFactory;

		var trimmedInstanceId = instanceId?.Trim();
		if (!string.IsNullOrWhiteSpace(trimmedInstanceId))
			_instanceId = trimmedInstanceId;
	}

	public IEnumerable<Fragment> Extract()
	{
		yield return new Fragment(ApplicationIdLabel, _appId);
		yield return new Fragment(InstanceIdLabel, _instanceId ?? Constants.NA);
		yield return new Fragment(ScopeIdLabel, GetScopeId());
	}

	internal object GetScopeId() =>
		TryRunScopeIdFactory(_scopeIdFactory) ?? new Random().NextInt64();

	private static object? TryRunScopeIdFactory(Func<object?>? factory)
	{
		try
		{
			return (factory ?? (() => null))();
		}
		catch (Exception ex)
		{
			Debug.WriteLine($"Exception caught when running factory for {ScopeIdLabel}: {ex}");
			return null;
		}
	}
}
