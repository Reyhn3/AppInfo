using AppInfo.Definition;
using AppInfo.Renderers;


namespace AppInfo.Output;


public static class AppInfoOutputBuilderExtensions
{
	public static IAppInfoOutputBuilder ToConsole(this IAppInfoOutputBuilder builder)
	{
		builder.AddRenderer(new ConsoleRenderer());
		return builder;
	}

	public static IAppInfoOutputBuilder ToTrace(this IAppInfoOutputBuilder builder)
	{
		builder.AddRenderer(new TraceRenderer());
		return builder;
	}

	public static IAppInfoOutputBuilder ToLog(this IAppInfoOutputBuilder builder, Action<string, object?[]> logger)
	{
		builder.AddRenderer(new LogRenderer(logger));
		return builder;
	}

	public static IAppInfoOutputBuilder ToTextFile(this IAppInfoOutputBuilder builder)
	{
		builder.AddRenderer(new TextFileRenderer());
		return builder;
	}

	public static IAppInfoOutputBuilder ToJsonFile(this IAppInfoOutputBuilder builder)
	{
		builder.AddRenderer(new JsonFileRenderer());
		return builder;
	}
}
