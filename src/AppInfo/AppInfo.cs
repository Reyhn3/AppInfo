using System.Collections.Immutable;
using System.Globalization;
using System.Text.Json.Serialization;
using AppInformation.Helpers;
using AppInformation.Serializers;


namespace AppInformation;


[JsonConverter(typeof(AppInfoConverter))]
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
			InternalLogger.LogIf(immutable.Length == 0, "Warning: No fragments received");
			return immutable;
		}
		catch (Exception ex)
		{
			InternalLogger.Log("Unexpected exception when converting fragments to ImmutableArray: {0}", ex);
			return [];
		}
	}

	/// <summary>
	///     This property is only used for rendering purposes. It is <b>not</b> used by the host.
	/// </summary>
	[JsonConverter(typeof(CultureInfoConverter))]
	public CultureInfo Culture { get; }

	public IEnumerable<Fragment> Fragments => _fragments.AsEnumerable();
}
