namespace AppInformation.Extractors;


public interface IExtractor
{
	IEnumerable<Fragment> Extract();
}
