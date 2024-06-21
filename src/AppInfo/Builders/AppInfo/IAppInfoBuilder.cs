using System.Reflection;
using AppInfo.Builders.AppOutput;


namespace AppInfo.Builders.AppInfo;


public interface IAppInfoBuilder :
	IAppInfoIdentity,
	IAppInfoTimestamp,
	IAppInfoExtras,
	IAppInfoAssembly,
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


public interface IAppInfoExtras
{
	IAppInfoBuilder AddExtras(params (string Label, object? Value)[] extras);
}


public interface IAppInfoAssembly
{
	IAppInfoBuilder AddAssembly(Assembly assembly, string? shortName = null, bool stripCommitHash = false);
}
