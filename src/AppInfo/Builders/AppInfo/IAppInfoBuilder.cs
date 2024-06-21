using AppInfo.Builders.AppOutput;


namespace AppInfo.Builders.AppInfo;


public interface IAppInfoBuilder :
	IAppIdentity,
	IAppTimestamp,
	IAppOutput
{
	IAppInfo Build();
}


public interface IAppIdentity
{
	IAppInfoBuilder WithIdentities();
}


public interface IAppTimestamp
{
	IAppInfoBuilder AddTimestamp();
}
