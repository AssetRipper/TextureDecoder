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
			get => (Half)1;
			set { }
		}
		
		public void GetChannels(out Half r, out Half g, out Half b, out Half a)
		{
			DefaultColorMethods.GetChannels(this, out r, out g, out b, out a);
		}
		
		public void SetChannels(Half r, Half g, Half b, Half a)
		{
			DefaultColorMethods.SetChannels(ref this, r, g, b, a);
		}
	}
}
