using AppInfo.Fragments;


namespace AppInfo.Tests;


internal static class Helpers
{
	public static void PrintFragments(IEnumerable<Fragment> fragments)
	{
		foreach (var fragment in fragments)
			Console.WriteLine("Label: '{0}', Value: {1}", fragment.Label, ValueFormatter(fragment.Value));

		return;

		static string ValueFormatter(object? value)
		{
			return value == null ? "<null>" : $"{value} ({value.GetType().Name})";
		}
	}
}
