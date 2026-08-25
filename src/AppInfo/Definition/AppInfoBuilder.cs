using System.Diagnostics;
using System.Globalization;
using AppInfo.Extractors;


namespace AppInfo;


public class AppInfoBuilder : IAppInfoBuilder
{
	private CultureInfo _culture = CultureInfo.CurrentUICulture;
	private readonly List<IExtractor> _extractors = new();

	public IAppInfoBuilder UseCulture(CultureInfo cultureInfo)
	{
		_culture = cultureInfo;
		return this;
	}

	public IAppInfoBuilder AddExtractor<T>(T extractor)
		where T : IExtractor
	{
		// ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
		if (extractor == null)
		{
			Debug.WriteLine("Attempted to add null extractor");
			return this;
		}

		if (_extractors.Contains(extractor))
		{
			Debug.WriteLine("Attempted to add duplicate extractor");
			return this;
		}

		_extractors.Add(extractor);
		return this;
	}

	public IAppInfoBuilder AddExtractor<T>()
		where T : IExtractor, new()
	{
		try
		{
			var extractor = new T();
			return AddExtractor(extractor);
		}
		catch (Exception ex)
		{
			Debug.WriteLine($"Exception caught when trying to create and add extractor of {typeof(T)}: {ex}");
			return this;
		}
	}

	public IAppInfo Build()
	{
//TODO: try-catch
		var fragments = _extractors.SelectMany(e => e.Extract()).ToArray();
//TODO: #11: Move fragment compilation to formatter class
//TODO: #11: Inject culture when formatting
//TODO: #11: Trim label and value
		var appInfo = new AppInfo(_culture, fragments);
		return appInfo;
	}
}
