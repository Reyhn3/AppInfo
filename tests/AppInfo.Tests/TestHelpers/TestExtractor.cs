using AppInformation.Extractors;


namespace AppInformation.Tests.TestHelpers;


internal class TestExtractor : IExtractor
{
	public IEnumerable<Fragment> Extract() =>
		A.CollectionOfDummy<Fragment>(1);
}
