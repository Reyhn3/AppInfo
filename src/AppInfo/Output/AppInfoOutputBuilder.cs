using System.Diagnostics;
using System.Globalization;
using AppInfo.Renderers;


namespace AppInfo.Output;


public class AppInfoOutputBuilder : IAppInfoOutputBuilder
{
	private IAppInfo? _appInfo;
	private readonly List<IRenderer> _renderers = new();

	public void UseAppInfo(IAppInfo appInfo)
	{
		// ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
		if (appInfo == null)
		{
			Debug.WriteLine("Attempted to use a null appInfo");
			return;
		}

		_appInfo = appInfo;
	}

	public void AddRenderer<T>(T renderer)
		where T : IRenderer
	{
		// ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
		if (renderer == null)
		{
			Debug.WriteLine("Attempted to add a null renderer");
			return;
		}

		if (_renderers.Contains(renderer))
		{
			Debug.WriteLine("Attempted to add duplicate renderer");
			return;
		}

		_renderers.Add(renderer);
	}

	public IAppInfo Write() =>
		WriteAsync(CancellationToken.None).GetAwaiter().GetResult();

	public Task<IAppInfo> WriteAsync(CancellationToken cancellationToken)
	{
		if (_appInfo == null)
		{
			Debug.WriteLine($"Attempted to call {nameof(WriteAsync)} without calling {nameof(UseAppInfo)}");
			return Task.FromResult<IAppInfo>(new AppInfo(CultureInfo.CurrentUICulture, Enumerable.Empty<Fragment>()));
		}

//TODO: Make async
//TODO: Make safe
		foreach (var renderer in _renderers)
		{
			renderer.Render(_appInfo);
		}

		return Task.FromResult(_appInfo);
	}
}
