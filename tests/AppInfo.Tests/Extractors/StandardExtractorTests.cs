using System.Reflection;
using AppInfo.Extractors;
using Shouldly;


namespace AppInfo.Tests.Extractors;


public class StandardExtractorTests
{
	private StandardExtractor _sut;

	[SetUp]
	public void PreRun()
	{
//TODO: Replace this assembly with a generated one and reuse it in all tests
		var assembly = Assembly.GetExecutingAssembly();
		_sut = new StandardExtractor(assembly);
	}

	[Test]
	public void Extract_shall_yield_fragment_for_product_name()
	{
		var result = _sut.Extract()?.ToArray();

		result.ShouldNotBeNull();
		result.ShouldNotBeEmpty();
		result.ShouldContain(f => f.Label == StandardExtractor.LabelForProductName);
	}
}
