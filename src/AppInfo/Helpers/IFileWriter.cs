using System.Text;


namespace AppInformation.Helpers;


public interface IFileWriter
{
	FileInfo? WriteToFile(string pathAndFileName, string? contents);
	FileInfo? WriteToFile(string pathAndFileName, Stream contents);
}


public class FileWriter : IFileWriter
{
	public FileInfo? WriteToFile(string pathAndFileName, string? contents)
	{
		try
		{
			InternalLogger.Log("Writing string to file '{0}'", pathAndFileName);
			File.WriteAllText(pathAndFileName, contents, Encoding.UTF8);
			return new FileInfo(pathAndFileName);
		}
		catch (Exception ex)
		{
			InternalLogger.Log("Exception caught when writing string to file '{0}': {1}", pathAndFileName, ex);
			return null;
		}
	}

	public FileInfo? WriteToFile(string pathAndFileName, Stream contents)
	{
		try
		{
			InternalLogger.Log("Writing stream to file '{0}'", pathAndFileName);
			using var fileStream = File.OpenWrite(pathAndFileName);
			contents.Seek(0, SeekOrigin.Begin);
			contents.CopyTo(fileStream);
			return new FileInfo(pathAndFileName);
		}
		catch (Exception ex)
		{
			InternalLogger.Log("Exception caught when writing stream to file '{0}': {1}", pathAndFileName, ex);
			return null;
		}
	}
}
