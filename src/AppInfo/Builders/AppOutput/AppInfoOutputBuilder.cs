using System.Diagnostics;


namespace AppInfo.Builders.AppOutput;


public class AppInfoOutputBuilder : IAppInfoOutputBuilder
{
	private readonly AppInfoOutput _output = new();

	public IAppInfoOutputBuilder ToConsole()
	{
//TODO: Remove prefix
		// _output.AddOutput(Console.WriteLine);
		_output.AddOutput(s => Console.WriteLine("StdOut: {0}", s));
		return this;
	}

	public IAppInfoOutputBuilder ToTrace()
	{
//TODO: Remove prefix
//TODO: Add category
		_output.AddOutput(s => Trace.WriteLine("Trace: " + s));
		return this;
	}

	public IAppInfoOutputBuilder ToLog(Action<string> logger)
	{
		_output.AddOutput(logger);
		return this;
	}

	internal AppInfoOutput Build() =>
		_output;
}


internal class AppInfoOutput
{
	private List<Action<string>> _writers = [];

	public void AddOutput(Action<string> writer) =>
		_writers.Add(writer);

//TODO: Make async
//TODO: Make safe
	public void Execute(IAppInfo appInfo)
	{
		foreach (var writer in _writers)
		{
			writer(appInfo.Formatted);
		}
	}
}
