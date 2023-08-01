//This code is source generated. Do not edit manually.

using AssetRipper.TextureDecoder.Attributes;

namespace AssetRipper.TextureDecoder.Rgb.Formats
{
	[RgbaAttribute(RedChannel = true, GreenChannel = true, BlueChannel = true, AlphaChannel = false, FullyUtilizedChannels = true)]
	public partial struct ColorRGB24 : IColor<byte>
	{
		public byte R { get; set; }
		
		public byte G { get; set; }
		
		public byte B { get; set; }
		
		public readonly byte A 
		{
			get => byte.MaxValue;
			set { }
		}
		
		public readonly void GetChannels(out byte r, out byte g, out byte b, out byte a)
		{
			r = R;
			g = G;
			b = B;
			a = A;
		}
		
		public void SetChannels(byte r, byte g, byte b, byte a)
		{
			R = r;
			G = g;
			B = b;
		}
		
		static bool IColor<byte>.HasRedChannel => true;
		static bool IColor<byte>.HasGreenChannel => true;
		static bool IColor<byte>.HasBlueChannel => true;
		static bool IColor<byte>.HasAlphaChannel => false;
	}
}
