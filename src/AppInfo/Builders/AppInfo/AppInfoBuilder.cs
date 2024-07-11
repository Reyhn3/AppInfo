using System.Globalization;
using System.Reflection;
using AppInfo.Extractors;


namespace AppInfo;


public class AppInfoBuilder : IAppInfoBuilder
{
	public CultureInfo Culture { get; private set; }
	public IAppOutput Output { get; private set; }
	internal List<IExtractor> Extractors { get; private init; }

	public static IAppInfoBuilder CreateDefaultBuilder() =>
		new AppInfoBuilder
			{
//TODO: Make sure this is the right assembly
				Extractors = [new StandardExtractor(Assembly.GetEntryAssembly())],
				Output = AppInfoOutputBuilder.Default
			};

	public IAppInfo Build()
	{
		var fragments = Extractors.SelectMany(e => e.Extract());
//TODO: #11: Move fragment compilation to formatter class
//TODO: #11: Inject culture when formatting
//TODO: #11: Trim label and value
		var appInfo = new AppInfo(fragments);

		Output.Execute(appInfo);

		return appInfo;
	}

	public IAppInfoBuilder WithIdentities(
		string appId,
		string? instanceId = null,
		Func<object?>? scopeIdFactory = null,
		params string[] args) =>
		AddExtractors(new IdentityExtractor(
			appId,
			instanceId,
			() => AppSettingsReader.ReadTopLevelKeyFromAppSettings(IdentityExtractor.InstanceIdLabel),
			scopeIdFactory,
			args));

	public IAppInfoBuilder AddTimestamp() =>
		AddExtractors(new TimestampExtractor());

	public IAppInfoBuilder AddExtras(params (string Label, object? Value)[] extras) =>
		AddExtractors(new ExtrasExtractor(extras));

	public IAppInfoBuilder AddAssembly(
		Assembly assembly,
		string? shortName = null,
		bool stripSourceRevision = false) =>
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
		Extractors.AddRange(extractors);
		return this;
	}
}
