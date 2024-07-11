using System.Diagnostics;


namespace AppInfo.Extractors;


public class IdentityExtractor : Extractor
{
	internal const string ApplicationIdLabel = "ApplicationId";
	internal const string InstanceIdLabel = "InstanceId";
	internal const string ScopeIdLabel = "ScopeId";

	private readonly string _appId;
	private readonly string? _instanceId;
	private readonly Func<object?>? _instanceIdFactory;
	private readonly Func<object?>? _scopeIdFactory;
	private readonly string[] _args;

	public IdentityExtractor(
		string appId,
		string? instanceId = null,
		Func<object?>? instanceIdFactory = null,
		Func<object?>? scopeIdFactory = null,
		params string[] args)
	{
		if (string.IsNullOrWhiteSpace(appId))
			throw new ArgumentNullException(nameof(appId));

		_appId = appId.Trim();
		_instanceIdFactory = instanceIdFactory;
		_scopeIdFactory = scopeIdFactory;
		_args = args;

		var trimmedInstanceId = instanceId?.Trim();
		if (!string.IsNullOrWhiteSpace(trimmedInstanceId))
			_instanceId = trimmedInstanceId;
	}

	protected override IEnumerable<Func<Fragment>> ProduceExtractors()
	{
		yield return () => new Fragment(ApplicationIdLabel, _appId);
		yield return () => new Fragment(InstanceIdLabel, GetInstanceId(_args, _instanceIdFactory, _instanceId));
		yield return () => new Fragment(ScopeIdLabel, GetScopeId(_scopeIdFactory));
	}

	internal static object? GetInstanceId(string[]? fromCli, Func<object?>? factory, string? fromArgument) =>
		GetInstanceIdFromCli(fromCli) ?? GetInstanceIdFromAppSettings(factory) ?? fromArgument;

	private static string? GetInstanceIdFromCli(string[]? args)
	{
		if (args == null || args.Length == 0)
			return null;

		for (var i = 0; i < args.Length; i++)
		{
			// Check if the argument is present
			var switchName = args[i].TrimStart('-');
			if (!string.Equals(InstanceIdLabel, switchName, StringComparison.InvariantCultureIgnoreCase))
				continue;

			// Check if there is a value to the argument
			if (i + 1 > args.Length)
				continue;

			return args[i + 1];
		}

		return null;
	}

	private static object? GetInstanceIdFromAppSettings(Func<object?>? factory) =>
		TryRunFactory(InstanceIdLabel, factory);

	internal static object GetScopeId(Func<object?>? factory) =>
		TryRunFactory(ScopeIdLabel, factory) ?? new Random().NextInt64();

	private static object? TryRunFactory(string name, Func<object?>? factory)
	{
		try
		{
			return (factory ?? (() => null))();
		}
		catch (Exception ex)
		{
			Debug.WriteLine($"Exception caught when running factory for {name}: {ex}");
			return null;
		}
	}
}
