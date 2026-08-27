using System.Globalization;
using AppInformation.Extractors;


namespace AppInformation;


public interface IAppInfoBuilder
{
	IAppInfoBuilder UseCulture(CultureInfo cultureInfo);

	IAppInfoBuilder AddExtractor<T>(T extractor)
		where T : IExtractor;

	IAppInfoBuilder AddExtractor<T>()
		where T : IExtractor, new();

	IAppInfo Build();
}
