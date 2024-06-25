using AppInfo.Extractors;
using Shouldly;


namespace AppInfo.Tests.Extractors;


public class IdentityExtractorTests
{
	[TestCase(null)]
	[TestCase("")]
	[TestCase("\t")]
	public void Ctor_shall_throw_exception_if_application_ID_is_null_or_whitespace(string? value) =>
		Should.Throw<ArgumentNullException>(() => new IdentityExtractor(null));

	[Test]
	public void Extract_shall_yield_application_ID()
	{
		const string appId = "test";
		var sut = new IdentityExtractor(appId, null, null);


		var result = sut.Extract().ToArray();


		result.ShouldNotBeNull();
		result.ShouldNotBeEmpty();
		Helpers.PrintFragments(result);
		result.ShouldContain(f => string.Equals(IdentityExtractor.ApplicationIdLabel, f.Label)
			&& string.Equals(appId, (string?)f.Value));
	}

	[Test]
	public void Extract_shall_yield_instance_ID_if_not_null_or_empty()
	{
		const string value = "not empty";
		var sut = new IdentityExtractor("dummy", value, null);


		var result = sut.Extract().ToArray();


		result.ShouldNotBeNull();
		result.ShouldNotBeEmpty();
		Helpers.PrintFragments(result);
		result.ShouldContain(f => string.Equals(IdentityExtractor.InstanceIdLabel, f.Label)
			&& string.Equals(value, (string?)f.Value));
	}

	[TestCase(null)]
	[TestCase("")]
	[TestCase("\t")]
	public void Extract_shall_yield_instance_ID_with_NA_value_if_null_or_empty(string? value)
	{
		var sut = new IdentityExtractor("dummy", value, null);


		var result = sut.Extract().ToArray();


		result.ShouldNotBeNull();
		result.ShouldNotBeEmpty();
		Helpers.PrintFragments(result);
		result.ShouldContain(f => string.Equals(IdentityExtractor.InstanceIdLabel, f.Label)
			&& string.Equals(Constants.NA, (string?)f.Value));
	}

	[Test]
	public void Extract_shall_yield_scope_ID_if_not_null_or_empty()
	{
		const string value = "not empty";
		var sut = new IdentityExtractor("dummy", null, value);


		var result = sut.Extract().ToArray();


		result.ShouldNotBeNull();
		result.ShouldNotBeEmpty();
		Helpers.PrintFragments(result);
		result.ShouldContain(f => string.Equals(IdentityExtractor.ScopeIdLabel, f.Label) && string.Equals(value, (string?)f.Value));
	}

	[TestCase(null)]
	[TestCase("")]
	[TestCase("\t")]
	public void Extract_shall_yield_scope_ID_with_NA_value_if_null_or_empty(string? value)
	{
		var sut = new IdentityExtractor("dummy", null, value);


		var result = sut.Extract().ToArray();


		result.ShouldNotBeNull();
		result.ShouldNotBeEmpty();
		Helpers.PrintFragments(result);
		result.ShouldContain(f => string.Equals(IdentityExtractor.ScopeIdLabel, f.Label)
			&& string.Equals(Constants.NA, (string?)f.Value));
	}

	[Test]
	public void Extract_shall_trim_values()
	{
		const string appId = "app-id";
		const string instanceId = "instance-id";
		const string scopeId = "scope-id";

		var sut = new IdentityExtractor(
			" " + appId + Environment.NewLine,
			"\t" + instanceId + "	",
			Environment.NewLine + scopeId + "\v");


		var result = sut
			.Extract().ToArray();


		result.ShouldNotBeNull();
		result.ShouldNotBeEmpty();
		Helpers.PrintFragments(result);
		result.ShouldContain(f => string.Equals(f.Label, IdentityExtractor.ApplicationIdLabel) && string.Equals(appId, (string?)f.Value));
		result.ShouldContain(f => string.Equals(f.Label, IdentityExtractor.InstanceIdLabel) && string.Equals(instanceId, (string?)f.Value));
		result.ShouldContain(f => string.Equals(f.Label, IdentityExtractor.ScopeIdLabel) && string.Equals(scopeId, (string?)f.Value));
	}
}
