using Shouldly;


namespace AppInfo.Tests;


public class AppInfoTests
{
	[SetUp]
	public void PreRun()
	{}

	[Test]
	public void CreateDefaultBuilder_shall_return_new_builder_with_default_configuration() =>
		Should.NotThrow(() => throw new NotImplementedException());
}


public class AppInfoBuilderTests
{
	[SetUp]
	public void PreRun()
	{}

	[Test]
	public void Build_shall_create_a_new_AppInfo() =>
		Should.NotThrow(() => throw new NotImplementedException());

	[Test]
	public void ConfigureIdentities_shall_add_identities() =>
		Should.NotThrow(() => throw new NotImplementedException());
}


public class AppInfoBuilderExtensionsTests
{
	[SetUp]
	public void PreRun()
	{}

	[Test]
	public void ConfigureDefaults_shall_add_identities() =>
		Should.NotThrow(() => throw new NotImplementedException());

	[Test]
	public void WithIdentities_shall_add_identities() =>
		Should.NotThrow(() => throw new NotImplementedException());

	[Test]
	public void AddTimestamp_shall_add_timestamp() =>
		Should.NotThrow(() => throw new NotImplementedException());

	[Test]
	public void WriteToConsole_shall_add_console_output() =>
		Should.NotThrow(() => throw new NotImplementedException());
}
