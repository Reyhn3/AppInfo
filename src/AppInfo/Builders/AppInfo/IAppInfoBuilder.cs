using AppInfo.Builders.AppOutput;


namespace AppInfo.Builders.AppInfo;


public interface IAppInfoBuilder :
	IAppInfoIdentity,
	IAppInfoTimestamp,
	IAppInfoOutput
{
	IAppInfo Build();
}


public interface IAppInfoIdentity
{
	IAppInfoBuilder WithIdentities();
}


public interface IAppInfoTimestamp
{
	IAppInfoBuilder AddTimestamp();
}
