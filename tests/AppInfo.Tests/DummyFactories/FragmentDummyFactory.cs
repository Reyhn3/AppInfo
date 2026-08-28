namespace AppInformation.Tests.DummyFactories;


// ReSharper disable once UnusedType.Global
public class FragmentDummyFactory : DummyFactory<Fragment>
{
	protected override Fragment Create() =>
		new("test", "test-value");
}
