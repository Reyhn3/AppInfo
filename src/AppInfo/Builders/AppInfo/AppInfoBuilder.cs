using System.Reflection;
using AppInfo.Fragments;


namespace AppInfo;


public class AppInfoBuilder : IAppInfoBuilder
{
	private readonly List<Fragment> _fragments = [];
	private AppInfoOutput _output;

	public static IAppInfoBuilder CreateDefaultBuilder()
	{
		AppInfoBuilder builder = new();
		return builder;
	}

	public IAppInfo Build()
	{
//TODO: Move fragment compilation to formatter class
		var compiled = string.Join(" ␍␊ ", _fragments.Select(f => f.Value));
		var appInfo = new global::AppInfo.AppInfo
			{
				Formatted = compiled
			};

		_output.Execute(appInfo);

		return appInfo;
	}

	public IAppInfoBuilder WithIdentities() =>
		AddFragment(new Fragment("My identities"));

	public IAppInfoBuilder AddTimestamp() =>
		AddFragment(new Fragment("My timestamp"));


	public IAppInfoBuilder AddExtras(params (string Label, object? Value)[] extras)
	{
		if (extras.Length == 0)
			return this;

//TODO: Filter away empty extras
//TODO: Trim label and value
		foreach (var (label, value) in extras)
			AddFragment(new Fragment($"{label}: {value}"));

		return this;
	}

//TODO: Get assembly name, version etc.
	public IAppInfoBuilder AddAssembly(Assembly assembly, string? shortName = null, bool stripCommitHash = false) =>
		AddFragment(new Fragment(assembly.GetName().Name));

	public IAppInfoBuilder WithOutput(Action<IAppInfoOutputBuilder> configure)
	{
		var builder = new AppInfoOutputBuilder();
		configure(builder);
		_output = builder.Build();
		return this;
	}

	private AppInfoBuilder AddFragment(Fragment fragment)
	{
		_fragments.Add(fragment);
		return this;
	}
}
