using AppInformation.Helpers;
using AppInformation.Renderers;


namespace AppInformation;


public static class OutputExtensions
{
	extension(IAppInfo appInfo)
	{
		public IOutputBuilder WithDefaultOutput() =>
			AppInfo.CreateDefaultOutputBuilder(appInfo);

		public IOutputBuilder WithOutput(
			Action<IOutputBuilder> configure)
		{
			var builder = AppInfo.CreateEmptyOutputBuilder(appInfo);
			configure(builder);
			return builder;
		}
	}


	extension(IOutputBuilder builder)
	{
		public IOutputBuilder ToConsole() =>
			builder.AddRenderer(new ConsoleRenderer());

		public IOutputBuilder ToTrace() =>
			builder.AddRenderer(new TraceRenderer());

		public IOutputBuilder ToLog(Action<string, object?[]> logger) =>
			builder.AddRenderer(new LogRenderer(logger));

		public IOutputBuilder ToTextFile() =>
			builder.AddRenderer(
				new TextFileRenderer(
//TODO: Make path and filename configurable
					new TempFileNameProvider(),
					new FileWriter()));

		public IOutputBuilder ToJsonFile() =>
			builder.AddRenderer(
				new JsonFileRenderer(
//TODO: Make path and filename configurable
					new TempFileNameProvider(),
					new FileWriter()));
	}
}
