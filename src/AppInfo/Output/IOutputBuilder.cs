using AppInformation.Renderers;


namespace AppInformation;


public interface IOutputBuilder
{
	IOutputBuilder UseAppInfo(IAppInfo appInfo);

	IOutputBuilder AddRenderer<T>(T renderer)
		where T : IRenderer;

	IOutputBuilder AddRenderer<T>()
		where T : IRenderer, new();

	IAppInfo Write();
	Task<IAppInfo> WriteAsync(CancellationToken cancellationToken = default);
}
