using System.Text;
using AppInformation.Helpers;


namespace AppInformation.Tests.Helpers;


public class FileWriterTests
{
	private FileWriter _sut;

	[SetUp]
	public void PreRun() =>
		_sut = new FileWriter();

	[Test]
	public void WriteToFile_shall_not_throw_exception_on_errors() =>
		Should.NotThrow(() => _sut.WriteToFile(Path.GetInvalidFileNameChars()[0].ToString(), "test"));

	[Test]
	public void WriteToFile_shall_write_the_string_content_to_the_specified_file()
	{
		var pathAndFileName = Path.GetTempFileName();
		var result = _sut.WriteToFile(pathAndFileName, "test");

		result.ShouldNotBeNull();
		Console.WriteLine(result);

		File.Exists(pathAndFileName).ShouldBeTrue();
		File.ReadAllText(pathAndFileName).ShouldBe("test");
	}

	[Test]
	public void WriteToFile_shall_write_the_stream_content_to_the_specified_file()
	{
		var pathAndFileName = Path.GetTempFileName();
		using var stream = new MemoryStream();
		stream.Write("{\"test\"}"u8);

		var result = _sut.WriteToFile(pathAndFileName, stream);

		result.ShouldNotBeNull();
		Console.WriteLine(result);

		File.Exists(pathAndFileName).ShouldBeTrue();
		File.ReadAllText(pathAndFileName).ShouldBe("{\"test\"}");
	}
}
