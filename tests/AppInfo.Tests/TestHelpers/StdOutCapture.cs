using System.Text;


namespace AppInformation.Tests.TestHelpers;


public class StdOutCapture : TextWriter
{
	private readonly TextWriter _stdOutWriter;

	public StdOutCapture()
	{
		_stdOutWriter = Console.Out;
		Console.SetOut(this);
		Captured = new StringWriter();
	}

	public TextWriter Captured { get; private set; }
	public override Encoding Encoding => Encoding.UTF8;

	public override void Write(string? output)
	{
		// Capture the output and also send it to StdOut
		Captured.Write(output);
		_stdOutWriter.Write(output);
	}

	public override void WriteLine(string? output)
	{
		// Capture the output and also send it to StdOut
		Captured.WriteLine(output);
		_stdOutWriter.WriteLine(output);
	}

	protected override void Dispose(bool disposing)
	{
		Console.SetOut(_stdOutWriter);
		base.Dispose(disposing);
	}
}
