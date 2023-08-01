//This code is source generated. Do not edit manually.

using AssetRipper.TextureDecoder.Attributes;

namespace AssetRipper.TextureDecoder.Rgb.Formats
{
	[RgbaAttribute(RedChannel = true, GreenChannel = false, BlueChannel = false, AlphaChannel = false, FullyUtilizedChannels = true)]
	public partial struct ColorR8 : IColor<byte>
	{
		public byte R { get; set; }
		
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
		}
		
		static bool IColor<byte>.HasRedChannel => true;
		static bool IColor<byte>.HasGreenChannel => false;
		static bool IColor<byte>.HasBlueChannel => false;
		static bool IColor<byte>.HasAlphaChannel => false;
	}
}
