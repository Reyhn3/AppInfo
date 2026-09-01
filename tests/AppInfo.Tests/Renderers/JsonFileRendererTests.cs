using AppInformation.Helpers;
using AppInformation.Renderers;


namespace AppInformation.Tests.Renderers;


public class JsonFileRendererTests
{
	private JsonFileRenderer _sut;
	private IFileWriter _fileWriter;

	[SetUp]
	public void PreRun() =>
		_sut = new JsonFileRenderer(
			A.Dummy<IFileNameProvider>(),
			_fileWriter = A.Fake<IFileWriter>());

	[Test]
	public void Render_should_not_throw_exception_if_file_cannot_be_created()
	{
		A.CallTo(() => _fileWriter.WriteToFile(A<string>.Ignored, A<string?>.Ignored))
			.Throws<Exception>();

		var appInfo = AppInfo.CreateDefaultBuilder().Build();

		Should.NotThrow(() => _sut.Render(appInfo));
	}

	[Test]
	public void Render_should_write_appinfo_to_a_JSON_text_file()
	{
		var appInfo = AppInfo.CreateDefaultBuilder().Build();

		_sut.Render(appInfo);

		A.CallTo(() => _fileWriter.WriteToFile(A<string>.Ignored, A<Stream>.Ignored))
			.MustHaveHappenedOnceExactly();
	}

	[Description("This is used not as a test but a way to see the actual output")]
	[Test]
	public void Render_should_write_appinfo_string()
	{
		var appInfo = AppInfo.CreateDefaultBuilder().Build();

		var filename = Path.GetTempFileName();
		var fileNameProvider = A.Fake<IFileNameProvider>(f => f.ConfigureFake(ff =>
			A.CallTo(() => ff.GetPathAndFileName(A<string?>.Ignored))
				.Returns(filename)));

		var sut = new JsonFileRenderer(
			fileNameProvider,
			new FileWriter());
		sut.Render(appInfo);

		Console.WriteLine(filename);
		var result = File.ReadAllText(filename);
		TestHelpers.Helpers.PrintCapturedOutput(result);
	}
}
