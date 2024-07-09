namespace AppInfo;


public interface IAppInfoOutputBuilder :
	IAppOutputConsole,
	IAppOutputTrace,
	IAppOutputLog,
	IAppOutputTextFile,
	IAppOutputJsonFile
{
	IAppOutput Build();
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
	IAppInfoOutputBuilder ToLog(Action<string, object?[]> logger);
}


public interface IAppOutputTextFile
{
//TODO: Add filename
//TODO: Add encoding
//TODO: Add option to append, overwrite or create unique every time
	IAppInfoOutputBuilder ToTextFile();
}


//TODO: Rename to IAddOutputJsonFile?
public interface IAppOutputJsonFile
{
	IAppInfoOutputBuilder ToJsonFile();
}
