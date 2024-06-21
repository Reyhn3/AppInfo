using AppInfo.Builders.AppOutput;
using AppInfo.Fragments;


namespace AppInfo.Builders.AppInfo;


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
		var compiled = string.Join(" + ↲" + Environment.NewLine, _fragments.Select(f => f.Value));
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
