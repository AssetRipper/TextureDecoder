namespace AssetRipper.TextureDecoder.Exr;

public readonly record struct ExrBox2I(int XMin, int YMin, int XMax, int YMax) : IExrDataType
{
	public static ReadOnlySpan<byte> TypeName => "box2i"u8;

	public int Size => sizeof(int) + sizeof(int) + sizeof(int) + sizeof(int);

	public void Write(BinaryWriter writer)
	{
		writer.Write(XMin);
		writer.Write(YMin);
		writer.Write(XMax);
		writer.Write(YMax);
	}
}
