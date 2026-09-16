using System.Text.Json;
using System.Text.Json.Serialization;
using AppInformation.Helpers;


namespace AppInformation.Serializers;


public class AppInfoConverter : JsonConverter<AppInfo>
{
	// ! Not implemented
	// ! The Fragment class requires information not available.
	public override AppInfo? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var startDepth = reader.CurrentDepth;

		// This looks weird, but apparently the Read-method must leave the reader
		// positioned on the EndObject token of the object where it was
		// originally positioned. So, read and ignore.
		while (reader.Read())
		{
			if (reader.TokenType == JsonTokenType.EndObject && reader.CurrentDepth == startDepth)
				return null;
		}

		return null;
	}

	public override void Write(Utf8JsonWriter writer, AppInfo value, JsonSerializerOptions options)
	{
		try
		{
			writer.WriteStartArray();

			foreach (var fragment in value.Fragments)
			{
				JsonSerializer.Serialize(writer, fragment, options);
			}

			writer.WriteEndArray();
		}
		catch (Exception ex)
		{
			InternalLogger.Log("Exception caught when trying to serialize AppInfo: {0}", ex);
		}
	}
}
