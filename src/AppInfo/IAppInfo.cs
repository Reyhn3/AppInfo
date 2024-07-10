namespace AppInfo;


public interface IAppInfo
{
//TODO: Make Fragment internal and replace with public KVP (possibly a dictionary instead of IEnumerable)
	IEnumerable<Fragment> Fragments { get; }
}
