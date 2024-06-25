using System.Diagnostics;


namespace AppInfo.Renderers;


public class TraceRenderer : IRenderer
{
	public void Render(IAppInfo info)
	{
//TODO: Format for plain output
		var formatted = string.Join(" ␍␊ ", info.Fragments.Select(f => string.Join('/', f.Value)));
//TODO: Add category
		Trace.WriteLine(formatted);
	}
}
