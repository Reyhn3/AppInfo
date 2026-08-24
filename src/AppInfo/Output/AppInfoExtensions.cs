namespace AppInfo.Output;


public static class AppInfoExtensions
{
	public static IAppInfoOutputBuilder WithDefaultOutput(
		this IAppInfo appInfo)
	{
		var builder = new AppInfoOutputBuilder();
		builder.UseAppInfo(appInfo);
		builder.ToConsole();
		return builder;
	}

	public static IAppInfoOutputBuilder WithOutput(
		this IAppInfo appInfo,
		Action<IAppInfoOutputBuilder> configure)
	{
		var builder = new AppInfoOutputBuilder();
		builder.UseAppInfo(appInfo);
		configure(builder);
		return builder;
	}
}
