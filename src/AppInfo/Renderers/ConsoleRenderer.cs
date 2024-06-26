namespace AppInfo.Renderers;


public class ConsoleRenderer : Renderer
{
	protected override void RenderAppInfo(IAppInfo info)
	{
//TODO: Detect VT100 support and format for console
		var formatted = string.Join(" ␍␊ ", info.Fragments.Select(f => string.Join('/', f.Value)));
		Console.WriteLine(formatted);
	}
}
