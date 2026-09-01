using System.Globalization;
using AppInformation.Extractors;


namespace AppInformation;


public interface IInputBuilder
{
	/// <summary>
	///     Sets the culture to use when building the <see cref="IAppInfo" />.
	/// </summary>
	/// <param name="cultureInfo">The culture to use when building the <see cref="IAppInfo" />.</param>
	/// <returns>The <see cref="IInputBuilder" />.</returns>
	/// <remarks>
	///     This culture is <b>only used when rendering</b> the <see cref="IAppInfo" />. It is not used anywhere else in the host
	///     application.
	/// </remarks>
	IInputBuilder UseCulture(CultureInfo cultureInfo);

	IInputBuilder AddExtractor<T>(T extractor)
		where T : IExtractor;

	IInputBuilder AddExtractor<T>()
		where T : IExtractor, new();

	IAppInfo Build();
}
