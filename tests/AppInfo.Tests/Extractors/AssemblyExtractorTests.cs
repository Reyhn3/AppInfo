using System.Reflection;
using AppInfo.Extractors;
using Shouldly;


namespace AppInfo.Tests.Extractors;


public class AssemblyExtractorTests
{
	private readonly Assembly _assembly = typeof(IAppInfo).Assembly;

	[Test]
	public void Ctor_shall_throw_exception_if_assembly_is_null() =>
		Should.Throw<ArgumentNullException>(() => new AssemblyExtractor(null));

	[Test]
	public void Extract_should_yield_fragment_for_assembly()
	{
		var sut = new AssemblyExtractor(_assembly);


		var result = sut.Extract()?.ToArray();


		result.ShouldNotBeNull();
		result.ShouldNotBeEmpty();
		Helpers.PrintFragments(result);
		result.ShouldContain(f => string.Equals(AssemblyExtractor.AssemblyLabel, f.Label)
			&& Equals(f.Value, "AppInfo"));
	}

	[Test]
	public void Extract_should_yield_fragment_for_assembly_and_use_specified_short_name()
	{
		const string expected = "test";
		var sut = new AssemblyExtractor(_assembly, expected);


		var result = sut.Extract()?.ToArray();


		result.ShouldNotBeNull();
		result.ShouldNotBeEmpty();
		Helpers.PrintFragments(result);
		result.ShouldContain(f => string.Equals(AssemblyExtractor.AssemblyLabel, f.Label)
			&& Equals(f.Value, expected));
	}
}
