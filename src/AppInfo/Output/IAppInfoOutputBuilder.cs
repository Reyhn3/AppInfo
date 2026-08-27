using AppInformation.Renderers;


namespace AppInformation;


public interface IAppInfoOutputBuilder
{
	IAppInfoOutputBuilder UseAppInfo(IAppInfo appInfo);

	IAppInfoOutputBuilder AddRenderer<T>(T renderer)
		where T : IRenderer;

	IAppInfoOutputBuilder AddRenderer<T>()
		where T : IRenderer, new();

	IAppInfo Write();
	Task<IAppInfo> WriteAsync(CancellationToken cancellationToken = default);
}
