using Shouldly;


namespace AppInfo.Tests.Builders.AppOutput;


public class AppInfoOutputBuilderTests
{
	private AppInfoOutputBuilder _sut;

	[SetUp]
	public void PreRun() =>
		_sut = new AppInfoOutputBuilder();

	[Test]
	public void Build_shall_create_an_output_instance() =>
		_sut.Build().ShouldNotBeNull().ShouldBeOfType<global::AppInfo.AppOutput>();
}
