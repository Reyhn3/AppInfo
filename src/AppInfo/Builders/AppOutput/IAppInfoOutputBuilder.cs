namespace AppInfo;


public interface IAppInfoOutputBuilder :
	IAppOutputConsole,
	IAppOutputTrace,
	IAppOutputLog,
	IAppOutputTextFile,
	IAppOutputJsonFile
{
	AppOutput Build();
}


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


//TODO: Rename to IAddOutputJsonFile?
public interface IAppOutputJsonFile
{
	IAppInfoOutputBuilder ToJsonFile();
}


//TODO: Rename to IAddOutput?
public interface IAppOutput
{
	IAppInfoBuilder WithOutput(Action<IAppInfoOutputBuilder> configure);
}
