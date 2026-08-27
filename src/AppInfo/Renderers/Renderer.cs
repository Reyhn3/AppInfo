using System.Globalization;
using AppInformation.Helpers;


namespace AppInformation.Renderers;


public abstract class Renderer : IRenderer
{
	private CultureInfo _culture;

	public void Render(IAppInfo info)
	{
		try
		{
			_culture = info.Culture;
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

	protected string FormatWithCulture(object value) =>
		string.Format(_culture, "{0}", value!);


	protected record struct Title(string Lead, string Name, string Tail);
}
