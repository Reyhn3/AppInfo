using AppInformation.Renderers;


namespace AppInformation.Tests.TestHelpers;


internal class TestRenderer : IRenderer
{
	public IAppInfo? AppInfo { get; private set; }
	public bool HasRenderBeenCalled { get; private set; }

	public void Render(IAppInfo info)
	{
		HasRenderBeenCalled = true;
		AppInfo = info;
	}
}
