using System.Reflection;
using AppInfo.Helpers;


namespace AppInfo.Extractors;


internal class StandardExtractor(Assembly assembly)
	: Extractor
{
	private readonly Assembly _assembly = assembly ?? throw new ArgumentNullException(nameof(assembly));

	internal const string LabelForProductName = "Product";
	internal const string LabelForProductVersion = "Version";
	internal const string LabelForAssembly = "Assembly";
	internal const string LabelForFileName = "File Name";
	internal const string LabelForLocation = "Location";
	internal const string LabelForRelease = "Is Release";
	internal const string LabelForEnvironment = "Environment";
	internal const string LabelForBase = "Base";
	internal const string LabelForMachineName = "MachineName";
	internal const string LabelForOSVersion = "OSVersion";
	internal const string LabelForClrVersion = "ClrVersion";
	internal const string LabelForArchitecture = "64-bit";
	internal const string LabelForProcessId = "ProcessId";

	protected override IEnumerable<Func<Fragment>> ProduceExtractors()
	{
		yield return () => new Fragment(LabelForProductName, AssemblyHelper.GetProductName(_assembly));
		yield return () => new Fragment(LabelForProductVersion, AssemblyHelper.GetVersionString(_assembly, false));

		yield return () => new Fragment(LabelForAssembly, _assembly.GetName().Name);
		yield return () => new Fragment(LabelForFileName, Path.GetFileName(_assembly.Location));
		yield return () => new Fragment(LabelForRelease, AssemblyHelper.GetReleaseMode(_assembly));
		yield return () => new Fragment(LabelForArchitecture, Environment.Is64BitProcess);
		yield return () => new Fragment(LabelForLocation, Path.GetDirectoryName(_assembly.Location));
		yield return () => new Fragment(LabelForBase, AppContext.BaseDirectory);

		yield return () => new Fragment(LabelForEnvironment, GetEnvironment());
		yield return () => new Fragment(LabelForMachineName, Environment.MachineName);
		yield return () => new Fragment(LabelForOSVersion, Environment.OSVersion.VersionString); // This includes the name of the OS as well
		yield return () => new Fragment(LabelForClrVersion, Environment.Version);
		yield return () => new Fragment(LabelForProcessId, Environment.ProcessId);
	}

	private string GetEnvironment()
	{
		// Fall back to "Production" just to be safe
		var value = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
		return string.IsNullOrWhiteSpace(value) ? "Production" : value;
	}
}
