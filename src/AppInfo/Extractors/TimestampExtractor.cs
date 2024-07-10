namespace AppInfo.Extractors;


public class TimestampExtractor : IExtractor
{
	internal const string TimestampLabel = "Timestamp";
	internal const string TimestampUtcLabel = "TimestampUTC";
	internal const string TimeZoneLabel = "TimeZone";

	public IEnumerable<Fragment> Extract()
	{
		var timestamp = DateTime.Now;
//TODO: Return local and UTC times as multi-valued fragment instead?
		yield return new Fragment(TimestampLabel, timestamp);
		yield return new Fragment(TimestampUtcLabel, timestamp.ToUniversalTime());
		yield return new Fragment(TimeZoneLabel, TimeZoneInfo.Local);
	}
}
