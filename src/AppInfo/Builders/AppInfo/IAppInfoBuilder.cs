using System.Globalization;
using System.Reflection;


namespace AppInfo;


public interface IAppInfoBuilder :
	IAppInfoIdentity,
	IAppInfoTimestamp,
	IAppInfoExtras,
	IAppInfoAssembly,
	IAppInfoCulture,
	IAddOutput
{
	IAppInfo Build();
	CultureInfo Culture { get; }
	IAppOutput Output { get; }
}


public interface IAppInfoCulture
{
	IAppInfoBuilder UseCulture(CultureInfo cultureInfo);
}


public interface IAppInfoIdentity
{
	IAppInfoBuilder WithIdentities(
		string appId,
		string? instanceId = null,
		Func<object?>? scopeIdFactory = null,
		params string[] args);
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
	IAppInfoBuilder AddAssembly(Assembly assembly, string? shortName = null, bool stripSourceRevision = false);
}


public interface IAddOutput
{
	IAppInfoBuilder WithOutput(Action<IAppInfoOutputBuilder> configure);
}
