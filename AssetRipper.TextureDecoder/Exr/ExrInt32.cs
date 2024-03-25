namespace AssetRipper.TextureDecoder.Exr;

public readonly record struct ExrInt32(int Value) : IExrDataType
{
	public static ReadOnlySpan<byte> TypeName => "int"u8;

	public int Size => sizeof(int);

	public void Write(BinaryWriter writer)
	{
		writer.Write(Value);
	}

	public static implicit operator int(ExrInt32 value) => value.Value;

	public static implicit operator ExrInt32(int value) => new(value);
}
