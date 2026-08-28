using System.Globalization;
using AppInformation.Extractors;


namespace AppInformation;


public interface IAppInfoBuilder
{
	/// <summary>
	///     Sets the culture to use when building the <see cref="IAppInfo" />.
	/// </summary>
	/// <param name="cultureInfo">The culture to use when building the <see cref="IAppInfo" />.</param>
	/// <returns>The <see cref="IAppInfoBuilder" />.</returns>
	/// <remarks>
	///     This culture is <b>only used when rendering</b> the <see cref="IAppInfo" />. It is not used anywhere else in the host
	///     application.
	/// </remarks>
	IAppInfoBuilder UseCulture(CultureInfo cultureInfo);

	IAppInfoBuilder AddExtractor<T>(T extractor)
		where T : IExtractor;

	IAppInfoBuilder AddExtractor<T>()
		where T : IExtractor, new();

	IAppInfo Build();
}
