using System.Reflection;


namespace AppInformation.Tests.TestHelpers;


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
}
