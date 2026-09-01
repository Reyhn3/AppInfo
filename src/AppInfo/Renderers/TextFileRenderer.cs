using System.CodeDom.Compiler;
using AppInformation.Helpers;


namespace AppInformation.Renderers;


public class TextFileRenderer(IFileNameProvider fileNameProvider, IFileWriter fileWriter)
	: UnstructuredTextRenderer
{
	protected override void RenderAppInfo(IAppInfo info)
	{
		var output = BuildPlainString(info);

//TODO: #30: Generate unique file name, or append to existing file
		var path = fileNameProvider.GetPathAndFileName("txt");

//TODO: #30: Let the user choose encoding
		var file = fileWriter.WriteToFile(path, output);
		InternalLogger.Log("Plain-text file written to {0}", file);
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

//TODO: Check if IsScalar (move to base and test)
	private string RenderValue(IEnumerable<object?>? value)
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
