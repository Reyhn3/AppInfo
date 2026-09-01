using System.Globalization;


namespace AppInformation.Tests;


public class AppInfoTests
{
	[Test]
	public void Ctor_should_not_throw_exception_if_culture_is_null() =>
		Should.NotThrow(() => new AppInfo(null, A.CollectionOfDummy<Fragment>(1)));

	[Test]
	public void Ctor_should_not_throw_exception_if_fragments_are_null() =>
		Should.NotThrow(() => new AppInfo(A.Dummy<CultureInfo>(), null));

	[Test]
	public void Ctor_should_not_throw_exception_if_fragments_are_empty() =>
		Should.NotThrow(() => new AppInfo(A.Dummy<CultureInfo>(), Enumerable.Empty<Fragment>()));
}
