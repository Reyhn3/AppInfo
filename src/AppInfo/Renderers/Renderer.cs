namespace AppInfo.Renderers;


public abstract class Renderer : IRenderer
{
	public void Render(IAppInfo info)
	{
		try
		{
			RenderAppInfo(info);
		}
		catch (Exception ex)
		{
			InternalLogger.Log("Exception when rendering with {1}:{0}{3}", Environment.NewLine, GetType().Name, ex);
		}
	}

	protected abstract void RenderAppInfo(IAppInfo info);

	protected static Title GenerateTitleParts(IAppInfo info) => new(
		"Application ",
//TODO: Replace this part with the ProductName-fragment from StandardExtractor
		"DUMMY",
		" created with context:");


	protected record struct Title(string Lead, string Name, string Tail);
}
