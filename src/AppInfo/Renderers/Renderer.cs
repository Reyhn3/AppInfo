using System.Globalization;
using AppInformation.Extractors;
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

	protected static Title GenerateTitleParts(IAppInfo info)
	{
		var productName = info.Fragments.FirstOrDefault(f => string.Equals(f.Label, StandardExtractor.LabelForProductName));
		if (productName is null || productName.Value is null || !productName.Value.Any())
			return new Title(
				"Application ",
				string.Empty,
				" created with context:");

		return new Title(
			"Application ",
			productName.Value.FirstOrDefault()?.ToString() ?? "\b",
			" created with context:");
	}

	protected string FormatWithCulture(object value) =>
		string.Format(_culture, "{0}", value!);


	protected internal record struct Title(string Lead, string Name, string Tail);
}
