using AppInformation.Renderers;


namespace AppInformation.Tests.TestHelpers;


/// <summary>
///     Used to test that the <see cref="AddRenderer" /> method
///     can create an instance.
/// </summary>
internal class TestRenderer : IRenderer
{
	public IAppInfo? AppInfo { get; private set; }

	public void Render(IAppInfo info) =>
		AppInfo = info;
}
