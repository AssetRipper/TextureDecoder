//This code is source generated. Do not edit manually.

using AssetRipper.TextureDecoder.Attributes;

namespace AssetRipper.TextureDecoder.Rgb.Formats
{
	[RgbaAttribute(RedChannel = true, GreenChannel = true, BlueChannel = false, AlphaChannel = false, FullyUtilizedChannels = true)]
	public partial struct ColorRG32Signed : IColor<short>
	{
		public short R { get; set; }
		
		public short G { get; set; }
		
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
			G = g;
		}
	}
}
