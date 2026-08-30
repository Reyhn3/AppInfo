using AppInformation.Renderers;


namespace AppInformation.Tests.TestHelpers;


internal class ExceptionThrowingRenderer : IRenderer
{
	public void Render(IAppInfo info) =>
		throw new Exception("Intentional exception for testing");
}
