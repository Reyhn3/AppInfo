using System.Globalization;


namespace AppInfo;


public static class Default
{
	public static IAppInfo BuildAndOutput() =>
		Create.DefaultBuilder()
			.Build()
			.WithDefaultOutput()
			.Write();

	internal static IAppInfoBuilder DefaultBuilder() =>
		new AppInfoBuilder()
			.UseCulture(CultureInfo.CurrentUICulture)
			.AddStandard();

	internal static IAppInfoOutputBuilder DefaultOutputBuilder(IAppInfo appInfo) =>
		new AppInfoOutputBuilder()
			.UseAppInfo(appInfo)
			.ToConsole();
}


public static class Create
{
	public static IAppInfoBuilder DefaultBuilder() =>
		Default.DefaultBuilder();
}
