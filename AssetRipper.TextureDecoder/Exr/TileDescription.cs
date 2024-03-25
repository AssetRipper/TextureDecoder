namespace AssetRipper.TextureDecoder.Exr;

public readonly record struct TileDescription(int XSize, int YSize, ExrLevelMode Mode) : IExrDataType
{
	public static ReadOnlySpan<byte> TypeName => "tileDesc"u8;

	public int Size => sizeof(int) + sizeof(int) + sizeof(ExrLevelMode);

	public void Write(BinaryWriter writer)
	{
		writer.Write(XSize);
		writer.Write(YSize);
		writer.Write(Mode);
	}
}
