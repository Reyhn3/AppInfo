using System.Globalization;


namespace AppInfo;


public interface IAppInfo
{
	CultureInfo Culture { get; }

//TODO: #32: Make Fragment internal and replace with public KVP (possibly a dictionary instead of IEnumerable)
	IEnumerable<Fragment> Fragments { get; }
}
