using System.Globalization;


namespace AppInfo;


public static class Default
{
	public static IAppInfo BuildAndOutput() =>
		Create.DefaultBuilder()
			.Build()
			.WithDefaultOutput()
			.Write();
}


public static class Create
{
	public static IAppInfoBuilder DefaultBuilder() =>
		new AppInfoBuilder()
			.UseCulture(CultureInfo.CurrentUICulture)
			.AddStandard();
}
