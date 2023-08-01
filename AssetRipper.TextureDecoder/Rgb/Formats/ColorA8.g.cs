//This code is source generated. Do not edit manually.

using AssetRipper.TextureDecoder.Attributes;

namespace AssetRipper.TextureDecoder.Rgb.Formats
{
	[RgbaAttribute(RedChannel = false, GreenChannel = false, BlueChannel = false, AlphaChannel = true, FullyUtilizedChannels = true)]
	public partial struct ColorA8 : IColor<byte>
	{
		public readonly byte R 
		{
			get => byte.MinValue;
			set { }
		}
		
		public readonly byte G 
		{
			get => byte.MinValue;
			set { }
		}
		
		public readonly byte B 
		{
			get => byte.MinValue;
			set { }
		}
		
		public byte A { get; set; }
		
		public readonly void GetChannels(out byte r, out byte g, out byte b, out byte a)
		{
			r = R;
			g = G;
			b = B;
			a = A;
		}
		
		public void SetChannels(byte r, byte g, byte b, byte a)
		{
			A = a;
		}
		
		static bool IColor<byte>.HasRedChannel => false;
		static bool IColor<byte>.HasGreenChannel => false;
		static bool IColor<byte>.HasBlueChannel => false;
		static bool IColor<byte>.HasAlphaChannel => true;
	}
}
