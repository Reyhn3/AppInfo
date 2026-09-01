using System.Diagnostics;
using System.Globalization;
using AppInformation.Extractors;
using AppInformation.Helpers;


namespace AppInformation;


public class AppInfoBuilder : IAppInfoBuilder
{
	private CultureInfo _culture = Constants.DefaultCulture;
	private readonly List<IExtractor> _extractors = new();

	public IAppInfoBuilder UseCulture(CultureInfo cultureInfo)
	{
		if (cultureInfo == null)
			return this;

		_culture = cultureInfo;
		return this;
	}

	public IAppInfoBuilder AddExtractor<T>(T extractor)
		where T : IExtractor
	{
		// ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
		if (extractor == null)
		{
			InternalLogger.Log("Attempted to add null extractor");
			return this;
		}

		if (_extractors.Contains(extractor))
		{
			InternalLogger.Log("Attempted to add duplicate extractor");
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
			InternalLogger.Log("Exception caught when trying to create and add extractor of {0}: {1}", typeof(T), ex);
			return this;
		}
	}

	public IAppInfo Build()
	{
//TODO: #11: Move fragment compilation to formatter class
//TODO: #11: Inject culture when formatting
//TODO: #11: Trim label and value
		var fragments = _extractors.SelectMany(SafelyExtract).ToArray();
		var appInfo = new AppInfo(_culture, fragments);
		return appInfo;
	}

	private IEnumerable<Fragment> SafelyExtract(IExtractor extractor)
	{
		try
		{
			return extractor.Extract();
		}
		catch (Exception ex)
		{
			InternalLogger.Log("Exception caught when extracting fragments from {0}: {1}", extractor, ex);
			return [];
		}
	}
}
