using AppInformation.Helpers;
using AppInformation.Renderers;


namespace AppInformation;


public static class OutputExtensions
{
	extension(IAppInfo appInfo)
	{
		public IAppInfoOutputBuilder WithDefaultOutput() =>
			AppInfo.CreateDefaultOutputBuilder(appInfo);

		public IAppInfoOutputBuilder WithOutput(
			Action<IAppInfoOutputBuilder> configure)
		{
			var builder = AppInfo.CreateEmptyOutputBuilder(appInfo);
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
			builder.AddRenderer(
				new TextFileRenderer(
//TODO: Make path and filename configurable
					new TempFileNameProvider(),
					new FileWriter()));

		public IAppInfoOutputBuilder ToJsonFile() =>
			builder.AddRenderer(
				new JsonFileRenderer(
//TODO: Make path and filename configurable
					new TempFileNameProvider(),
					new FileWriter()));
	}
}
