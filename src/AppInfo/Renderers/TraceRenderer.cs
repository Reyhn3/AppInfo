using System.CodeDom.Compiler;
using System.Diagnostics;


namespace AppInfo.Renderers;


public class TraceRenderer : UnstructuredTextRenderer
{
	protected override void RenderAppInfo(IAppInfo info)
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

		// Note to self:
		// Traces will not appear anywhere when running the IDE in Debug mode.
		// To see the traces, run directly from command line.
		var final = output.ToString();
		Trace.WriteLine(final, Constants.LibraryName);
	}

	private static string ConcatenateTitle(IAppInfo info)
	{
		var (lead, name, tail) = GenerateTitleParts(info);
		return lead + name + tail;
	}

	private static string RenderValue(IEnumerable<object?> value) =>
		string.Join(", ", value.Select(FormatValue));
}
