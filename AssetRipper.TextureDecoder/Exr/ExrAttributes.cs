namespace AssetRipper.TextureDecoder.Exr;

public static class ExrAttributes
{
	public static ReadOnlySpan<byte> Channels => "channels"u8;

	public static ReadOnlySpan<byte> Compression => "compression"u8;

	public static ReadOnlySpan<byte> DataWindow => "dataWindow"u8;

	public static ReadOnlySpan<byte> DisplayWindow => "displayWindow"u8;

	public static ReadOnlySpan<byte> LineOrder => "lineOrder"u8;

	public static ReadOnlySpan<byte> PixelAspectRatio => "pixelAspectRatio"u8;

	public static ReadOnlySpan<byte> ScreenWindowCenter => "screenWindowCenter"u8;

	public static ReadOnlySpan<byte> ScreenWindowWidth => "screenWindowWidth"u8;

	public static ReadOnlySpan<byte> Tiles => "tiles"u8;
}
