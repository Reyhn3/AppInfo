using System.Diagnostics;
using System.Reflection;


namespace AppInfo.Helpers;


internal static class AssemblyHelper
{
	public static string? GetProductName(Assembly assembly)
	{
		try
		{
			var attribute = assembly.GetCustomAttribute<AssemblyProductAttribute>();
			if (!string.IsNullOrWhiteSpace(attribute?.Product))
				return attribute.Product;

			var name = assembly.GetName().Name;
			return name;
		}
		catch (Exception ex)
		{
//TODO: Extract this logic (and everywhere else) to a debug helper class
			Debug.WriteLine("Failed to read product name from assembly {0}: {1}", assembly, ex);
			return null;
		}
	}

	public static string GetVersionString(Assembly assembly, bool stripSourceRevision)
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

	public static bool? GetReleaseMode(Assembly assembly)
	{
		try
		{
			return !assembly
				.GetCustomAttributes(false)
				.OfType<DebuggableAttribute>()
				.Any(a => a.IsJITTrackingEnabled);
		}
		catch (Exception ex)
		{
			Debug.WriteLine("Failed to read release mode info from assembly {0}: {1}", assembly, ex);
			return null;
		}
	}
}
