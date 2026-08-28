using System.Collections.Immutable;
using System.Diagnostics;
using System.Globalization;


namespace AppInformation;


public partial class AppInfo : IAppInfo
{
	private readonly ImmutableArray<Fragment> _fragments;

	internal AppInfo(CultureInfo culture, IEnumerable<Fragment> fragments)
	{
		Culture = culture ?? Constants.DefaultCulture;
		_fragments = ToSafeImmutableArray(fragments);
	}

	private static ImmutableArray<Fragment> ToSafeImmutableArray(IEnumerable<Fragment> fragments)
	{
		try
		{
			var immutable = fragments?.ToImmutableArray() ?? [];
			Debug.WriteLineIf(immutable.Length == 0, "Warning: No fragments received");
			return immutable;
		}
		catch (Exception ex)
		{
			Debug.WriteLine($"Unexpected exception when converting fragments to ImmutableArray: {ex}");
			return [];
		}
	}

	public CultureInfo Culture { get; }
	public IEnumerable<Fragment> Fragments => _fragments.AsEnumerable();
}
