namespace AppInfo.Renderers;


public interface IRenderer
{
//TODO: #32: Refactor to accept the built but unrendered fragments, not the IAppInfo
	void Render(IAppInfo info);
}
