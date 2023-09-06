using System.Text.Json.Serialization;

namespace AssetRipper.TextureDecoder.TestGenerator;

[JsonSourceGenerationOptions(GenerationMode = JsonSourceGenerationMode.Metadata)]
[JsonSerializable(typeof(TextureMetadata))]
internal sealed partial class TextureMetadataSerializerContext : JsonSerializerContext
{
}
