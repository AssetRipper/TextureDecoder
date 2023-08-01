//This code is source generated. Do not edit manually.

using AssetRipper.TextureDecoder.Attributes;

namespace AssetRipper.TextureDecoder.Rgb.Formats
{
	[RgbaAttribute(RedChannel = true, GreenChannel = true, BlueChannel = false, AlphaChannel = false, FullyUtilizedChannels = true)]
	public partial struct ColorRG64Single : IColor<float>
	{
		public float R { get; set; }
		
		public float G { get; set; }
		
		public float B 
		{
			get => 0f;
			set { }
		}
		
		public float A 
		{
			get => 1f;
			set { }
		}
		
		public void GetChannels(out float r, out float g, out float b, out float a)
		{
			r = R;
			g = G;
			b = B;
			a = A;
		}
		
		public void SetChannels(float r, float g, float b, float a)
		{
			R = r;
			G = g;
		}
		
		static bool IColor<float>.HasRedChannel => true;
		static bool IColor<float>.HasGreenChannel => true;
		static bool IColor<float>.HasBlueChannel => false;
		static bool IColor<float>.HasAlphaChannel => false;
	}
}
