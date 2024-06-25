using AppInfo.Fragments;


namespace AppInfo.Extractors;


public interface IExtractor
{
	IEnumerable<Fragment> Extract();
}
