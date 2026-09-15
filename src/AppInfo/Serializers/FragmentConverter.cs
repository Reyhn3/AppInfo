using System.Text.Json;
using System.Text.Json.Serialization;
using AppInformation.Helpers;


namespace AppInformation.Serializers;


public class FragmentConverter : JsonConverter<Fragment>
{
	// ! Not implemented
	// ! Converting back from the JsonElement inside the Value-property
	// ! requires knowledge of the original type of the object, which
	// ! is not known when deserializing.
	public override Fragment? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
		null;

	public override void Write(Utf8JsonWriter writer, Fragment value, JsonSerializerOptions options)
	{
		try
		{
			writer.WriteStartObject();

			writer.WritePropertyName(nameof(Fragment.Label));
			writer.WriteStringValue(value.Label);

			writer.WritePropertyName(nameof(Fragment.Value));

			if (value.Value == null)
			{
				writer.WriteNullValue();
			}
			else
			{
				writer.WriteStartArray();

				foreach (var element in value.Value)
				{
					// Use the default converter to serialize the object
					JsonSerializer.Serialize(writer, element, options);
				}

				writer.WriteEndArray();
			}

			writer.WriteEndObject();
		}
		catch (Exception ex)
		{
			InternalLogger.Log("Exception caught when trying to serialize Fragment: {0}", ex);
		}
	}
}
