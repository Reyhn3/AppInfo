using AppInformation.Renderers;


namespace AppInformation.Tests.TestHelpers;


/// <summary>
///     Used to test that the <see cref="AddRenderer" /> method
///     can create an instance.
/// </summary>
internal class ExceptionThrowingRenderer : IRenderer
{
	public void Render(IAppInfo info) =>
		throw new Exception("Intentional exception for testing");
}
