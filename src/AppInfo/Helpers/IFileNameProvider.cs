namespace AppInformation.Helpers;


public interface IFileNameProvider
{
	string GetPathAndFileName(string? extension);
}


public class TempFileNameProvider : IFileNameProvider
{
	public string GetPathAndFileName(string? extension)
	{
		// Make sure to use null (instead of empty strings)
		// to avoid trailing dots
		var trimmed = extension?.Trim();
		if (string.IsNullOrWhiteSpace(trimmed))
			return Path.ChangeExtension(Path.GetTempFileName(), null);

		return Path.ChangeExtension(Path.GetTempFileName(), trimmed);
	}
}
