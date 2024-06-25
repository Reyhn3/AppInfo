using AppInfo.Extractors;
using Shouldly;


namespace AppInfo.Tests.Extractors;


public class ExtrasExtractorTests
{
	[Test]
	public void Extract_shall_yield_fragment_for_each_extra()
	{
		var sut = new ExtrasExtractor(("first", 1), ("second", 2), ("third", 3));
		var result = sut.Extract()?.ToArray();

		result.ShouldNotBeNull();
		result.ShouldNotBeEmpty();
		Helpers.PrintFragments(result);
		result.Length.ShouldBe(3);
		result.ShouldContain(f => string.Equals(f.Label, "first") && Equals(f.Value.Single(), 1));
		result.ShouldContain(f => string.Equals(f.Label, "second") && Equals(f.Value.Single(), 2));
		result.ShouldContain(f => string.Equals(f.Label, "third") && Equals(f.Value.Single(), 3));
	}

	[TestCase(null)]
	[TestCase("")]
	[TestCase("\t")]
	public void Extract_shall_not_yield_fragment_for_extras_without_a_label(string? label)
	{
		var sut = new ExtrasExtractor(("first", 1), (label, 2), ("third", 3));
		var result = sut.Extract()?.ToArray();

		result.ShouldNotBeNull();
		result.ShouldNotBeEmpty();
		Helpers.PrintFragments(result);
		result.Length.ShouldBe(2);
		result.ShouldContain(f => string.Equals(f.Label, "first") && Equals(f.Value.Single(), 1));
		result.ShouldNotContain(f => string.Equals(f.Label, label));
		result.ShouldNotContain(f => Equals(f.Value.Single(), 2));
		result.ShouldContain(f => string.Equals(f.Label, "third") && Equals(f.Value.Single(), 3));
	}

	[TestCase(null)]
	[TestCase("")]
	[TestCase("\t")]
	public void Extract_shall_yield_fragment_for_extras_without_a_value(string? value)
	{
		var sut = new ExtrasExtractor(("first", 1), ("second", value), ("third", 3));
		var result = sut.Extract()?.ToArray();

		result.ShouldNotBeNull();
		result.ShouldNotBeEmpty();
		Helpers.PrintFragments(result);
		result.Length.ShouldBe(3);
		result.ShouldContain(f => string.Equals(f.Label, "first") && Equals(f.Value.Single(), 1));
		result.ShouldContain(f => string.Equals(f.Label, "second")
			&& value == null ? !f.Value.Any() : Equals(f.Value.Single(), value));
		result.ShouldContain(f => string.Equals(f.Label, "third") && Equals(f.Value.Single(), 3));
	}
}
