using AppInfo.Builders.AppInfo;


namespace AppInfo.Builders.AppOutput;


public interface IAppInfoOutputBuilder
{
	IAppInfoOutputBuilder ToConsole();
	IAppInfoOutputBuilder ToTrace();
}


public interface IAppOutput
{
	IAppInfoBuilder WithOutput(Action<IAppInfoOutputBuilder> configure);
}
