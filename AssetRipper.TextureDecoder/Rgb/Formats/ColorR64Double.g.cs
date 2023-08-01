//This code is source generated. Do not edit manually.

using AssetRipper.TextureDecoder.Attributes;

namespace AssetRipper.TextureDecoder.Rgb.Formats
{
	[RgbaAttribute(RedChannel = true, GreenChannel = false, BlueChannel = false, AlphaChannel = false, FullyUtilizedChannels = true)]
	public partial struct ColorR64Double : IColor<double>
	{
		public double R { get; set; }
		
		public readonly double G 
		{
			get => 0d;
			set { }
		}
		
		public readonly double B 
		{
			get => 0d;
			set { }
		}
		
		public readonly double A 
		{
			get => 1d;
			set { }
		}
		
		public readonly void GetChannels(out double r, out double g, out double b, out double a)
		{
			r = R;
			g = G;
			b = B;
			a = A;
		}
		
		public void SetChannels(double r, double g, double b, double a)
		{
			R = r;
		}
		
		static bool IColor<double>.HasRedChannel => true;
		static bool IColor<double>.HasGreenChannel => false;
		static bool IColor<double>.HasBlueChannel => false;
		static bool IColor<double>.HasAlphaChannel => false;
	}
}
