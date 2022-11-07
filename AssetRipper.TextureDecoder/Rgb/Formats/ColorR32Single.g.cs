//This code is source generated. Do not edit manually.

using AssetRipper.TextureDecoder.Attributes;

namespace AssetRipper.TextureDecoder.Rgb.Formats
{
	[RgbaAttribute(RedChannel = true, GreenChannel = false, BlueChannel = false, AlphaChannel = false, FullyUtilizedChannels = true)]
	public partial struct ColorR32Single : IColor<float>
	{
		public float R { get; set; }
		
		public float G 
		{
			get => 0f;
			set { }
		}
		
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
			DefaultColorMethods.GetChannels(this, out r, out g, out b, out a);
		}
		
		public void SetChannels(float r, float g, float b, float a)
		{
			DefaultColorMethods.SetChannels(ref this, r, g, b, a);
		}
	}
}
