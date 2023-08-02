//This code is source generated. Do not edit manually.

using AssetRipper.TextureDecoder.Attributes;

namespace AssetRipper.TextureDecoder.Rgb.Formats
{
	[RgbaAttribute(RedChannel = true, GreenChannel = true, BlueChannel = true, AlphaChannel = true, FullyUtilizedChannels = false)]
	public partial struct ColorARGB16 : IColor<byte>
	{
		static bool IColor<byte>.HasRedChannel => true;
		static bool IColor<byte>.HasGreenChannel => true;
		static bool IColor<byte>.HasBlueChannel => true;
		static bool IColor<byte>.HasAlphaChannel => true;

		public override string ToString()
		{
			return $"{{ R: {R}, G: {G}, B: {B}, A: {A} }}";
		}
	}
}
