namespace AssetRipper.TextureDecoder.Exr;

public readonly record struct ExrSingle(float Value) : IExrDataType
{
	public static ReadOnlySpan<byte> TypeName => "float"u8;

	public int Size => sizeof(float);

	public void Write(BinaryWriter writer)
	{
		writer.Write(Value);
	}

	public static implicit operator float(ExrSingle value) => value.Value;

	public static implicit operator ExrSingle(float value) => new(value);
}
