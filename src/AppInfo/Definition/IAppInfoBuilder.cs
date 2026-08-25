using System.Globalization;
using AppInfo.Extractors;


namespace AppInfo;


public interface IAppInfoBuilder
{
	IAppInfoBuilder UseCulture(CultureInfo cultureInfo);

	IAppInfoBuilder AddExtractor<T>(T extractor)
		where T : IExtractor;

	IAppInfo Build();
}
