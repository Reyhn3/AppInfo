using System.Globalization;


namespace AppInformation;


public partial class AppInfo
{
	public static IAppInfo BuildAndOutputDefault() =>
		CreateDefaultBuilder()
			.Build()
			.WithDefaultOutput()
			.Write();

	public static IAppInfoBuilder CreateEmptyBuilder() =>
		new AppInfoBuilder();

	public static IAppInfoBuilder CreateDefaultBuilder() =>
		new AppInfoBuilder()
			.UseCulture(CultureInfo.CurrentUICulture)
			.AddStandard();

	internal static IAppInfoOutputBuilder CreateDefaultOutputBuilder(IAppInfo appInfo) =>
		new AppInfoOutputBuilder()
			.UseAppInfo(appInfo)
			.ToConsole();
}
