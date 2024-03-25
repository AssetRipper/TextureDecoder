namespace AssetRipper.TextureDecoder.Exr;
public enum ExrImageFormat
{
	ScanLines,
	Tiles,
}
public static class ExrImageFormatExtensions
{
	public static byte ToVersionByte(this ExrImageFormat format) => format switch
	{
		ExrImageFormat.Tiles => 0x02,
		_ => 0x00,
	};
}
