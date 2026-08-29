using System.Reflection;
using AppInformation.Extractors;


namespace AppInformation;


public static class DefinitionExtensions
{
	extension(IAppInfoBuilder builder)
	{
		public IAppInfo BuildAndWriteToDefault()
		{
			var appInfo = builder.Build();

			var outputBuilder = AppInfo.CreateDefaultOutputBuilder(appInfo);
			outputBuilder.Write();

			return appInfo;
		}

		public IAppInfo BuildAndWriteTo(Action<IAppInfoOutputBuilder> configure)
		{
			var appInfo = builder.Build();

			var outputBuilder = AppInfo.CreateEmptyOutputBuilder(appInfo);
			configure(outputBuilder);
			outputBuilder.Write();

			return appInfo;
		}

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

		public IAppInfoBuilder AddExtra(
			string label,
			object? value) =>
			builder.AddExtractor(
				new ExtrasExtractor((label, value)));

		public IAppInfoBuilder AddExtra(
			string label,
			Func<object?> valueFactory) =>
			builder.AddExtractor(
				new ExtrasExtractor((label, valueFactory)));

		public IAppInfoBuilder AddExtra(
			params (string Label, object? Value)[] extras) =>
			builder.AddExtractor(
				new ExtrasExtractor(extras));

		public IAppInfoBuilder AddExtra(
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
