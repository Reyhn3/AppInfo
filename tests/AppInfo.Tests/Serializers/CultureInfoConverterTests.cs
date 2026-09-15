using System.Globalization;
using AppInformation.Serializers;
using AppInformation.Tests.Helpers;


namespace AppInformation.Tests.Serializers;


public class CultureInfoConverterTests
{
	private CultureInfoConverter _sut;

	[SetUp]
	public void PreRun() =>
		_sut = new CultureInfoConverter();

	[Test]
	public void Write_should_use_only_the_culture_name() =>
		_sut.Write(CultureInfo.CreateSpecificCulture("sv-SE"))
			.ShouldBe("\"sv-SE\"");

	[Test]
	public void Read_should_covert_the_culture_name() =>
		_sut.Read("\"sv-SE\"")
			.ShouldBe(CultureInfo.CreateSpecificCulture("sv-SE"));
}
