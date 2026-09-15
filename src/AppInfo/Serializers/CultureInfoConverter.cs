using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using AppInformation.Helpers;


namespace AppInformation.Serializers;


public class CultureInfoConverter : JsonConverter<CultureInfo>
{
	public override CultureInfo? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		try
		{
			var value = reader.GetString();
			if (string.IsNullOrWhiteSpace(value))
			{
				InternalLogger.Log("Empty value detected when deserializing CultureInfo");
				return Constants.DefaultCulture;
			}

			return CultureInfo.CreateSpecificCulture(value);
		}
		catch (Exception ex)
		{
			InternalLogger.Log("Exception caught when trying to deserialize CultureInfo: {0}", ex);
			return Constants.DefaultCulture;
		}
	}

	public override void Write(Utf8JsonWriter writer, CultureInfo value, JsonSerializerOptions options)
	{
		try
		{
			writer.WriteStringValue(value.Name);
		}
		catch (Exception ex)
		{
			InternalLogger.Log("Exception caught when trying to serialize CultureInfo: {0}", ex);
		}
	}
}
