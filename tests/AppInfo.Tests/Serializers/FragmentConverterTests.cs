using AppInformation.Serializers;
using AppInformation.Tests.Helpers;


namespace AppInformation.Tests.Serializers;


public class FragmentConverterTests
{
	private FragmentConverter _sut;

	[SetUp]
	public void PreRun() =>
		_sut = new FragmentConverter();

	[Test]
	public void Write_should_write_null_value() =>
		_sut.Write(new Fragment("test", null))
			.ShouldNotBeNull()
			.ShouldBe("{\"Label\":\"test\",\"Value\":null}");

	[Test]
	public void Write_should_write_empty_value() =>
		_sut.Write(new Fragment("test", []))
			.ShouldNotBeNull()
			.ShouldBe("{\"Label\":\"test\",\"Value\":[]}");

	[Test]
	public void Write_should_write_scalar_value() =>
		_sut.Write(new Fragment("test", "test-value"))
			.ShouldNotBeNull()
			.ShouldBe("{\"Label\":\"test\",\"Value\":[\"test-value\"]}");

	[Test]
	public void Write_should_write_vector_value() =>
		_sut.Write(new Fragment("test", "test-value-1", "test-value-2"))
			.ShouldNotBeNull()
			.ShouldBe("{\"Label\":\"test\",\"Value\":[\"test-value-1\",\"test-value-2\"]}");

	[Test]
	public void Write_should_write_string_value() =>
		_sut.Write(new Fragment("test", "test-value"))
			.ShouldNotBeNull()
			.ShouldBe("{\"Label\":\"test\",\"Value\":[\"test-value\"]}");

	[Test]
	public void Write_should_write_boolean_value() =>
		_sut.Write(new Fragment("test", true))
			.ShouldNotBeNull()
			.ShouldBe("{\"Label\":\"test\",\"Value\":[true]}");

	[Test]
	public void Write_should_write_integer_value() =>
		_sut.Write(new Fragment("test", int.MaxValue))
			.ShouldNotBeNull()
			.ShouldBe("{\"Label\":\"test\",\"Value\":[2147483647]}");

	[Test]
	public void Write_should_write_decimal_value() =>
		_sut.Write(new Fragment("test", 1234.5678m))
			.ShouldNotBeNull()
			.ShouldBe("{\"Label\":\"test\",\"Value\":[1234.5678]}");

	[Test]
	public void Read_should_return_null() =>
		_sut.Read("{\"Label\":\"test\",\"Value\":null}")
			.ShouldBeNull();
}
