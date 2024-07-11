using Shouldly;


namespace AppInfo.Tests.Builders.AppInfo;


public class AppInfoBuilderTests
{
	private AppInfoBuilder _sut;

	[SetUp]
	public void PreRun() =>
		_sut = new AppInfoBuilder();

	[Test]
	public void CreateDefaultBuilder_shall_return_new_builder_with_default_configuration() =>
		AppInfoBuilder.CreateDefaultBuilder().ShouldNotBeNull();

	[Test]
	public void CreateDefaultBuilder_shall_set_default_output() =>
		AppInfoBuilder.CreateDefaultBuilder().Output.ShouldNotBeNull();

	[Test]
	public void CreateDefaultBuilder_shall_set_default_extractors() =>
		((AppInfoBuilder)AppInfoBuilder.CreateDefaultBuilder()).Extractors.ShouldNotBeNull();

	[Test]
	public void Build_shall_create_an_instance_of_AppInfo() =>
		AppInfoBuilder.CreateDefaultBuilder().Build().ShouldNotBeNull().ShouldBeOfType<global::AppInfo.AppInfo>();
}
