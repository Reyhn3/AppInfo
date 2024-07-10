using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Text;


namespace AppInfo.Renderers;


public class TextFileRenderer : UnstructuredTextRenderer
{
	protected override void RenderAppInfo(IAppInfo info)
	{
		var output = BuildPlainString(info);
//TODO: Generate unique file name, or append to existing file
		var path = Path.ChangeExtension(Path.GetTempFileName(), "txt");

//TODO: Let the user choose encoding
		File.WriteAllText(path, output, Encoding.UTF8);
		Debug.WriteLine($"Plain-text file written to {path}", Constants.LibraryName);

//TODO: Remove this
		Console.WriteLine("Text file: {0}", path);
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

//TODO: Check if IsScalar (move to base and test)
	private static string RenderValue(IEnumerable<object?>? value)
	{
		if (value == null)
			return FormatValue(null);

		var rendered = value.Aggregate(string.Empty, (agg, curr) =>
			string.IsNullOrWhiteSpace(agg)
				? FormatValue(curr)
				: agg + ", " + FormatValue(curr));

		return rendered;
	}
}
