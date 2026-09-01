namespace AppInformation;


public partial class AppInfo
{
	public static IAppInfo BuildAndOutputDefault() =>
		CreateDefaultBuilder()
			.Build()
			.WithDefaultOutput()
			.Write();

	public static IInputBuilder CreateEmptyBuilder() =>
		new InputBuilder();

	public static IInputBuilder CreateDefaultBuilder() =>
		new InputBuilder()
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
