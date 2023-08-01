//This code is source generated. Do not edit manually.

using AssetRipper.TextureDecoder.Attributes;

namespace AssetRipper.TextureDecoder.Rgb.Formats
{
	[RgbaAttribute(RedChannel = true, GreenChannel = false, BlueChannel = false, AlphaChannel = false, FullyUtilizedChannels = true)]
	public partial struct ColorR8Signed : IColor<sbyte>
	{
		public sbyte R { get; set; }
		
		public readonly sbyte G 
		{
			get => sbyte.MinValue;
			set { }
		}
		
		public readonly sbyte B 
		{
			get => sbyte.MinValue;
			set { }
		}
		
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
		}
		
		static bool IColor<sbyte>.HasRedChannel => true;
		static bool IColor<sbyte>.HasGreenChannel => false;
		static bool IColor<sbyte>.HasBlueChannel => false;
		static bool IColor<sbyte>.HasAlphaChannel => false;
	}
}
