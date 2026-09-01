using System.Globalization;
using System.Reflection;


namespace AppInformation.Tests.TestHelpers;


internal static class Helpers
{
	public static void PrintCapturedOutput(string? output)
	{
		const string subject = "Captured output";
		PrintTestPreamble(subject);
		Console.WriteLine(output);
		PrintTestPostamble(subject);
	}

	public static void PrintCulture(CultureInfo cultureInfo)
	{
		const string subject = "Culture";
		PrintTestPreamble(subject);
		Console.WriteLine(cultureInfo);
		PrintTestPostamble(subject);
	}

	public static void PrintFragments(IEnumerable<Fragment> fragments)
	{
		const string subject = "Fragments";
		PrintTestPreamble(subject);

		foreach (var fragment in fragments)
		{
			Console.WriteLine("Label: '{0}', Value: {1}",
				fragment.Label,
				fragment.Value == null
					? "<null>"
					: !fragment.Value.Any()
						? "<empty>"
						: string.Join(" :: ", fragment.Value.Select(ValueFormatter)));
		}

		PrintTestPostamble(subject);
	}

	public static void PrintValues(IEnumerable<object?>? values)
	{
		const string subject = "Values";
		PrintTestPreamble(subject);

		if (values == null)
		{
			Console.WriteLine("<null>");
			return;
		}

		foreach (var value in values)
			Console.WriteLine(ValueFormatter(value));

		PrintTestPostamble(subject);
	}

	private static string ValueFormatter(object? value) =>
		value == null
			? "<null>"
			: $"{value} ({value.GetType().Name})";

	public static object? GetFieldValue(object obj, string fieldName)
	{
		var type = obj.GetType();
		var field = type.GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);

		if (field == null)
			throw new ArgumentException($"Field '{fieldName}' not found in type '{type.Name}'");

		return field?.GetValue(obj);
	}

	public static T? GetFieldValue<T>(object obj, string fieldName)
	{
		var type = obj.GetType();
		var field = type.GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);
		return (T?)field?.GetValue(obj);
	}

	private static void PrintTestPreamble(string subject) =>
		Console.WriteLine("---TEST: Begin ({0})---", subject);

	private static void PrintTestPostamble(string subject) =>
		Console.WriteLine("---TEST: End ({0})---", subject);
}
