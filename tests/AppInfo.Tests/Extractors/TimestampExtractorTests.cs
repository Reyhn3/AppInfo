using AppInfo.Extractors;
using Shouldly;


namespace AppInfo.Tests.Extractors;


public class TimestampExtractorTests
{
	[Test]
	public void Extract_shall_yield_timestamp()
	{
		var sut = new TimestampExtractor();


		var result = sut.Extract().ToArray();


		result.ShouldNotBeNull();
		result.ShouldNotBeEmpty();
		Helpers.PrintFragments(result);
		result.ShouldContain(f => string.Equals(TimestampExtractor.TimestampLabel, f.Label)
			&& f.Value.Single() is DateTime);
	}

	[Test]
	public void Extract_shall_yield_timestamp_UTC()
	{
		var sut = new TimestampExtractor();


		var result = sut.Extract().ToArray();


		result.ShouldNotBeNull();
		result.ShouldNotBeEmpty();
		Helpers.PrintFragments(result);
		result.ShouldContain(f => string.Equals(TimestampExtractor.TimestampUtcLabel, f.Label)
			&& f.Value.Single() is DateTime);
	}

	[Test]
	public void Extract_shall_yield_time_zone_info()
	{
		var sut = new TimestampExtractor();


		var result = sut.Extract().ToArray();


		result.ShouldNotBeNull();
		result.ShouldNotBeEmpty();
		Helpers.PrintFragments(result);
		result.ShouldContain(f => string.Equals(TimestampExtractor.TimeZoneLabel, f.Label)
			&& f.Value.Single() is TimeZoneInfo);
	}
}
