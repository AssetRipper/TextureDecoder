//This code is source generated. Do not edit manually.

using AssetRipper.TextureDecoder.Attributes;

namespace AssetRipper.TextureDecoder.Rgb.Formats
{
	[RgbaAttribute(RedChannel = true, GreenChannel = true, BlueChannel = true, AlphaChannel = true, FullyUtilizedChannels = true)]
	public partial struct ColorRGBA64Signed : IColor<short>
	{
		public short R { get; set; }
		
		public short G { get; set; }
		
		public short B { get; set; }
		
		public short A { get; set; }
		
		public void GetChannels(out short r, out short g, out short b, out short a)
		{
			DefaultColorMethods.GetChannels(this, out r, out g, out b, out a);
		}
		
		public void SetChannels(short r, short g, short b, short a)
		{
			DefaultColorMethods.SetChannels(ref this, r, g, b, a);
		}
	}
}