//This code is source generated. Do not edit manually.

using AssetRipper.TextureDecoder.Attributes;

namespace AssetRipper.TextureDecoder.Rgb.Formats
{
	[RgbaAttribute(RedChannel = true, GreenChannel = true, BlueChannel = true, AlphaChannel = true, FullyUtilizedChannels = true)]
	public partial struct ColorRGBA32Signed : IColor<sbyte>
	{
		public sbyte R { get; set; }
		
		public sbyte G { get; set; }
		
		public sbyte B { get; set; }
		
		public sbyte A { get; set; }
		
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
			A = a;
		}
		
		static bool IColor<sbyte>.HasRedChannel => true;
		static bool IColor<sbyte>.HasGreenChannel => true;
		static bool IColor<sbyte>.HasBlueChannel => true;
		static bool IColor<sbyte>.HasAlphaChannel => true;
	}
}
