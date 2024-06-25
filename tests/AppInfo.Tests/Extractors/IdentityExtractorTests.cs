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
	public void GetInstanceId_shall_yield_first_cli_then_config_then_argument()
	{
		const string expectedCli = "from-cli";
		const string expectedFactory = "from-settings";
		const string expectedArg = "from-argument";

		var fromCliWithoutValue = Array.Empty<string>();
		var fromCliWithValidValue = new[]{"--" + IdentityExtractor.InstanceIdLabel, expectedCli};
		Func<object?> fromFactoryWithoutValue = () => null;
		Func<object?> fromFactoryWithValidValue = () => expectedFactory;
		const string? fromArgumentWithoutValue = (string?)null;
		const string fromArgumentWithValidValue = expectedArg;


		// CLI wins
		var result1 = IdentityExtractor.GetInstanceId(fromCliWithValidValue, fromFactoryWithValidValue, fromArgumentWithValidValue);
		result1.ShouldNotBeNull().ShouldBe(expectedCli);

		// Factory wins
		var result2 = IdentityExtractor.GetInstanceId(fromCliWithoutValue, fromFactoryWithValidValue, fromArgumentWithValidValue);
		result2.ShouldNotBeNull().ShouldBe(expectedFactory);

		// Argument wins
		var result3 = IdentityExtractor.GetInstanceId(fromCliWithoutValue, fromFactoryWithoutValue, fromArgumentWithValidValue);
		result3.ShouldNotBeNull().ShouldBe(expectedArg);

		// Fallback wins
		var result4 = IdentityExtractor.GetInstanceId(fromCliWithoutValue, fromFactoryWithoutValue, fromArgumentWithoutValue);
		result4.ShouldBeNull();
	}

	[Test]
	public void GetInstanceId_shall_yield_instance_ID_from_cli()
	{
		const string expected = "instance-id-from-cli";
		var result = IdentityExtractor.GetInstanceId(["--" + IdentityExtractor.InstanceIdLabel, expected], null, null);


		result.ShouldNotBeNull();
		result.ShouldBe(expected);
	}

	[Test]
	public void GetInstanceId_shall_yield_instance_ID_from_factory()
	{
		const string expected = "instance-id-from-factory";
		Func<object?> factory = () => expected;
		var result = IdentityExtractor.GetInstanceId(null, factory, null);


		result.ShouldNotBeNull();
		result.ShouldBe(expected);
	}

	[Test]
	public void GetInstanceId_shall_yield_instance_ID_from_argument()
	{
		const string expected = "not empty";


		var result = IdentityExtractor.GetInstanceId(null, null, expected);


		result.ShouldNotBeNull();
		result.ShouldBe(expected);
	}

	[Test]
	public void GetScopeId_shall_yield_scope_ID_if_not_null_or_empty()
	{
		const string expected = "test-scope-id";
		Func<object?> factory = () => expected;


		var result = IdentityExtractor.GetScopeId(factory);


		result.ShouldNotBeNull();
		result.ShouldBe(expected);
	}

	[Test]
	public void GetScopeId_shall_yield_random_scope_ID_if_factory_is_null() =>
		IdentityExtractor.GetScopeId(null)
			.ShouldNotBeNull()
			.ShouldBeOfType<long>();

	[Test]
	public void GetScopeId_shall_yield_random_scope_ID_if_factory_produces_null() =>
		IdentityExtractor.GetScopeId((Func<object?>)(() => null))
			.ShouldNotBeNull()
			.ShouldBeOfType<long>();

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
