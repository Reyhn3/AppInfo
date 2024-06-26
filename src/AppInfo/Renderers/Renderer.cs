using System.Diagnostics;


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
			Trace.WriteLine($"Exception when rendering with {GetType().Name}:{Environment.NewLine}{ex}", Constants.LibraryName);
		}
	}

	protected abstract void RenderAppInfo(IAppInfo info);
}
