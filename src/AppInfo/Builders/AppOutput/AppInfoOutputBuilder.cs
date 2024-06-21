using System.Diagnostics;


namespace AppInfo.Builders.AppOutput;


public class AppInfoOutputBuilder : IAppInfoOutputBuilder
{
	private readonly AppInfoOutput _output = new();

	public IAppInfoOutputBuilder ToConsole()
	{
		_output.AddOutput(Console.WriteLine);
		return this;
	}

	public IAppInfoOutputBuilder ToTrace()
	{
//TODO: Add category
		_output.AddOutput(s => Trace.WriteLine(s));
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
