//This code is source generated. Do not edit manually.

using AssetRipper.TextureDecoder.Attributes;

namespace AssetRipper.TextureDecoder.Rgb.Formats
{
	[RgbaAttribute(RedChannel = true, GreenChannel = false, BlueChannel = false, AlphaChannel = false, FullyUtilizedChannels = true)]
	public partial struct ColorR8Signed : IColor<sbyte>
	{
		public sbyte R { get; set; }
		
		public sbyte G 
		{
			get => sbyte.MinValue;
			set { }
		}
		
		public sbyte B 
		{
			get => sbyte.MinValue;
			set { }
		}
		
		public sbyte A 
		{
			get => sbyte.MaxValue;
			set { }
		}
		
		public void GetChannels(out sbyte r, out sbyte g, out sbyte b, out sbyte a)
		{
			DefaultColorMethods.GetChannels(this, out r, out g, out b, out a);
		}
		
		public void SetChannels(sbyte r, sbyte g, sbyte b, sbyte a)
		{
			DefaultColorMethods.SetChannels(ref this, r, g, b, a);
		}
	}
}
