//This code is source generated. Do not edit manually.

using AssetRipper.TextureDecoder.Attributes;

namespace AssetRipper.TextureDecoder.Rgb.Formats
{
	[RgbaAttribute(RedChannel = true, GreenChannel = true, BlueChannel = true, AlphaChannel = false, FullyUtilizedChannels = true)]
	public partial struct ColorRGB48Signed : IColor<short>
	{
		public short R { get; set; }
		
		public short G { get; set; }
		
		public short B { get; set; }
		
		public readonly short A 
		{
			get => short.MaxValue;
			set { }
		}
		
		public readonly void GetChannels(out short r, out short g, out short b, out short a)
		{
			r = R;
			g = G;
			b = B;
			a = A;
		}
		
		public void SetChannels(short r, short g, short b, short a)
		{
			R = r;
			G = g;
			B = b;
		}
		
		static bool IColor<short>.HasRedChannel => true;
		static bool IColor<short>.HasGreenChannel => true;
		static bool IColor<short>.HasBlueChannel => true;
		static bool IColor<short>.HasAlphaChannel => false;
	}
}
