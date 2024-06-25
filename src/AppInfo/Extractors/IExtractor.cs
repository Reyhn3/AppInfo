namespace AppInfo.Extractors;


public interface IExtractor
{
	IEnumerable<Fragment> Extract();
}
