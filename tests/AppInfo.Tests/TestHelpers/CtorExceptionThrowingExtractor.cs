using AppInformation.Extractors;


namespace AppInformation.Tests.TestHelpers;


internal class CtorExceptionThrowingExtractor : IExtractor
{
	public CtorExceptionThrowingExtractor()
	{
		throw new Exception("Intentional exception for testing");
	}

	public IEnumerable<Fragment> Extract() =>
		throw new NotImplementedException("Ignored");
}
