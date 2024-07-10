using static System.Console;


namespace AppInfo.Renderers;


public class ConsoleRenderer : UnstructuredTextRenderer
{
	protected override void RenderAppInfo(IAppInfo info)
	{
//TODO: #14: Detect VT100 support and format for console
		try
		{
			WriteTitle(info);

			var width = CalculateLabelMaxWidth(info);
			foreach (var (label, value) in info.Fragments)
			{
				Write("{0}{1}", Indentation, PadLabel(label, width));
				WriteValue(value);
			}
		}
		finally
		{
			ResetColor();
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

	private static void WriteValue(IEnumerable<object?>? value)
	{
		var originalForeground = ForegroundColor;

		if (value == null)
		{
			ForegroundColor = Colorize(null);
			WriteLine(FormatValue(null));
			ForegroundColor = originalForeground;
		}
		else
		{
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
		}

		ResetColor();
	}

	private static ConsoleColor Colorize(object? value) =>
		value switch
			{
				null                                       => ConsoleColor.DarkGray,
				bool v                                     => v ? ConsoleColor.Green : ConsoleColor.Red,
				string s when string.IsNullOrWhiteSpace(s) => ConsoleColor.DarkGray,
				string                                     => ConsoleColor.White,
				ValueType                                  => ConsoleColor.Magenta,
				_                                          => ConsoleColor.Gray
			};
}
