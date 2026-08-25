using AppInfo.Renderers;


namespace AppInfo;


public static class OutputExtensions
{
	extension(IAppInfo appInfo)
	{
		public IAppInfoOutputBuilder WithDefaultOutput() =>
			Default.DefaultOutputBuilder(appInfo);

		public IAppInfoOutputBuilder WithOutput(
			Action<IAppInfoOutputBuilder> configure)
		{
			var builder = Default.DefaultOutputBuilder(appInfo);
			configure(builder);
			return builder;
		}
	}


	extension(IAppInfoOutputBuilder builder)
	{
		public IAppInfoOutputBuilder ToConsole() =>
			builder.AddRenderer(new ConsoleRenderer());

		public IAppInfoOutputBuilder ToTrace() =>
			builder.AddRenderer(new TraceRenderer());

		public IAppInfoOutputBuilder ToLog(Action<string, object?[]> logger) =>
			builder.AddRenderer(new LogRenderer(logger));

		public IAppInfoOutputBuilder ToTextFile() =>
			builder.AddRenderer(new TextFileRenderer());

		public IAppInfoOutputBuilder ToJsonFile() =>
			builder.AddRenderer(new JsonFileRenderer());
	}
}
