using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace AppInformation.Tests.Helpers;


public static class JsonConverterExtensions
{
	extension<T>(JsonConverter<T> converter)
	{
		public T? Read(string value, JsonSerializerOptions? options = null)
		{
			options ??= JsonSerializerOptions.Default;

			var bytes = Encoding.UTF8.GetBytes(value);
			var reader = new Utf8JsonReader(bytes);
			reader.Read();

			var result = converter.Read(ref reader, typeof(T), options);
			return result;
		}

		public string Write(T value, JsonSerializerOptions? options = null)
		{
			options ??= JsonSerializerOptions.Default;

			using var stream = new MemoryStream();
			using var writer = new Utf8JsonWriter(stream);

			converter.Write(writer, value, options);
			writer.Flush();

			var result = Encoding.UTF8.GetString(stream.ToArray());
			return result;
		}
	}
}
