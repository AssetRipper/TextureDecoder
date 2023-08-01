//This code is source generated. Do not edit manually.

using AssetRipper.TextureDecoder.Attributes;

namespace AssetRipper.TextureDecoder.Rgb.Formats
{
	[RgbaAttribute(RedChannel = true, GreenChannel = true, BlueChannel = true, AlphaChannel = false, FullyUtilizedChannels = true)]
	public partial struct ColorRGB24Signed : IColor<sbyte>
	{
		public sbyte R { get; set; }
		
		public sbyte G { get; set; }
		
		public sbyte B { get; set; }
		
		public readonly sbyte A 
		{
			get => sbyte.MaxValue;
			set { }
		}
		
		public readonly void GetChannels(out sbyte r, out sbyte g, out sbyte b, out sbyte a)
		{
			r = R;
			g = G;
			b = B;
			a = A;
		}
		
		public void SetChannels(sbyte r, sbyte g, sbyte b, sbyte a)
		{
			R = r;
			G = g;
			B = b;
		}
		
		static bool IColor<sbyte>.HasRedChannel => true;
		static bool IColor<sbyte>.HasGreenChannel => true;
		static bool IColor<sbyte>.HasBlueChannel => true;
		static bool IColor<sbyte>.HasAlphaChannel => false;
	}
}
