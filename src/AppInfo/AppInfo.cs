using System.Collections.Immutable;
using System.Globalization;


namespace AppInformation;


public partial class AppInfo : IAppInfo
{
	private readonly ImmutableArray<Fragment> _fragments;

	internal AppInfo(CultureInfo culture, IEnumerable<Fragment> fragments)
	{
		if (fragments == null)
			throw new ArgumentNullException(nameof(fragments));

		Culture = culture;
		_fragments = [.. fragments];
	}

	public CultureInfo Culture { get; }
	public IEnumerable<Fragment> Fragments => _fragments.AsEnumerable();
}
