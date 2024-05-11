using Shouldly;


namespace AppInfo.Tests;


public class Tests
{
	[SetUp]
	public void PreRun()
	{}

	[Test]
	public void Test1() =>
		Should.NotThrow(() => throw new NotImplementedException());
}
