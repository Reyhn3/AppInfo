namespace AppInfo.Extractors;


public class TimestampExtractor : Extractor
{
	internal const string TimestampLabel = "Timestamp";
	internal const string TimestampUtcLabel = "TimestampUTC";
	internal const string TimeZoneLabel = "TimeZone";

	protected override IEnumerable<Func<Fragment>> ProduceExtractors()
	{
		var timestamp = DateTime.Now;
//TODO: Return local and UTC times as multi-valued fragment instead?
		yield return () => new Fragment(TimestampLabel, timestamp);
		yield return () => new Fragment(TimestampUtcLabel, timestamp.ToUniversalTime());
		yield return () => new Fragment(TimeZoneLabel, TimeZoneInfo.Local);
	}
}
