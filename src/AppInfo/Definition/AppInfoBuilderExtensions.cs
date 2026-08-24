using System.Reflection;
using AppInfo.Extractors;


namespace AppInfo.Definition;


public static class AppInfoBuilderExtensions
{
	public static IAppInfoBuilder AddStandard(this IAppInfoBuilder builder)
	{
		builder.AddExtractor(
			new StandardExtractor(Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly()));
		return builder;
	}

	public static IAppInfoBuilder WithIdentities(
		this IAppInfoBuilder builder,
		string appId,
		string? instanceId = null,
		Func<object?>? scopeIdFactory = null,
		params string[] args)
	{
		builder.AddExtractor(
			new IdentityExtractor(
				appId,
				instanceId,
				() => AppSettingsReader.ReadTopLevelKeyFromAppSettings(IdentityExtractor.InstanceIdLabel),
				scopeIdFactory,
				args));
		return builder;
	}

	public static IAppInfoBuilder AddTimestamp(this IAppInfoBuilder builder)
	{
		builder.AddExtractor(
			new TimestampExtractor());
		return builder;
	}

	public static IAppInfoBuilder AddExtras(
		this IAppInfoBuilder builder,
		string label,
		object? value)
	{
		builder.AddExtractor(
			new ExtrasExtractor((label, value)));
		return builder;
	}

	public static IAppInfoBuilder AddExtras(
		this IAppInfoBuilder builder,
		params (string Label, object? Value)[] extras)
	{
		builder.AddExtractor(
			new ExtrasExtractor(extras));
		return builder;
	}

	public static IAppInfoBuilder AddExtras(
		this IAppInfoBuilder builder,
		string label,
		Func<object?> valueFactory)
	{
		builder.AddExtractor(
			new ExtrasExtractor((label, valueFactory)));
		return builder;
	}

	public static IAppInfoBuilder AddExtras(
		this IAppInfoBuilder builder,
		params (string Label, Func<object?> ValueFactory)[] extras)
	{
		builder.AddExtractor(
			new ExtrasExtractor(extras));
		return builder;
	}

	public static IAppInfoBuilder AddAssembly(
		this IAppInfoBuilder builder,
		Assembly assembly,
		string? shortName = null,
		bool stripSourceRevision = false)
	{
		builder.AddExtractor(
			new AssemblyExtractor(assembly, shortName, stripSourceRevision));
		return builder;
	}
}
