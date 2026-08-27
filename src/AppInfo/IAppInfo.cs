using System.Globalization;


namespace AppInformation;


public interface IAppInfo
{
	CultureInfo Culture { get; }

//TODO: #32: Make Fragment internal and replace with public KVP (possibly a dictionary instead of IEnumerable)
	IEnumerable<Fragment> Fragments { get; }
}
