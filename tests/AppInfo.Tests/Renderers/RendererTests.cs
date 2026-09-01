using System.Globalization;
using AppInformation.Extractors;
using AppInformation.Renderers;


namespace AppInformation.Tests.Renderers;


public class RendererTests
{
#region Render
	[Test]
	public void Render_shall_not_throw_exception_when_rendering() =>
		Should.NotThrow(() =>
			new StubRenderer(_ => throw new Exception("test"))
				.Render(A.Fake<IAppInfo>()));
#endregion

#region GenerateTitleParts
	[Test]
	public void GenerateTitleParts_shall_return_simple_title_if_ProductName_fragment_is_not_found()
	{
		var appInfo = CreateFakeAppInfo([]);
		var result = StubRenderer.GenerateTitleParts(appInfo);

		result.Lead.ShouldBe("Application ");
		result.Name.ShouldBe(string.Empty);
		result.Tail.ShouldBe(" created with context:");
	}

	[Test]
	public void GenerateTitleParts_shall_return_simple_title_if_ProductName_fragment_is_null()
	{
		var appInfo = CreateFakeAppInfo([new Fragment(StandardExtractor.LabelForProductName, null)]);
		var result = StubRenderer.GenerateTitleParts(appInfo);

		result.Lead.ShouldBe("Application ");
		result.Name.ShouldBe(string.Empty);
		result.Tail.ShouldBe(" created with context:");
	}

	[Test]
	public void GenerateTitleParts_shall_return_simple_title_if_ProductName_fragment_is_an_empty_array()
	{
		var appInfo = CreateFakeAppInfo([new Fragment(StandardExtractor.LabelForProductName, [])]);
		var result = StubRenderer.GenerateTitleParts(appInfo);

		result.Lead.ShouldBe("Application ");
		result.Name.ShouldBe(string.Empty);
		result.Tail.ShouldBe(" created with context:");
	}

	[Test]
	public void GenerateTitleParts_shall_return_named_title_if_ProductName_fragment_is_found()
	{
		var appInfo = CreateFakeAppInfo([new Fragment(StandardExtractor.LabelForProductName, "test")]);
		var result = StubRenderer.GenerateTitleParts(appInfo);

		result.Lead.ShouldBe("Application ");
		result.Name.ShouldBe("test");
		result.Tail.ShouldBe(" created with context:");
	}
#endregion

#region FormatWithCulture
	[Test]
	public void FormatWithCulture_shall_use_the_culture_from_the_AppInfo()
	{
		var appInfo = CreateFakeAppInfo([]);
		A.CallTo(() => appInfo.Culture)
			.Returns(CultureInfo.CreateSpecificCulture("sv-SE"));
		var sut = new StubRenderer(ai => {});
		sut.Render(appInfo); // This is required to initialize the renderer with the CultureInfo

		var result = sut.FormatWithCulture(1234.567m);

		result.ShouldBe("1234,567");
	}
#endregion

	private static IAppInfo CreateFakeAppInfo(IEnumerable<Fragment> fragments) =>
		A.Fake<IAppInfo>(f => f.ConfigureFake(ff =>
			A.CallTo(() => ff.Fragments)
				.Returns(fragments)));


	private class StubRenderer(Action<IAppInfo> renderAppInfo) : Renderer
	{
		protected override void RenderAppInfo(IAppInfo info) =>
			renderAppInfo(info);

		internal static new Title GenerateTitleParts(IAppInfo info) =>
			Renderer.GenerateTitleParts(info);

		public new string FormatWithCulture(object value) =>
			base.FormatWithCulture(value);
	}
}
