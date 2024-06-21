using AppInfo.Builders.AppInfo;


namespace AppInfo.Builders.AppOutput;


public interface IAppInfoOutputBuilder :
	IAppOutputConsole,
	IAppOutputTrace,
	IAppOutputLog;


public interface IAppOutputConsole
{
	IAppInfoOutputBuilder ToConsole();
}


public interface IAppOutputTrace
{
	IAppInfoOutputBuilder ToTrace();
}

public interface IAppOutputLog
{
	IAppInfoOutputBuilder ToLog(Action<string> logger);
}


public interface IAppInfoOutput
{
	IAppInfoBuilder WithOutput(Action<IAppInfoOutputBuilder> configure);
}
