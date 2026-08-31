using System.Text;


namespace AppInformation.Helpers;


public interface IFileWriter
{
	FileInfo? WriteToFile(string pathAndFileName, string? contents);
}


public class FileWriter : IFileWriter
{
	public FileInfo? WriteToFile(string pathAndFileName, string? contents)
	{
		try
		{
			InternalLogger.Log("Writing to file '{0}'", pathAndFileName);
			File.WriteAllText(pathAndFileName, contents, Encoding.UTF8);
			return new FileInfo(pathAndFileName);
		}
		catch (Exception ex)
		{
			InternalLogger.Log("Exception caught when writing to file '{0}': {1}", pathAndFileName, ex);
			return null;
		}
	}
}
