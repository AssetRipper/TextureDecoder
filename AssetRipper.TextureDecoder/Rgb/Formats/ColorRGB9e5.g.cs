// Auto-generated code. Do not modify manually.

namespace AssetRipper.TextureDecoder.Rgb.Formats
{
	public partial struct ColorRGB9e5 : IColor<double>
	{
		static bool IColor.HasRedChannel => true;
		static bool IColor.HasGreenChannel => true;
		static bool IColor.HasBlueChannel => true;
		static bool IColor.HasAlphaChannel => false;
		static bool IColor.ChannelsAreFullyUtilized => false;
		static Type IColor.ChannelType => typeof(double);

		public override string ToString()
		{
			return $"{{ R: {R}, G: {G}, B: {B} }}";
		}
	}
}
