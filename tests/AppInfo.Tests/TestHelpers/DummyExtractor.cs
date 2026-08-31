using AppInformation.Extractors;


namespace AppInformation.Tests.TestHelpers;


internal class DummyExtractor(IEnumerable<Func<Fragment>>? extractors) : Extractor
{
	private readonly Func<Fragment>[]? _extractors = extractors?.ToArray();

	protected override IEnumerable<Func<Fragment>> ProduceExtractors() =>
		_extractors!;
}