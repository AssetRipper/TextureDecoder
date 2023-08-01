//This code is source generated. Do not edit manually.

using AssetRipper.TextureDecoder.Attributes;

namespace AssetRipper.TextureDecoder.Rgb.Formats
{
	[RgbaAttribute(RedChannel = true, GreenChannel = true, BlueChannel = true, AlphaChannel = false, FullyUtilizedChannels = false)]
	public partial struct ColorRGB32Half : IColor<Half>
	{
		static bool IColor<Half>.HasRedChannel => true;
		static bool IColor<Half>.HasGreenChannel => true;
		static bool IColor<Half>.HasBlueChannel => true;
		static bool IColor<Half>.HasAlphaChannel => false;
	}
}
