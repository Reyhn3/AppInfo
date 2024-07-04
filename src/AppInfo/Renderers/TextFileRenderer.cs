namespace AppInfo.Renderers;


public class TextFileRenderer : Renderer
{
	protected override void RenderAppInfo(IAppInfo info)
	{
//TODO: Format for plain text file
//TODO: Handle null-value
		var formatted = string.Join(" ␍␊ ", info.Fragments.Select(f => string.Join('/', f.Value)));
//TODO: Write to file
		Console.WriteLine(formatted);
	}
}
