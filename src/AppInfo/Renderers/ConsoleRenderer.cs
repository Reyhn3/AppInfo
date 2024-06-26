using static System.Console;


namespace AppInfo.Renderers;


public class ConsoleRenderer : Renderer
{
	private const string Indentation = "  ";
	private const char Separator = ':';
	private const int MaxLabelWidth = 15;
	private const string NullValue = "<null>";
	private const string EmptyValue = "<empty>";

	protected override void RenderAppInfo(IAppInfo info)
	{
//TODO: #14: Detect VT100 support and format for console
		var width = CalculateLabelMaxWidth(info);
		WriteLine("Application created with context:");

//TODO: Wrap in try-catch that restores console colors
//TODO: Add ordering of standard fragments
		foreach (var (label, value) in info.Fragments)
		{
			Write("{0}{1}", Indentation, PadLabel(label, width));
			WriteValue(value);
		}
	}

	private static int CalculateLabelMaxWidth(IAppInfo info) =>
		Math.Min(
			MaxLabelWidth,
			info.Fragments
				.Select(f => f.Label)
				.Where(s => !string.IsNullOrWhiteSpace(s))
				.Max(s => s.Length));

	private static string PadLabel(string label, int width) =>
		(label + Separator + ' ').PadRight(width + 2);

	private static void WriteValue(IEnumerable<object?> value)
	{
		var originalForeground = ForegroundColor;

		var array = value.ToArray();
		if (array.Length == 1)
		{
			ForegroundColor = Colorize(array[0]);
			WriteLine(FormatValue(array[0]));
			ForegroundColor = originalForeground;
		}
		else
		{
//TODO: Enrich Fragment with instructions on how to render multi-values (e.g. concat, newline, separate etc.)
			for (var i = 0; i < array.Length; i++)
			{
				var element = array[i];
				ForegroundColor = Colorize(element);

				Write(FormatValue(element));

				ForegroundColor = originalForeground;

				if (i + 1 < array.Length)
					Write(", ");
			}

			WriteLine();
		}

		ResetColor();
	}

	private static string FormatValue(object? value) =>
		value switch
			{
				null                                       => NullValue,
				bool b                                     => b.ToString().ToLower(),
				string s when string.IsNullOrWhiteSpace(s) => EmptyValue,
				_                                          => value.ToString()!
			};

	private static ConsoleColor Colorize(object? value) =>
		value switch
			{
				bool v    => v ? ConsoleColor.Green : ConsoleColor.Red,
				string    => ConsoleColor.White,
				ValueType => ConsoleColor.Magenta,
				null      => ConsoleColor.DarkGray,
				_         => ConsoleColor.Gray
			};
}
