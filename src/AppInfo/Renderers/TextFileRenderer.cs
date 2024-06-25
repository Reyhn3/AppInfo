namespace AppInfo.Renderers;


public class TextFileRenderer : IRenderer
{
	public void Render(IAppInfo info)
	{
//TODO: Format for plain text file
		var formatted = string.Join(" ␍␊ ", info.Fragments.Select(f => string.Join('/', f.Value)));
//TODO: Write to file
		Console.WriteLine(formatted);
	}
}
