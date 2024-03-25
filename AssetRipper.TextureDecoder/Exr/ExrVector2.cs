namespace AssetRipper.TextureDecoder.Exr;

public readonly record struct ExrVector2(Vector2 Value) : IExrDataType
{
	public ExrVector2(float x, float y) : this(new Vector2(x, y))
	{
	}

	public int Size => sizeof(float) + sizeof(float);

	public float X => Value.X;

	public float Y => Value.Y;

	public static ReadOnlySpan<byte> TypeName => "v2f"u8;

	public void Write(BinaryWriter writer)
	{
		writer.Write(X);
		writer.Write(Y);
	}

	public static implicit operator Vector2(ExrVector2 value) => value.Value;

	public static implicit operator ExrVector2(Vector2 value) => new(value);
}
