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
				Extractors = [new StandardExtractor(Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly())],
				Culture = CultureInfo.CurrentUICulture,
				Output = AppInfoOutputBuilder.Default
			};

	public IAppInfo Build()
	{
		var fragments = Extractors.SelectMany(e => e.Extract()).ToArray();
//TODO: #11: Move fragment compilation to formatter class
//TODO: #11: Inject culture when formatting
//TODO: #11: Trim label and value
		var appInfo = new AppInfo(Culture, fragments);

		Output.Execute(appInfo);

		return appInfo;
	}

	private AppInfoBuilder AddExtractors(params IExtractor[] extractors)
	{
		Extractors.AddRange(extractors);
		return this;
	}

#region IAppInfoCulture
	public IAppInfoBuilder UseCulture(CultureInfo cultureInfo)
	{
		Culture = cultureInfo;
		return this;
	}
#endregion IAppInfoCulture

#region IAppInfoIdentity
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
#endregion IAppInfoIdentity

#region IAppInfoTimestamp
	public IAppInfoBuilder AddTimestamp() =>
		AddExtractors(new TimestampExtractor());
#endregion IAppInfoTimestamp

#region IAppInfoExtras
	public IAppInfoBuilder AddExtras(string label, object? value) =>
		AddExtractors(new ExtrasExtractor((label, value)));

	public IAppInfoBuilder AddExtras(params (string Label, object? Value)[] extras) =>
		AddExtractors(new ExtrasExtractor(extras));

	public IAppInfoBuilder AddExtras(string label, Func<object?> valueFactory) =>
		AddExtractors(new ExtrasExtractor((label, valueFactory)));

	public IAppInfoBuilder AddExtras(params (string Label, Func<object?> ValueFactory)[] extras) =>
		AddExtractors(new ExtrasExtractor(extras));
#endregion IAppInfoExtras

#region IAppInfoAssembly
	public IAppInfoBuilder AddAssembly(
		Assembly assembly,
		string? shortName = null,
		bool stripSourceRevision = false) =>
		AddExtractors(new AssemblyExtractor(assembly, shortName, stripSourceRevision));
#endregion IAppInfoAssembly

#region IAddOutput
	public IAppInfoBuilder WithOutput(Action<IAppInfoOutputBuilder> configure)
	{
		var builder = new AppInfoOutputBuilder();
		configure(builder);
		Output = builder.Build();
		return this;
	}
#endregion IAddOutput
}
