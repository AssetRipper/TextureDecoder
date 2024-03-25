namespace AssetRipper.TextureDecoder.Exr;

public interface IExrDataType
{
	static abstract ReadOnlySpan<byte> TypeName { get; }

	int Size { get; }

	void Write(BinaryWriter writer);
}
