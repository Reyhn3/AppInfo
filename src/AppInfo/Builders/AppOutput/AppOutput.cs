namespace AppInfo;


public class AppOutput
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
