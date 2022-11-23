//This code is source generated. Do not edit manually.

using AssetRipper.TextureDecoder.Attributes;

namespace AssetRipper.TextureDecoder.Rgb.Formats
{
	[RgbaAttribute(RedChannel = true, GreenChannel = false, BlueChannel = false, AlphaChannel = false, FullyUtilizedChannels = true)]
	public partial struct ColorR16Half : IColor<Half>
	{
		public Half R { get; set; }
		
		public Half G 
		{
			get => default;
			set { }
		}
		
		public Half B 
		{
			get => default;
			set { }
		}
		
		public Half A 
		{
			get => HalfConstants.One;
			set { }
		}
		
		public void GetChannels(out Half r, out Half g, out Half b, out Half a)
		{
			r = R;
			g = G;
			b = B;
			a = A;
		}
		
		public void SetChannels(Half r, Half g, Half b, Half a)
		{
			R = r;
		}
	}
}
