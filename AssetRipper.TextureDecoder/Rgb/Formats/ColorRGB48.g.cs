//This code is source generated. Do not edit manually.

using AssetRipper.TextureDecoder.Attributes;

namespace AssetRipper.TextureDecoder.Rgb.Formats
{
	[RgbaAttribute(RedChannel = true, GreenChannel = true, BlueChannel = true, AlphaChannel = false, FullyUtilizedChannels = true)]
	public partial struct ColorRGB48 : IColor<ushort>
	{
		public ushort R { get; set; }
		
		public ushort G { get; set; }
		
		public ushort B { get; set; }
		
		public ushort A 
		{
			get => ushort.MaxValue;
			set { }
		}
		
		public void GetChannels(out ushort r, out ushort g, out ushort b, out ushort a)
		{
			r = R;
			g = G;
			b = B;
			a = A;
		}
		
		public void SetChannels(ushort r, ushort g, ushort b, ushort a)
		{
			R = r;
			G = g;
			B = b;
		}
	}
}
