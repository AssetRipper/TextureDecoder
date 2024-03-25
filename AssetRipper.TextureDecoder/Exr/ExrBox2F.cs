namespace AssetRipper.TextureDecoder.Exr;

public readonly record struct ExrBox2F(float XMin, float YMin, float XMax, float YMax) : IExrDataType
{
	public static ReadOnlySpan<byte> TypeName => "box2f"u8;

	public int Size => sizeof(float) + sizeof(float) + sizeof(float) + sizeof(float);

	public void Write(BinaryWriter writer)
	{
		writer.Write(XMin);
		writer.Write(YMin);
		writer.Write(XMax);
		writer.Write(YMax);
	}
}
