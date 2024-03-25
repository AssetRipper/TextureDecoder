using System.Text;

namespace AssetRipper.TextureDecoder.Exr;

public readonly record struct ExrString(string Value) : IExrDataType
{
	public static ReadOnlySpan<byte> TypeName => "string"u8;

	public int Size => sizeof(int) + Encoding.UTF8.GetByteCount(Value);

	public void Write(BinaryWriter writer) => writer.WriteLengthPrefixedString(Value);

	public static implicit operator string(ExrString value) => value.Value;

	public static implicit operator ExrString(string value) => new(value);
}
