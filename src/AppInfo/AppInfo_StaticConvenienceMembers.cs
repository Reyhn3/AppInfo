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
			.UseCulture(Constants.DefaultCulture)
			.AddStandard();

	internal static IAppInfoOutputBuilder CreateEmptyOutputBuilder(IAppInfo appInfo) =>
		new AppInfoOutputBuilder()
			.UseAppInfo(appInfo);

	internal static IAppInfoOutputBuilder CreateDefaultOutputBuilder(IAppInfo appInfo) =>
		new AppInfoOutputBuilder()
			.UseAppInfo(appInfo)
			.ToConsole();
}
