using System.CodeDom.Compiler;


namespace AppInfo.Renderers;


public class TraceRenderer : UnstructuredTextRenderer
{
	protected override void RenderAppInfo(IAppInfo info)
	{
		var output = BuildPlainString(info);
		InternalLogger.Log(output);
	}

	private static string BuildPlainString(IAppInfo info)
	{
		var output = new StringWriter();
		var writer = new IndentedTextWriter(output, Indentation);
		writer.WriteLine(ConcatenateTitle(info));
		writer.Indent++;

		var width = CalculateLabelMaxWidth(info);
		foreach (var (label, value) in info.Fragments)
		{
			var line = $"{PadLabel(label, width)}{RenderValue(value)}";
			writer.WriteLine(line);
		}

		return output.ToString();
	}

	private static string ConcatenateTitle(IAppInfo info)
	{
		var (lead, name, tail) = GenerateTitleParts(info);
		return lead + name + tail;
	}

	private static string RenderValue(IEnumerable<object?>? value) =>
		string.Join(", ", value == null ? FormatValue(null) : value.Select(FormatValue));
}
