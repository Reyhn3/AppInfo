using System.Reflection;
using AppInformation.Extractors;


namespace AppInformation.Tests.Extractors;


public class AssemblyExtractorTests
{
	private readonly Assembly _assembly = typeof(IAppInfo).Assembly;

	[Test]
	public void Ctor_shall_use_ExecutingAssembly_if_assembly_is_null()
	{
		var result = new AssemblyExtractor(null);
		result.ShouldNotBeNull();

		// This should check that it is the executing assembly,
		// but it is not possible to check the exact assembly because
		// it is not known at compile time.
		TestHelpers.Helpers.GetFieldValue<Assembly>(result, "_assembly")
			.ShouldNotBeNull();
	}

	[Test]
	public void Ctor_shall_use_the_specified_assembly_if_not_null()
	{
		var result = new AssemblyExtractor(_assembly);
		result.ShouldNotBeNull();
		TestHelpers.Helpers.GetFieldValue<Assembly>(result, "_assembly")
			.ShouldBe(_assembly);
	}

	[Test]
	public void Extract_should_yield_fragment_for_assembly()
	{
		var sut = new AssemblyExtractor(_assembly);

		var result = sut.Extract()?.ToArray();

		result.ShouldNotBeNull();
		result.ShouldNotBeEmpty();
		TestHelpers.Helpers.PrintFragments(result);
		result.Length.ShouldBe(1);
		result.ShouldContain(f => string.Equals(AssemblyExtractor.AssemblyLabel, f.Label));

		var value = result.Single().Value?.ToArray();
		value.ShouldNotBeNull();
		value.ShouldNotBeEmpty();
		TestHelpers.Helpers.PrintValues(value);
		value.Length.ShouldBe(2);
	}

	[Test]
	public void CompileValue_shall_use_assembly_name_if_short_name_is_not_specified()
	{
		var result = AssemblyExtractor.CompileValue(_assembly, null, A.Dummy<bool>())?.ToArray();

		result.ShouldNotBeNull();
		result.ShouldNotBeEmpty();
		TestHelpers.Helpers.PrintValues(result);
		result.Length.ShouldBe(2);
		result.ShouldContain(v => string.Equals("AppInfo", (string?)v, StringComparison.Ordinal));
	}

	[Test]
	public void CompileValue_shall_use_short_name_is_specified()
	{
		var result = AssemblyExtractor.CompileValue(_assembly, "test", A.Dummy<bool>())?.ToArray();

		result.ShouldNotBeNull();
		result.ShouldNotBeEmpty();
		TestHelpers.Helpers.PrintValues(result);
		result.Length.ShouldBe(2);
		result.ShouldContain(v => string.Equals("test", (string?)v, StringComparison.Ordinal));
	}

	[Test]
	public void CompileValue_shall_use_full_version_number_if_strip_is_not_specified()
	{
		var result = AssemblyExtractor.CompileValue(_assembly, A.Dummy<string>(), false)?.ToArray();

		result.ShouldNotBeNull();
		result.ShouldNotBeEmpty();
		TestHelpers.Helpers.PrintValues(result);
		result.Length.ShouldBe(2);
		result.ShouldContain(v => ((string?)v).StartsWith("v1.0.0+"));
	}

	[Test]
	public void CompileValue_shall_use_stripped_version_number_if_strip_is_specified()
	{
		var result = AssemblyExtractor.CompileValue(_assembly, A.Dummy<string>(), true)?.ToArray();

		result.ShouldNotBeNull();
		result.ShouldNotBeEmpty();
		TestHelpers.Helpers.PrintValues(result);
		result.Length.ShouldBe(2);
		result.ShouldContain(v => string.Equals("v1.0.0", (string?)v));
	}
}
