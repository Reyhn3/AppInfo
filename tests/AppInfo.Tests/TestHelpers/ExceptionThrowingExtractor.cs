using AppInformation.Extractors;


namespace AppInformation.Tests.TestHelpers;


internal class ExceptionThrowingExtractor : IExtractor
{
	public IEnumerable<Fragment> Extract() =>
		throw new Exception("Intentional exception for testing");
}
