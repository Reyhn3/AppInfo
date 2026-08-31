namespace AppInformation.Renderers;


public abstract class UnstructuredTextRenderer : Renderer
{
	private const char Separator = ':';
	internal const int MaxLabelWidth = 15;
	protected const string Indentation = "  ";

	protected static int CalculateLabelMaxWidth(IAppInfo info) =>
		Math.Min(
			MaxLabelWidth,
			info.Fragments
				.Select(f => f.Label)
				.Where(s => !string.IsNullOrWhiteSpace(s))
				.DefaultIfEmpty(string.Empty)
				.Max(s => s.Trim().Length));

	protected static string PadLabel(string label, int width) =>
		string.IsNullOrWhiteSpace(label)
			? string.Empty
			: ((label.Length > width ? label[..(width - 1)] + "…" : label)
				+ Separator
				+ ' ')
			.PadRight(width + 2);

	protected string FormatValue(object? value) =>
		value switch
			{
				null                                       => "<null>",
				bool b                                     => b.ToString().ToLower(),
				string s when string.IsNullOrWhiteSpace(s) => "<empty>",
				_                                          => FormatWithCulture(value!)
			};
}
