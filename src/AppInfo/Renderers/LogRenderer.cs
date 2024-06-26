namespace AppInfo.Renderers;


public class LogRenderer(Action<string> logger) : Renderer
{
	private readonly Action<string> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

	protected override void RenderAppInfo(IAppInfo info)
	{
//TODO: Format for structured logging
		var formatted = string.Join(" ␍␊ ", info.Fragments.Select(f => string.Join('/', f.Value)));
		_logger(formatted);
	}
}
