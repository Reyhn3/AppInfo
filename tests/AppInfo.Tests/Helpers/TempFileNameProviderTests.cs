using AppInformation.Helpers;


namespace AppInformation.Tests.Helpers;


public class TempFileNameProviderTests
{
	private TempFileNameProvider _sut;

	[SetUp]
	public void PreRun() =>
		_sut = new TempFileNameProvider();

	[Test]
	public void GetFileName_should_provide_a_valid_file_name()
	{
		var result = _sut.GetPathAndFileName("txt");

		result.ShouldNotBeEmpty();
		Console.WriteLine(result);

		var path = Path.GetDirectoryName(result);
		var filename = Path.GetFileName(result);
		Path.GetInvalidPathChars().ShouldAllBe(c => !path.Contains(c));
		Path.GetInvalidFileNameChars().ShouldAllBe(c => !filename.Contains(c));
	}

	[Test]
	public void GetFileName_should_set_the_specified_file_extension_if_not_empty()
	{
		var result = _sut.GetPathAndFileName("txt");
		Console.WriteLine(result);
		Path.GetExtension(result).ShouldBe(".txt");
	}

	[TestCase(null)]
	[TestCase("")]
	[TestCase("\t")]
	public void GetFileName_should_not_add_file_extension_if_extension_is_empty(string? extension)
	{
		var result = _sut.GetPathAndFileName(extension);
		Console.WriteLine(result);
		Path.GetExtension(result).ShouldBeEmpty();
		Path.GetFileName(result)[^1].ShouldNotBe('.');
	}
}
