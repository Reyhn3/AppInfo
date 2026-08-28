using System.Globalization;


namespace AppInformation;


internal static class Constants
{
	public static readonly CultureInfo DefaultCulture = CultureInfo.CurrentUICulture;

	public const string Unknown = "Unknown";
	public const string NA = "N/A";
	public const string LibraryName = "AppInfo";
	public const string TraceCategory = LibraryName;
}
