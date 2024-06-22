using System.Diagnostics;


namespace AppInfo;


public class AppInfoOutputBuilder : IAppInfoOutputBuilder
{
	private readonly List<Action<string>> _writers = [];

	public static AppOutput Default { get; } = new AppInfoOutputBuilder().ToConsole().Build();

	public IAppInfoOutputBuilder ToConsole()
	{
//TODO: Remove prefix
		// __writers.Add(Console.WriteLine);
		_writers.Add(s => Console.WriteLine("StdOut: {0}", s));
		return this;
	}

	public IAppInfoOutputBuilder ToTrace()
	{
//TODO: Remove prefix
//TODO: Add category
		_writers.Add(s => Trace.WriteLine("Trace: " + s));
		return this;
	}

	public IAppInfoOutputBuilder ToLog(Action<string> logger)
	{
		_writers.Add(logger);
		return this;
	}

	public IAppInfoOutputBuilder ToTextFile()
	{
//TODO: Write to plain text file
		_writers.Add(s => Console.WriteLine("Text file: {0}", s));
		return this;
	}

	public IAppInfoOutputBuilder ToJsonFile()
	{
//TODO: Write to JSON text file
		_writers.Add(s => Console.WriteLine("JSON file: {0}", s));
		return this;
	}

	public AppOutput Build()
	{
		var output = new AppOutput();
		foreach (var writer in _writers)
			output.AddOutput(writer);

		return output;
	}
}
