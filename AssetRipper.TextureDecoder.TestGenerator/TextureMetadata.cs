using System.Text.Json;
using System.Text.Json.Serialization;

namespace AssetRipper.TextureDecoder.TestGenerator;

public sealed class TextureMetadata
{
	[JsonIgnore]
	public string? Name { get; set; }
	public string? Type { get; set; }
	public string? Format { get; set; }
	public int FileSize { get; set; }
	public int ImageSize { get; set; }
	public int ImageCount { get; set; }
	public bool Mips { get; set; }
	public int Width { get; set; }
	public int Height { get; set; }

	public static TextureMetadata FromFile(string path)
	{
		using FileStream stream = File.OpenRead(path);
		TextureMetadata result = JsonSerializer.Deserialize(stream, TextureMetadataSerializerContext.Default.TextureMetadata)
			?? throw new NullReferenceException(nameof(TextureMetadata));
		result.Name = Path.GetFileNameWithoutExtension(path);
		return result;
	}
}
