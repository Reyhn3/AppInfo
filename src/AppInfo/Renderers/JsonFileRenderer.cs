using System.Text.Json;
using System.Text.Json.Serialization;
using AppInformation.Helpers;


namespace AppInformation.Renderers;


public class JsonFileRenderer(IFileNameProvider fileNameProvider, IFileWriter fileWriter)
	: Renderer
{
//TODO: This should be configurable
	private static readonly JsonSerializerOptions s_options = new()
		{
			WriteIndented = true,
			Converters =
				{
					new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
				}
		};

	protected override void RenderAppInfo(IAppInfo info)
	{
		var container = BuildContainer(info);

//TODO: Generate unique file name, or append to existing file
		var path = fileNameProvider.GetPathAndFileName("json");

		using var stream = new MemoryStream();
		JsonSerializer.Serialize(stream, container, s_options);

		var file = fileWriter.WriteToFile(path, stream);
		InternalLogger.Log("JSON file written to {0}", file);
	}

//TODO: Replace the object type with a public structured contract type
	private object BuildContainer(IAppInfo info) =>
		info.Fragments.ToArray();
}
