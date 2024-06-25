using AppInfo.Fragments;


namespace AppInfo.Extractors;


public class TimestampExtractor : IExtractor
{
	internal const string TimestampLabel = "Timestamp";
	internal const string TimestampUtcLabel = "TimestampUTC";

	public IEnumerable<Fragment> Extract()
	{
		var timestamp = DateTime.Now;
		yield return new Fragment(TimestampLabel, timestamp);
		yield return new Fragment(TimestampUtcLabel, timestamp.ToUniversalTime());
	}
}
