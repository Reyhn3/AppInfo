using AppInfo.Builders.AppInfo;


namespace AppInfo.Builders.AppOutput;


public interface IAppInfoOutputBuilder :
	IAppOutputConsole,
	IAppOutputTrace,
	IAppOutputLog,
	IAppOutputTextFile,
	IAppOutputJsonFile;


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


public interface IAppOutputTextFile
{
	IAppInfoOutputBuilder ToTextFile();
}


public interface IAppOutputJsonFile
{
	IAppInfoOutputBuilder ToJsonFile();
}


public interface IAppInfoOutput
{
	IAppInfoBuilder WithOutput(Action<IAppInfoOutputBuilder> configure);
}
