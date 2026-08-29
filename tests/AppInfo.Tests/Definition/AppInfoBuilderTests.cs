using System.Globalization;
using AppInformation.Extractors;


namespace AppInformation.Tests.Definition;


public class AppInfoBuilderTests
{
	private AppInfoBuilder _sut;
	private string _extractorsFieldName = "_extractors";

	[SetUp]
	public void PreRun() =>
		_sut = new AppInfoBuilder();

#region Ctor
	[Test]
	public void Ctor_should_populate_culture_field() =>
		TestHelpers.Helpers.GetFieldValue(new AppInfoBuilder(), "_culture")
			.ShouldNotBeNull()
			.ShouldBeOfType<CultureInfo>()
			.ShouldBe(Constants.DefaultCulture);

	[Test]
	public void Ctor_should_populate_extractors_field() =>
		TestHelpers.Helpers.GetFieldValue(new AppInfoBuilder(), _extractorsFieldName)
			.ShouldNotBeNull()
			.ShouldBeOfType<List<IExtractor>>()
			.ShouldBeEmpty();
#endregion

#region UseCulture
	[Test]
	public void UseCulture_shall_do_nothing_if_culture_is_null()
	{
		_sut.UseCulture(null);
		TestHelpers.Helpers.GetFieldValue(_sut, "_culture")
			.ShouldBe(Constants.DefaultCulture);
	}
#endregion

#region AddExtractor
	[Test]
	public void AddExtractor_with_instance_shall_do_nothing_if_instance_is_null()
	{
		_sut.AddExtractor((IExtractor)null!);
		TestHelpers.Helpers.GetFieldValue<List<IExtractor>>(_sut, _extractorsFieldName)
			.ShouldBeEmpty();
	}

	[Test]
	public void AddExtractor_with_instance_shall_do_nothing_if_instance_is_already_added()
	{
		var extractor = A.Fake<IExtractor>();

		TestHelpers.Helpers.GetFieldValue<List<IExtractor>>(_sut, _extractorsFieldName)
			.ShouldBeEmpty();

		// First addition
		_sut.AddExtractor(extractor);
		TestHelpers.Helpers.GetFieldValue<List<IExtractor>>(_sut, _extractorsFieldName)!
			.Count.ShouldBe(1);

		// Second addition (should be ignored)
		_sut.AddExtractor(extractor);
		TestHelpers.Helpers.GetFieldValue<List<IExtractor>>(_sut, _extractorsFieldName)!
			.Count.ShouldBe(1);
	}

	[Test]
	public void AddExtractor_with_instance_shall_add_the_instance_to_the_collection()
	{
		TestHelpers.Helpers.GetFieldValue<List<IExtractor>>(_sut, _extractorsFieldName)
			.ShouldBeEmpty();
		_sut.AddExtractor(A.Fake<IExtractor>());
		TestHelpers.Helpers.GetFieldValue<List<IExtractor>>(_sut, _extractorsFieldName)!
			.Count.ShouldBe(1);
	}

	[Test]
	public void AddExtractor_with_instance_shall_add_multiple_instances_to_the_collection()
	{
		TestHelpers.Helpers.GetFieldValue<List<IExtractor>>(_sut, _extractorsFieldName)
			.ShouldBeEmpty();

		_sut.AddExtractor(A.Fake<IExtractor>());
		_sut.AddExtractor(A.Fake<IExtractor>());

		TestHelpers.Helpers.GetFieldValue<List<IExtractor>>(_sut, _extractorsFieldName)!
			.Count.ShouldBe(2);
	}

	[Test]
	public void AddExtractor_with_instance_shall_add_multiple_instances_of_the_same_type_to_the_collection()
	{
		TestHelpers.Helpers.GetFieldValue<List<IExtractor>>(_sut, _extractorsFieldName)
			.ShouldBeEmpty();

		_sut.AddExtractor(new TestExtractor());
		_sut.AddExtractor(new TestExtractor());

		TestHelpers.Helpers.GetFieldValue<List<IExtractor>>(_sut, _extractorsFieldName)!
			.Count.ShouldBe(2);
	}

	[Test]
	public void AddExtractor_without_instance_shall_do_nothing_if_creating_instance_throws_exception() =>
		Should.NotThrow(() => _sut.AddExtractor<ExceptionThrowingExtractor>());

	[Test]
	public void AddExtractor_without_instance_shall_create_instance_and_add_to_the_collection()
	{
		_sut.AddExtractor<TestExtractor>();
		TestHelpers.Helpers.GetFieldValue<List<IExtractor>>(_sut, _extractorsFieldName)!
			.Count.ShouldBe(1);
	}
#endregion

#region Build
	[Test]
	public void Build_shall_not_throw_exception_if_any_extractor_throws_exceptions()
	{
		_sut.AddExtractor<TestExtractor>();
		_sut.AddExtractor<ExceptionThrowingExtractor>();
		_sut.AddExtractor(A.Dummy<IExtractor>());

		Should.NotThrow(() => _sut.Build());
	}

	[Test]
	public void Build_shall_not_throw_exception_if_any_extractor_throws_exceptions_but_continue_adding_all_functional_extractors()
	{
		// Arrange

		_sut.AddExtractor<TestExtractor>();
		_sut.AddExtractor<ExceptionThrowingExtractor>();
		_sut.AddExtractor(A.Fake<IExtractor>(f => f.ConfigureFake(ff =>
			A.CallTo(() => ff.Extract())
				.Returns(A.CollectionOfDummy<Fragment>(1)))));

		// Act

		var result = Should.NotThrow(() => _sut.Build());

		// Assert

		result.ShouldNotBeNull();
		result.Fragments.ShouldNotBeEmpty();
		result.Fragments.Count().ShouldBe(2);
	}

	[Test]
	public void Build_shall_return_app_info_with_all_extracted_fragments()
	{
		// Arrange
		_sut.AddExtractor(A.Fake<IExtractor>(f => f.ConfigureFake(ff =>
			A.CallTo(() => ff.Extract())
				.Returns(A.CollectionOfDummy<Fragment>(1)))));
		_sut.AddExtractor(A.Fake<IExtractor>(f => f.ConfigureFake(ff =>
			A.CallTo(() => ff.Extract())
				.Returns(A.CollectionOfDummy<Fragment>(2)))));
		_sut.AddExtractor(A.Fake<IExtractor>(f => f.ConfigureFake(ff =>
			A.CallTo(() => ff.Extract())
				.Returns(A.CollectionOfDummy<Fragment>(3)))));

		// Act

		var result = Should.NotThrow(() => _sut.Build());

		// Assert

		result.ShouldNotBeNull();
		result.Fragments.ShouldNotBeEmpty();
		result.Fragments.Count().ShouldBe(6);
	}

	[Test]
	public void Build_shall_return_app_info_with_the_specified_culture()
	{
		// Arrange

		_sut.UseCulture(CultureInfo.CreateSpecificCulture("de-DE"));

		// Act

		var result = _sut.Build();

		// Assert

		result.ShouldNotBeNull();
		result.Culture.ShouldNotBeNull();
		result.Culture.Name.ShouldBe("de-DE");
	}
#endregion

#region Helpers
	private class TestExtractor : IExtractor
	{
		public IEnumerable<Fragment> Extract() =>
			A.CollectionOfDummy<Fragment>(1);
	}


	private class ExceptionThrowingExtractor : IExtractor
	{
		public IEnumerable<Fragment> Extract() =>
			throw new Exception("Intentional exception for testing");
	}


	private class CtorExceptionThrowingExtractor : IExtractor
	{
		public CtorExceptionThrowingExtractor()
		{
			throw new Exception("Intentional exception for testing");
		}

		public IEnumerable<Fragment> Extract() =>
			throw new NotImplementedException("Ignored");
	}
#endregion
}
