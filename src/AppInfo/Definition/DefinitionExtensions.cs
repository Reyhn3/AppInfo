using System.Reflection;
using AppInformation.Extractors;
using AppInformation.Helpers;


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

		public IAppInfo BuildAndWriteTo(Action<IOutputBuilder> configure)
		{
			var appInfo = builder.Build();

			var outputBuilder = AppInfo.CreateEmptyOutputBuilder(appInfo);
			configure(outputBuilder);
			outputBuilder.Write();

			return appInfo;
		}

		private IAppInfoBuilder SafelyAddExtractor(Func<IExtractor> factory)
		{
			try
			{
				var extractor = factory();
				return builder.AddExtractor(extractor);
			}
			catch (Exception ex)
			{
				InternalLogger.Log("Exception caught when trying to add extractor: {0}", ex);
				return builder;
			}
		}

		public IAppInfoBuilder AddStandard() =>
			builder.SafelyAddExtractor(() =>
				new StandardExtractor(Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly()));

		public IAppInfoBuilder WithIdentities(
			string appId,
			string? instanceId = null,
			Func<object?>? scopeIdFactory = null,
			params string[] args) =>
			builder.SafelyAddExtractor(() =>
				new IdentityExtractor(
					appId,
					instanceId,
					() => AppSettingsReader.ReadTopLevelKeyFromAppSettings(IdentityExtractor.InstanceIdLabel),
					scopeIdFactory,
					args));

		public IAppInfoBuilder AddTimestamp() =>
			builder.SafelyAddExtractor(() =>
				new TimestampExtractor());

		public IAppInfoBuilder AddExtra(
			string label,
			object? value) =>
			builder.SafelyAddExtractor(() =>
				new ExtrasExtractor((label, value)));

		public IAppInfoBuilder AddExtra(
			string label,
			Func<object?> valueFactory) =>
			builder.SafelyAddExtractor(() =>
				new ExtrasExtractor((label, valueFactory)));

		public IAppInfoBuilder AddExtra(
			params (string Label, object? Value)[] extras) =>
			builder.SafelyAddExtractor(() =>
				new ExtrasExtractor(extras));

		public IAppInfoBuilder AddExtra(
			params (string Label, Func<object?> ValueFactory)[] extras) =>
			builder.SafelyAddExtractor(() =>
				new ExtrasExtractor(extras));

		public IAppInfoBuilder AddAssembly(
			Assembly assembly,
			string? shortName = null,
			bool stripSourceRevision = false) =>
			builder.SafelyAddExtractor(() =>
				new AssemblyExtractor(assembly, shortName, stripSourceRevision));
	}
}
