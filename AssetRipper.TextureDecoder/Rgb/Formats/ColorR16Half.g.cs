//This code is source generated. Do not edit manually.

using AssetRipper.TextureDecoder.Attributes;

namespace AssetRipper.TextureDecoder.Rgb.Formats
{
	[RgbaAttribute(RedChannel = true, GreenChannel = false, BlueChannel = false, AlphaChannel = false, FullyUtilizedChannels = true)]
	public partial struct ColorR16Half : IColor<Half>
	{
		public Half R { get; set; }
		
		public readonly Half G 
		{
			get => default;
			set { }
		}
		
		public readonly Half B 
		{
			get => default;
			set { }
		}
		
		public readonly Half A 
		{
			get => HalfConstants.One;
			set { }
		}
		
		public readonly void GetChannels(out Half r, out Half g, out Half b, out Half a)
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
		
		static bool IColor<Half>.HasRedChannel => true;
		static bool IColor<Half>.HasGreenChannel => false;
		static bool IColor<Half>.HasBlueChannel => false;
		static bool IColor<Half>.HasAlphaChannel => false;
	}
}
