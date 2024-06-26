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


public abstract class UnstructuredTextRenderer : Renderer
{
	private const char Separator = ':';
	private const int MaxLabelWidth = 15;
	private const string NullValue = "<null>";
	private const string EmptyValue = "<empty>";
	protected const string Indentation = "  ";

	protected static int CalculateLabelMaxWidth(IAppInfo info) =>
		Math.Min(
			MaxLabelWidth,
			info.Fragments
				.Select(f => f.Label)
				.Where(s => !string.IsNullOrWhiteSpace(s))
				.Max(s => s.Length));

	protected static string PadLabel(string label, int width) =>
		(label + Separator + ' ').PadRight(width + 2);

	protected static string FormatValue(object? value) =>
		value switch
			{
				null                                       => NullValue,
				bool b                                     => b.ToString().ToLower(),
				string s when string.IsNullOrWhiteSpace(s) => EmptyValue,
				_                                          => value.ToString()!
			};
}
