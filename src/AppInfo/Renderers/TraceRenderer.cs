using System.Diagnostics;


namespace AppInfo.Renderers;


public class TraceRenderer : Renderer
{
	protected override void RenderAppInfo(IAppInfo info)
	{
//TODO: Format for plain output
		var formatted = string.Join(" ␍␊ ", info.Fragments.Select(f => string.Join('/', f.Value)));
		Trace.WriteLine(formatted, Constants.LibraryName);
	}
}
