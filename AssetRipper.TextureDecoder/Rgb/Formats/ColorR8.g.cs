//This code is source generated. Do not edit manually.

using AssetRipper.TextureDecoder.Attributes;

namespace AssetRipper.TextureDecoder.Rgb.Formats
{
	[RgbaAttribute(RedChannel = true, GreenChannel = false, BlueChannel = false, AlphaChannel = false, FullyUtilizedChannels = true)]
	public partial struct ColorR8 : IColor<byte>
	{
		public byte R { get; set; }
		
		public byte G 
		{
			get => byte.MinValue;
			set { }
		}
		
		public byte B 
		{
			get => byte.MinValue;
			set { }
		}
		
		public byte A 
		{
			get => byte.MaxValue;
			set { }
		}
		
		public void GetChannels(out byte r, out byte g, out byte b, out byte a)
		{
			r = R;
			g = G;
			b = B;
			a = A;
		}
		
		public void SetChannels(byte r, byte g, byte b, byte a)
		{
			R = r;
		}
	}
}
