//This code is source generated. Do not edit manually.

using AssetRipper.TextureDecoder.Attributes;

namespace AssetRipper.TextureDecoder.Rgb.Formats
{
	[RgbaAttribute(RedChannel = true, GreenChannel = true, BlueChannel = true, AlphaChannel = false, FullyUtilizedChannels = false)]
	public partial struct ColorRGB9e5 : IColor<double>
	{
		static bool IColor<double>.HasRedChannel => true;
		static bool IColor<double>.HasGreenChannel => true;
		static bool IColor<double>.HasBlueChannel => true;
		static bool IColor<double>.HasAlphaChannel => false;

		public override string ToString()
		{
			return $"{{ R: {R}, G: {G}, B: {B} }}";
		}
	}
}
