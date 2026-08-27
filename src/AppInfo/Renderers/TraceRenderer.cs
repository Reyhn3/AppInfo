using System.CodeDom.Compiler;
using System.Diagnostics;


namespace AppInformation.Renderers;


public class TraceRenderer : UnstructuredTextRenderer
{
	protected override void RenderAppInfo(IAppInfo info)
	{
		var output = BuildPlainString(info);
		Trace.WriteLine(output);
	}

	private string BuildPlainString(IAppInfo info)
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

	private string RenderValue(IEnumerable<object?>? value)
	{
		if (value == null)
		{
			return FormatValue(null);
		}

		var array = value.ToArray();
		return string.Join(", ", array.Select(FormatValue));
	}
}
