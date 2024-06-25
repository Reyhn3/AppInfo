using System.Diagnostics;
using System.Reflection;
using AppInfo.Fragments;


namespace AppInfo.Extractors;


public class AssemblyExtractor(Assembly assembly, string? shortName = null, bool stripSourceRevision = false)
	: IExtractor
{
	internal const string AssemblyLabel = "Assembly";

	private readonly Assembly _assembly = assembly ?? throw new ArgumentNullException(nameof(assembly));

	public IEnumerable<Fragment> Extract()
	{
		yield return new Fragment(AssemblyLabel, CompileValue(_assembly, shortName, stripSourceRevision).ToArray());
	}

	internal static IEnumerable<object?> CompileValue(Assembly assembly, string? shortName, bool stripSourceRevision = false)
	{
		yield return string.IsNullOrWhiteSpace(shortName)
			? assembly.GetName().Name
			: shortName.Trim();

		yield return 'v' + GetVersionString(assembly, stripSourceRevision);
	}

	private static string GetVersionString(Assembly assembly, bool stripSourceRevision)
	{
		try
		{
			if (!string.IsNullOrWhiteSpace(assembly.Location))
			{
				var fvi = FileVersionInfo.GetVersionInfo(assembly.Location);

				if (!string.IsNullOrWhiteSpace(fvi.ProductVersion))
					return stripSourceRevision
						? WithoutSourceRevision(fvi.ProductVersion)!
						: fvi.ProductVersion;

				if (!string.IsNullOrWhiteSpace(fvi.FileVersion))
					return fvi.FileVersion;
			}
			else
			{
				var attribute = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>();
				if (!string.IsNullOrWhiteSpace(attribute?.InformationalVersion))
					return stripSourceRevision
						? WithoutSourceRevision(attribute.InformationalVersion)!
						: attribute.InformationalVersion;
			}

			// If all else fails, try the alternate method.
			// Note that this is a Version-object, which does NOT
			// allow any version suffixes.
			return assembly.GetName()?.Version?.ToString() ?? Constants.NA;


			string? WithoutSourceRevision(string? original)
			{
				if (string.IsNullOrWhiteSpace(original))
					return original;

				var indexOfPlus = original.IndexOf('+');
				return indexOfPlus > 0 ? original[..indexOfPlus] : original;
			}
		}
		catch (Exception ex)
		{
			Debug.WriteLine("Failed to read version info from assembly {0}: {1}", assembly, ex);
			return Constants.NA;
		}
	}
}
