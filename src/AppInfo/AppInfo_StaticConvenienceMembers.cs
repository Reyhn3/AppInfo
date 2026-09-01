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

	internal static IOutputBuilder CreateEmptyOutputBuilder(IAppInfo appInfo) =>
		new OutputBuilder()
			.UseAppInfo(appInfo);

	internal static IOutputBuilder CreateDefaultOutputBuilder(IAppInfo appInfo) =>
		new OutputBuilder()
			.UseAppInfo(appInfo)
			.ToConsole();
}
