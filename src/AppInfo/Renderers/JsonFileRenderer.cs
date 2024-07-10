using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace AppInfo.Renderers;


public class JsonFileRenderer : Renderer
{
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
		var path = Path.ChangeExtension(Path.GetTempFileName(), "json");

		using var stream = File.Create(path);
		JsonSerializer.Serialize(stream, container, s_options);
		Debug.WriteLine($"JSON file written to {path}", Constants.LibraryName);

//TODO: Remove this
		Console.WriteLine("JSON file: {0}", path);
	}

//TODO: Replace the object type with a structured type
	private object BuildContainer(IAppInfo info) =>
		info.Fragments.ToArray();
}
