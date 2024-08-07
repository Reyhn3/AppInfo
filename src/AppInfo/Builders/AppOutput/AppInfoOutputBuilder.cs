using AppInfo.Renderers;


namespace AppInfo;


public class AppInfoOutputBuilder : IAppInfoOutputBuilder
{
	private readonly List<IRenderer> _renderers = [];

	public static IAppOutput Default { get; } = new AppInfoOutputBuilder().ToConsole().Build();

	public IAppInfoOutputBuilder ToConsole() => AddRenderer(new ConsoleRenderer());
	public IAppInfoOutputBuilder ToTrace() => AddRenderer(new TraceRenderer());
	public IAppInfoOutputBuilder ToLog(Action<string, object?[]> logger) => AddRenderer(new LogRenderer(logger));
	public IAppInfoOutputBuilder ToTextFile() => AddRenderer(new TextFileRenderer());
	public IAppInfoOutputBuilder ToJsonFile() => AddRenderer(new JsonFileRenderer());

	private AppInfoOutputBuilder AddRenderer(IRenderer renderer)
	{
		_renderers.Add(renderer);
		return this;
	}

	public IAppOutput Build() =>
		new AppOutput(_renderers);
}
