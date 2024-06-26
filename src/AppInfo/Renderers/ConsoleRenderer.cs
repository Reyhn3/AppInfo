using static System.Console;


namespace AppInfo.Renderers;


public class ConsoleRenderer : UnstructuredTextRenderer
{
	protected override void RenderAppInfo(IAppInfo info)
	{
//TODO: #14: Detect VT100 support and format for console

		WriteTitle(info);

		var width = CalculateLabelMaxWidth(info);
//TODO: Wrap in try-catch that restores console colors
//TODO: Add ordering of standard fragments
		foreach (var (label, value) in info.Fragments)
		{
			Write("{0}{1}", Indentation, PadLabel(label, width));
			WriteValue(value);
		}
	}

	private static void WriteTitle(IAppInfo info)
	{
		var originalForeground = ForegroundColor;
		var originalBackground = BackgroundColor;

		var (lead, name, tail) = GenerateTitleParts(info);
		Write(lead);
		ForegroundColor = ConsoleColor.Black;
		BackgroundColor = ConsoleColor.White;
		Write(name);
		ForegroundColor = originalForeground;
		BackgroundColor = originalBackground;
		WriteLine(tail);
	}

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
