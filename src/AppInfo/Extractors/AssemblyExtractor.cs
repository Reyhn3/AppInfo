using System.Reflection;
using AppInfo.Fragments;


namespace AppInfo.Extractors;


public class AssemblyExtractor(Assembly assembly, string? shortName = null, bool stripCommitHash = false)
	: IExtractor
{
	internal const string AssemblyLabel = "Assembly";

	private readonly Assembly _assembly = assembly ?? throw new ArgumentNullException(nameof(assembly));

	public IEnumerable<Fragment> Extract() =>
		new[]
			{
				new Fragment(AssemblyLabel, CompileValue(_assembly, shortName))
			};

//TODO: Implement and get assembly name, version etc.
	internal static string CompileValue(Assembly assembly, string? shortName)
	{
		return shortName ?? assembly.GetName().Name;
	}
}
