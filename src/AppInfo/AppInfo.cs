using System.Collections.Immutable;


namespace AppInfo;


public sealed class AppInfo : IAppInfo
{
	private readonly ImmutableArray<Fragment> _fragments;

	internal AppInfo(IEnumerable<Fragment> fragments)
	{
		if (fragments == null)
			throw new ArgumentNullException(nameof(fragments));

		_fragments = [..fragments];
	}

	public IEnumerable<Fragment> Fragments => _fragments.AsEnumerable();
}
