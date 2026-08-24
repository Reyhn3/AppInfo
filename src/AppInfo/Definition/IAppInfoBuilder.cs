using System.Globalization;
using AppInfo.Extractors;


//TODO: Move to base namespace (and friends)
namespace AppInfo.Definition;


public interface IAppInfoBuilder
{
	IAppInfoBuilder UseCulture(CultureInfo cultureInfo);

	IAppInfoBuilder AddExtractor<T>(T extractor)
		where T : IExtractor;

	IAppInfo Build();
}
