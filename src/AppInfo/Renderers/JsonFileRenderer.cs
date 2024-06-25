namespace AppInfo.Renderers;


public class JsonFileRenderer : IRenderer
{
	public void Render(IAppInfo info)
	{
//TODO: Format for JSON file
		var formatted = string.Join(" ␍␊ ", info.Fragments.Select(f => string.Join('/', f.Value)));
//TODO: Write to file
		Console.WriteLine(formatted);
	}
}
