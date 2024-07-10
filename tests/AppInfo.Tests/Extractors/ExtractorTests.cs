using AppInfo.Extractors;
using Shouldly;


namespace AppInfo.Tests.Extractors;


public class ExtractorTests
{
	[Test]
	public void Extract_shall_yield_no_fragments_if_fails_to_produce_extractors()
	{
		var sut = new DummyExtractor(
			Enumerable
				.Range(0, 1)
				.Select<int, Func<Fragment>>(_ => () => throw new Exception("test")));

		var result = sut.Extract().ToArray();
		result.ShouldNotBeNull();
		result.ShouldBeEmpty();
	}

	[Test]
	public void Extract_shall_yield_no_fragments_if_there_are_no_extractors()
	{
		var sut = new DummyExtractor([]);
		var result = sut.Extract().ToArray();
		result.ShouldNotBeNull();
		result.ShouldBeEmpty();
	}

	[Test]
	public void Extract_shall_invoke_all_extractors()
	{
		// Arrange

		var extractors = new[]
			{
				() => new Fragment("1", 1),
				() => new Fragment("2", 2),
				() => new Fragment("3", 3)
			};

		var expected = extractors.Select(e => e()).Select(f => f.Value!.Single()).ToArray();

		var sut = new DummyExtractor(extractors);


		// Act

		var result = sut.Extract().ToArray();


		// Assert

		result.ShouldNotBeNull();
		result.ShouldNotBeEmpty();
		result.Length.ShouldBe(extractors.Length);
		result.ShouldAllBe(f => f != null);
		result.Select(f => f.Value!.Single()).ToArray().ShouldBeEquivalentTo(expected);
	}

	[Test]
	public void Extract_shall_invoke_all_functional_extractors()
	{
		// Arrange

		var extractors = new[]
			{
				() => new Fragment("1", 1),
				() => throw new Exception("test"),
				() => new Fragment("3", 3)
			};

		var expected = new object[]
			{
				1,
				3
			};

		var sut = new DummyExtractor(extractors);


		// Act

		var result = sut.Extract().ToArray();


		// Assert

		result.ShouldNotBeNull();
		result.ShouldNotBeEmpty();
		result.Length.ShouldBe(extractors.Length - 1);
		result.ShouldAllBe(f => f != null);
		result.Select(f => f.Value!.Single()).ToArray().ShouldBeEquivalentTo(expected);
	}


	private class DummyExtractor(IEnumerable<Func<Fragment>>? extractors) : Extractor
	{
		private readonly Func<Fragment>[]? _extractors = extractors?.ToArray();

		protected override IEnumerable<Func<Fragment>> ProduceExtractors() =>
			_extractors!;
	}
}
