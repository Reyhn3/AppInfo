using System.Reflection;
using AppInfo.Fragments;


namespace AppInfo;


public interface IAppInfoBuilder :
	IAppInfoIdentity,
	IAppInfoTimestamp,
	IAppInfoExtras,
	IAppInfoAssembly,
	IAppOutput
{
	IAppInfo Build();
	IEnumerable<Fragment> Fragments { get; }
	AppOutput Output { get; }
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
