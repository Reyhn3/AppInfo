namespace AppInfo;


public class AppInfo
{
	public static IAppInfoBuilder CreateDefaultBuilder()
	{
		AppInfoBuilder builder = new();
		return builder.ConfigureDefaults();
	}
}


public class AppInfoBuilder : IAppInfoBuilder
{
	public IAppInfo Build() =>
		throw new NotImplementedException();

//TODO: Implement identities
	public IAppInfoBuilder ConfigureIdentities()
	{
		throw new NotImplementedException();
		return this;
	}
}


public static class AppInfoBuilderExtensions
{
//TODO: Add more defaults
	public static IAppInfoBuilder ConfigureDefaults(this IAppInfoBuilder builder)
	{
		throw new NotImplementedException();
		return builder
			.ConfigureIdentities();
	}

	public static IAppInfoBuilder WithIdentities(this IAppInfoBuilder builder)
	{
		throw new NotImplementedException();
//TODO: Add identities
		return builder;
	}

	public static IAppInfoBuilder AddTimestamp(this IAppInfoBuilder builder)
	{
		throw new NotImplementedException();
//TODO: Add timestamp
		return builder;
	}

	public static IAppInfoBuilder WriteToConsole(this IAppInfoBuilder builder)
	{
		throw new NotImplementedException();
//TODO: Add console output
		return builder;
	}
}
