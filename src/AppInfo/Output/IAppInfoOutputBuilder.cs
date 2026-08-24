using AppInfo.Renderers;


namespace AppInfo.Output;


public interface IAppInfoOutputBuilder
{
	void UseAppInfo(IAppInfo appInfo);

	void AddRenderer<T>(T renderer)
		where T : IRenderer;

	IAppInfo Write();
	Task<IAppInfo> WriteAsync(CancellationToken cancellationToken = default);
}
