//This code is source generated. Do not edit manually.

using AssetRipper.TextureDecoder.Attributes;

namespace AssetRipper.TextureDecoder.Rgb.Formats
{
	[RgbaAttribute(RedChannel = true, GreenChannel = true, BlueChannel = true, AlphaChannel = true, FullyUtilizedChannels = true)]
	public partial struct ColorARGB32 : IColor<byte>
	{
		static bool IColor<byte>.HasRedChannel => true;
		static bool IColor<byte>.HasGreenChannel => true;
		static bool IColor<byte>.HasBlueChannel => true;
		static bool IColor<byte>.HasAlphaChannel => true;
	}
}
