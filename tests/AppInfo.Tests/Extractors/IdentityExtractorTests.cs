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
			&& string.Equals(appId, (string?)f.Value.Single()));
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
			&& string.Equals(value, (string?)f.Value.Single()));
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
			&& string.Equals(Constants.NA, (string?)f.Value.Single()));
	}

	[Test]
	public void Extract_shall_yield_scope_ID_if_not_null_or_empty()
	{
		const string expected = "test-scope-id";
		Func<object?> factory = () => expected;
		var sut = new IdentityExtractor("dummy", null, factory);


		var result = sut.Extract().ToArray();


		result.ShouldNotBeNull();
		result.ShouldNotBeEmpty();
		Helpers.PrintFragments(result);
		result.ShouldContain(f => string.Equals(IdentityExtractor.ScopeIdLabel, f.Label)
			&& string.Equals(expected, (string?)f.Value.Single()));
	}

	[Test]
	public void Extract_shall_yield_random_scope_ID_if_factory_is_null()
	{
		var sut = new IdentityExtractor("dummy", null, null);


		var result = sut.Extract().ToArray();


		result.ShouldNotBeNull();
		result.ShouldNotBeEmpty();
		Helpers.PrintFragments(result);
		result.ShouldContain(f => string.Equals(IdentityExtractor.ScopeIdLabel, f.Label)
			&& f.Value.Single()! is long);
	}

	[Test]
	public void Extract_shall_yield_random_scope_ID_if_factory_produces_null()
	{
		Func<object?> factory = () => null;
		var sut = new IdentityExtractor("dummy", null, factory);


		var result = sut.Extract().ToArray();


		result.ShouldNotBeNull();
		result.ShouldNotBeEmpty();
		Helpers.PrintFragments(result);
		result.ShouldContain(f => string.Equals(IdentityExtractor.ScopeIdLabel, f.Label)
			&& f.Value.Single()! is long);
	}

	[Test]
	public void Extract_shall_trim_values()
	{
		const string appId = "app-id";
		const string instanceId = "instance-id";

		var sut = new IdentityExtractor(
			" " + appId + Environment.NewLine,
			"\t" + instanceId + "	",
			null);


		var result = sut
			.Extract().ToArray();


		result.ShouldNotBeNull();
		result.ShouldNotBeEmpty();
		Helpers.PrintFragments(result);
		result.ShouldContain(f => string.Equals(f.Label, IdentityExtractor.ApplicationIdLabel)
			&& string.Equals(appId, (string?)f.Value.Single()));
		result.ShouldContain(f => string.Equals(f.Label, IdentityExtractor.InstanceIdLabel)
			&& string.Equals(instanceId, (string?)f.Value.Single()));
	}
}
