using System.Diagnostics;
using System.Reflection;


namespace AppInfo.Extractors;


internal class StandardExtractor(Assembly assembly)
	: Extractor
{
	private readonly Assembly _assembly = assembly ?? throw new ArgumentNullException(nameof(assembly));

	internal const string LabelForProductName = "Product Name";

	protected override IEnumerable<Func<Fragment>> ProduceExtractors()
	{
		yield return () => new Fragment(LabelForProductName, GetProductName(_assembly));
	}

	private static string? GetProductName(Assembly assembly)
	{
		try
		{
			var name = assembly.GetName().Name;
			if (!string.IsNullOrWhiteSpace(name))
				return name;

			var attribute = assembly.GetCustomAttribute<AssemblyProductAttribute>();
			return attribute?.Product;
		}
		catch (Exception ex)
		{
//TODO: Extract this logic (and everywhere else) to a debug helper class
			Debug.WriteLine("Failed to read product name from assembly {0}: {1}", assembly, ex);
			return null;
		}
	}
}
