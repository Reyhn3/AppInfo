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

	protected static Title GenerateTitleParts(IAppInfo info) => new(
		"Application ",
//TODO: Replace this part with the AssemblyProductAttribute.Product or assembly.GetName().Name
		"DUMMY",
		" created with context:");


	protected record struct Title(string Lead, string Name, string Tail);
}
