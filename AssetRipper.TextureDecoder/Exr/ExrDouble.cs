namespace AssetRipper.TextureDecoder.Exr;

public readonly record struct ExrDouble(double Value) : IExrDataType
{
	public static ReadOnlySpan<byte> TypeName => "double"u8;

	public int Size => sizeof(double);

	public void Write(BinaryWriter writer)
	{
		writer.Write(Value);
	}

	public static implicit operator double(ExrDouble value) => value.Value;

	public static implicit operator ExrDouble(double value) => new(value);
}
