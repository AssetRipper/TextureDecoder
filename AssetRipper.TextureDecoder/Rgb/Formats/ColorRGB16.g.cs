// Auto-generated code. Do not modify manually.

namespace AssetRipper.TextureDecoder.Rgb.Formats
{
	public partial struct ColorRGB16 : IColor<byte>
	{
		static bool IColorBase.HasRedChannel => true;
		static bool IColorBase.HasGreenChannel => true;
		static bool IColorBase.HasBlueChannel => true;
		static bool IColorBase.HasAlphaChannel => false;
		static bool IColorBase.ChannelsAreFullyUtilized => false;
		static Type IColorBase.ChannelType => typeof(byte);

		public override string ToString()
		{
			return $"{{ R: {R}, G: {G}, B: {B} }}";
		}
	}
}
