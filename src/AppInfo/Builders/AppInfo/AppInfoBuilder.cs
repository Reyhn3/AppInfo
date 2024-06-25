using System.Globalization;
using System.Reflection;
using AppInfo.Extractors;


namespace AppInfo;


public class AppInfoBuilder : IAppInfoBuilder
{
	internal readonly List<IExtractor> _extractors = [];

	public CultureInfo Culture { get; private set; }
	public AppOutput Output { get; private set; }

	public static IAppInfoBuilder CreateDefaultBuilder() =>
		new AppInfoBuilder
			{
				Output = AppInfoOutputBuilder.Default
			};

	public IAppInfo Build()
	{
		var fragments = _extractors.SelectMany(e => e.Extract());
//TODO: Move fragment compilation to formatter class
//TODO: Inject culture when formatting
//TODO: Trim label and value
		var formatted = string.Join(" ␍␊ ", fragments.Select(f => string.Join('/', f.Value)));
		var appInfo = new AppInfo
			{
				Formatted = formatted
			};

		Output.Execute(appInfo);

		return appInfo;
	}

	public IAppInfoBuilder WithIdentities(string appId, string? instanceId = null, string? scopeId = null) =>
		AddExtractors(new IdentityExtractor(appId, instanceId, scopeId));

	public IAppInfoBuilder AddTimestamp() =>
		AddExtractors(new TimestampExtractor());

	public IAppInfoBuilder AddExtras(params (string Label, object? Value)[] extras) =>
		AddExtractors(new ExtrasExtractor(extras));

	public IAppInfoBuilder AddAssembly(Assembly assembly, string? shortName = null, bool stripSourceRevision = false) =>
		AddExtractors(new AssemblyExtractor(assembly, shortName, stripSourceRevision));

	public IAppInfoBuilder UseCulture(CultureInfo cultureInfo)
	{
		Culture = cultureInfo;
		return this;
	}

	public IAppInfoBuilder WithOutput(Action<IAppInfoOutputBuilder> configure)
	{
		var builder = new AppInfoOutputBuilder();
		configure(builder);
		Output = builder.Build();
		return this;
	}

	private AppInfoBuilder AddExtractors(params IExtractor[] extractors)
	{
		_extractors.AddRange(extractors);
		return this;
	}
}
