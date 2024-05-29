namespace AppInfo;


public interface IAppInfo
{}


public interface IAppInfoBuilder
{
	IAppInfo Build();

	IAppInfoBuilder ConfigureIdentities();
}
