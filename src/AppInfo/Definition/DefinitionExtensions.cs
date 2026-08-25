using System.Reflection;
using AppInfo.Extractors;


namespace AppInfo;


public static class DefinitionExtensions
{
	extension(IAppInfoBuilder builder)
	{
		public IAppInfoBuilder AddStandard() =>
			builder.AddExtractor(
				new StandardExtractor(Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly()));

		public IAppInfoBuilder WithIdentities(
			string appId,
			string? instanceId = null,
			Func<object?>? scopeIdFactory = null,
			params string[] args) =>
			builder.AddExtractor(
				new IdentityExtractor(
					appId,
					instanceId,
					() => AppSettingsReader.ReadTopLevelKeyFromAppSettings(IdentityExtractor.InstanceIdLabel),
					scopeIdFactory,
					args));

		public IAppInfoBuilder AddTimestamp() =>
			builder.AddExtractor(
				new TimestampExtractor());

		public IAppInfoBuilder AddExtras(
			string label,
			object? value) =>
			builder.AddExtractor(
				new ExtrasExtractor((label, value)));

		public IAppInfoBuilder AddExtras(
			params (string Label, object? Value)[] extras) =>
			builder.AddExtractor(
				new ExtrasExtractor(extras));

		public IAppInfoBuilder AddExtras(
			string label,
			Func<object?> valueFactory) =>
			builder.AddExtractor(
				new ExtrasExtractor((label, valueFactory)));

		public IAppInfoBuilder AddExtras(
			params (string Label, Func<object?> ValueFactory)[] extras) =>
			builder.AddExtractor(
				new ExtrasExtractor(extras));

		public IAppInfoBuilder AddAssembly(
			Assembly assembly,
			string? shortName = null,
			bool stripSourceRevision = false) =>
			builder.AddExtractor(
				new AssemblyExtractor(assembly, shortName, stripSourceRevision));
	}
}
