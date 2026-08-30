using System.Diagnostics;
using System.Globalization;
using AppInformation.Renderers;


namespace AppInformation;


public class AppInfoOutputBuilder : IAppInfoOutputBuilder
{
	private IAppInfo? _appInfo;
	private readonly List<IRenderer> _renderers = new();

	public IAppInfoOutputBuilder UseAppInfo(IAppInfo appInfo)
	{
		// ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
		if (appInfo == null)
		{
			Debug.WriteLine("Attempted to use a null appInfo");
			return this;
		}

		_appInfo = appInfo;
		return this;
	}

	public IAppInfoOutputBuilder AddRenderer<T>(T renderer)
		where T : IRenderer
	{
		// ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
		if (renderer == null)
		{
			Debug.WriteLine("Attempted to add a null renderer");
			return this;
		}

		if (_renderers.Contains(renderer))
		{
			Debug.WriteLine("Attempted to add duplicate renderer");
			return this;
		}

		_renderers.Add(renderer);
		return this;
	}

	public IAppInfoOutputBuilder AddRenderer<T>()
		where T : IRenderer, new()
	{
		try
		{
			var renderer = new T();
			AddRenderer(renderer);
			return this;
		}
		catch (Exception ex)
		{
			Debug.WriteLine($"Exception caught when trying to create and add renderer of type {typeof(T)}: {ex}");
			return this;
		}
	}

	public IAppInfo Write() =>
		WriteAsync(CancellationToken.None).GetAwaiter().GetResult();

	public async Task<IAppInfo> WriteAsync(CancellationToken cancellationToken)
	{
		if (_appInfo == null)
		{
			Debug.WriteLine($"Attempted to call {nameof(WriteAsync)} without calling {nameof(UseAppInfo)}");
			return new AppInfo(Constants.DefaultCulture, Enumerable.Empty<Fragment>());
		}

		var tasks = _renderers.Select(renderer =>
			InvokeRenderer(renderer, _appInfo, cancellationToken));
		await Task.WhenAll(tasks);

		return _appInfo;
	}

	private static async Task InvokeRenderer(IRenderer renderer, IAppInfo appInfo, CancellationToken cancellationToken)
	{
		try
		{
//TODO: Make async
			renderer.Render(appInfo);
			await Task.CompletedTask;
		}
		catch (Exception ex)
		{
			Debug.WriteLine("Exception caught when invoking renderer: {0}", ex);
		}
	}
}
