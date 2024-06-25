using System.Collections.Immutable;
using AppInfo.Renderers;


namespace AppInfo;


public sealed class AppOutput
{
	private readonly ImmutableArray<IRenderer> _renderers;

	internal AppOutput(IEnumerable<IRenderer> renderers)
	{
		if (renderers == null)
			throw new ArgumentNullException(nameof(renderers));

		_renderers = [..renderers];
	}

//TODO: Make async
//TODO: Make safe
	public void Execute(IAppInfo appInfo)
	{
		foreach (var renderer in _renderers)
		{
			renderer.Render(appInfo);
		}
	}
}
