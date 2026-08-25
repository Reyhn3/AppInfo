using AppInfo.Renderers;


namespace AppInfo;


public interface IAppInfoOutputBuilder
{
	IAppInfoOutputBuilder UseAppInfo(IAppInfo appInfo);

	IAppInfoOutputBuilder AddRenderer<T>(T renderer)
		where T : IRenderer;

	IAppInfo Write();
	Task<IAppInfo> WriteAsync(CancellationToken cancellationToken = default);
}
