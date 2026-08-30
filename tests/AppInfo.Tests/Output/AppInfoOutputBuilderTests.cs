using AppInformation.Renderers;
using AppInformation.Tests.TestHelpers;


namespace AppInformation.Tests.Output;


public class AppInfoOutputBuilderTests
{
	private const string AppInfoFieldName = "_appInfo";
	private const string RenderersFieldName = "_renderers";

	private AppInfoOutputBuilder _sut;

	[SetUp]
	public void PreRun() =>
		_sut = new AppInfoOutputBuilder();

#region Ctor
	[Test]
	public void Ctor_should_not_populate_appinfo_field() =>
		TestHelpers.Helpers.GetFieldValue(new AppInfoOutputBuilder(), AppInfoFieldName)
			.ShouldBeNull();

	[Test]
	public void Ctor_should_populate_renderers_field() =>
		TestHelpers.Helpers.GetFieldValue(new AppInfoOutputBuilder(), RenderersFieldName)
			.ShouldNotBeNull()
			.ShouldBeOfType<List<IRenderer>>()
			.ShouldBeEmpty();
#endregion

#region UseAppInfo
	[Test]
	public void UseAppInfo_shall_do_nothing_if_appinfo_is_null()
	{
		_sut.UseAppInfo(null);
		TestHelpers.Helpers.GetFieldValue(_sut, AppInfoFieldName)
			.ShouldBeNull();
	}
#endregion

#region AddRenderer
	[Test]
	public void AddRenderer_with_instance_shall_do_nothing_if_instance_is_null()
	{
		_sut.AddRenderer((IRenderer)null!);
		TestHelpers.Helpers.GetFieldValue<List<IRenderer>>(_sut, RenderersFieldName)
			.ShouldBeEmpty();
	}

	[Test]
	public void AddRenderer_with_instance_shall_do_nothing_if_instance_is_already_added()
	{
		var renderer = A.Fake<IRenderer>();

		TestHelpers.Helpers.GetFieldValue<List<IRenderer>>(_sut, RenderersFieldName)
			.ShouldBeEmpty();

		// First addition
		_sut.AddRenderer(renderer);
		TestHelpers.Helpers.GetFieldValue<List<IRenderer>>(_sut, RenderersFieldName)!
			.Count.ShouldBe(1);

		// Second addition (should be ignored)
		_sut.AddRenderer(renderer);
		TestHelpers.Helpers.GetFieldValue<List<IRenderer>>(_sut, RenderersFieldName)!
			.Count.ShouldBe(1);
	}

	[Test]
	public void AddRenderer_with_instance_shall_add_the_instance_to_the_collection()
	{
		TestHelpers.Helpers.GetFieldValue<List<IRenderer>>(_sut, RenderersFieldName)
			.ShouldBeEmpty();
		_sut.AddRenderer(A.Fake<IRenderer>());
		TestHelpers.Helpers.GetFieldValue<List<IRenderer>>(_sut, RenderersFieldName)!
			.Count.ShouldBe(1);
	}

	[Test]
	public void AddRenderer_with_instance_shall_add_multiple_instances_to_the_collection()
	{
		TestHelpers.Helpers.GetFieldValue<List<IRenderer>>(_sut, RenderersFieldName)
			.ShouldBeEmpty();

		_sut.AddRenderer(A.Fake<IRenderer>());
		_sut.AddRenderer(A.Fake<IRenderer>());

		TestHelpers.Helpers.GetFieldValue<List<IRenderer>>(_sut, RenderersFieldName)!
			.Count.ShouldBe(2);
	}

	[Test]
	public void AddRenderer_with_instance_shall_add_multiple_instances_of_the_same_type_to_the_collection()
	{
		TestHelpers.Helpers.GetFieldValue<List<IRenderer>>(_sut, RenderersFieldName)
			.ShouldBeEmpty();

		_sut.AddRenderer(A.Fake<IRenderer>());
		_sut.AddRenderer(A.Fake<IRenderer>());

		TestHelpers.Helpers.GetFieldValue<List<IRenderer>>(_sut, RenderersFieldName)!
			.Count.ShouldBe(2);
	}

	[Test]
	public void AddRenderer_without_instance_shall_do_nothing_if_creating_instance_throws_exception() =>
		Should.NotThrow(() => _sut.AddRenderer<ExceptionThrowingRenderer>());

	[Test]
	public void AddRenderer_without_instance_shall_create_instance_and_add_to_the_collection()
	{
		_sut.AddRenderer<TestRenderer>();
		TestHelpers.Helpers.GetFieldValue<List<IRenderer>>(_sut, RenderersFieldName)!
			.Count.ShouldBe(1);
	}
#endregion

#region Write
	[Test]
	public void Write_shall_call_WriteAsync()
	{
		// Arrange

		var renderer = A.Fake<IRenderer>();
		_sut.AddRenderer(renderer);
		_sut.UseAppInfo(A.Dummy<IAppInfo>());

		// Act

		_sut.Write();

		// Assert

//TODO: Assert that it has actually called WriteAsync
		A.CallTo(() => renderer.Render(A<IAppInfo>.Ignored))
			.MustHaveHappenedOnceExactly();
	}
#endregion

#region WriteAsync
	[Test]
	public async Task WriteAsync_shall_do_nothing_if_appinfo_is_null()
	{
		// Arrange

		var renderer = A.Fake<IRenderer>();
		_sut.AddRenderer(renderer);

		// Act

		await _sut.WriteAsync(A.Dummy<CancellationToken>());

		// Assert

		A.CallTo(() => renderer.Render(A<IAppInfo>.Ignored))
			.MustNotHaveHappened();
	}

	[Test]
	public async Task WriteAsync_shall_invoke_each_renderer()
	{
		// Arrange

		var renderer1 = A.Fake<IRenderer>();
		var renderer2 = A.Fake<IRenderer>();
		_sut.AddRenderer(renderer1);
		_sut.AddRenderer(renderer2);

		_sut.UseAppInfo(A.Dummy<IAppInfo>());

		// Act

		await _sut.WriteAsync(A.Dummy<CancellationToken>());

		// Assert

		A.CallTo(() => renderer1.Render(A<IAppInfo>.Ignored)).MustHaveHappened();
		A.CallTo(() => renderer2.Render(A<IAppInfo>.Ignored)).MustHaveHappened();
	}

	[Test]
	public async Task WriteAsync_shall_continue_if_renderer_throws_exception()
	{
		// Arrange

		var renderer1 = A.Fake<IRenderer>();
		var renderer2 = A.Fake<IRenderer>(f => f.ConfigureFake(ff =>
			A.CallTo(() => ff.Render(A<IAppInfo>.Ignored))
				.Throws<Exception>()));
		var renderer3 = A.Fake<IRenderer>();
		_sut.AddRenderer(renderer1);
		_sut.AddRenderer(renderer2);
		_sut.AddRenderer(renderer3);

		_sut.UseAppInfo(A.Dummy<IAppInfo>());

		// Act

		await Should.NotThrowAsync(async () => await _sut.WriteAsync(A.Dummy<CancellationToken>()));

		// Assert

		A.CallTo(() => renderer1.Render(A<IAppInfo>.Ignored)).MustHaveHappened();
		A.CallTo(() => renderer2.Render(A<IAppInfo>.Ignored)).MustHaveHappened();
		A.CallTo(() => renderer3.Render(A<IAppInfo>.Ignored)).MustHaveHappened();
	}
#endregion
}
