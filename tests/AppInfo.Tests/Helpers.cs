namespace AppInfo.Tests;


internal static class Helpers
{
	public static void PrintFragments(IEnumerable<Fragment> fragments)
	{
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
	}

	public static void PrintValues(IEnumerable<object?>? values)
	{
		if (values == null)
		{
			Console.WriteLine("<null>");
			return;
		}

		foreach (var value in values)
			Console.WriteLine(ValueFormatter(value));
	}

	private static string ValueFormatter(object? value) =>
		value == null
			? "<null>"
			: $"{value} ({value.GetType().Name})";
}
