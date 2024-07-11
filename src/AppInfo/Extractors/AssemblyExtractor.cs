using System.Reflection;
using AppInfo.Helpers;


namespace AppInfo.Extractors;


public class AssemblyExtractor(Assembly assembly, string? shortName = null, bool stripSourceRevision = false)
	: Extractor
{
	internal const string AssemblyLabel = "Assembly";

	private readonly Assembly _assembly = assembly ?? throw new ArgumentNullException(nameof(assembly));

	protected override IEnumerable<Func<Fragment>> ProduceExtractors()
	{
		yield return () => new Fragment(AssemblyLabel, CompileValue(_assembly, shortName, stripSourceRevision).ToArray());
	}

	internal static IEnumerable<object?> CompileValue(Assembly assembly, string? shortName, bool stripSourceRevision = false)
	{
//TODO: Put the name as fragment key instead?
		yield return string.IsNullOrWhiteSpace(shortName)
			? assembly.GetName().Name
			: shortName.Trim();

		yield return 'v' + AssemblyHelper.GetVersionString(assembly, stripSourceRevision);
	}
}
