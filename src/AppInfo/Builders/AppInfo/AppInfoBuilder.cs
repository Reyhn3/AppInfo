using System.Reflection;
using AppInfo.Fragments;


namespace AppInfo;


public class AppInfoBuilder : IAppInfoBuilder
{
	private readonly List<Fragment> _fragments = [];

	public IEnumerable<Fragment> Fragments => _fragments.AsEnumerable();
	public AppOutput Output { get; private set; }

	public static IAppInfoBuilder CreateDefaultBuilder() =>
		new AppInfoBuilder
			{
				Output = AppInfoOutputBuilder.Default
			};

	public IAppInfo Build()
	{
//TODO: Move fragment compilation to formatter class
		var compiled = string.Join(" ␍␊ ", _fragments.Select(f => f.Value));
		var appInfo = new AppInfo
			{
				Formatted = compiled,
			};

		Output.Execute(appInfo);

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
		Output = builder.Build();
		return this;
	}

	private AppInfoBuilder AddFragment(Fragment fragment)
	{
		_fragments.Add(fragment);
		return this;
	}
}
