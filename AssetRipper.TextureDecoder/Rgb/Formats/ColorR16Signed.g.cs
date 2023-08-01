//This code is source generated. Do not edit manually.

using AssetRipper.TextureDecoder.Attributes;

namespace AssetRipper.TextureDecoder.Rgb.Formats
{
	[RgbaAttribute(RedChannel = true, GreenChannel = false, BlueChannel = false, AlphaChannel = false, FullyUtilizedChannels = true)]
	public partial struct ColorR16Signed : IColor<short>
	{
		public short R { get; set; }
		
		public short G 
		{
			get => short.MinValue;
			set { }
		}
		
		public short B 
		{
			get => short.MinValue;
			set { }
		}
		
		public short A 
		{
			get => short.MaxValue;
			set { }
		}
		
		public void GetChannels(out short r, out short g, out short b, out short a)
		{
			r = R;
			g = G;
			b = B;
			a = A;
		}
		
		public void SetChannels(short r, short g, short b, short a)
		{
			R = r;
		}
		
		static bool IColor<short>.HasRedChannel => true;
		static bool IColor<short>.HasGreenChannel => false;
		static bool IColor<short>.HasBlueChannel => false;
		static bool IColor<short>.HasAlphaChannel => false;
	}
}
