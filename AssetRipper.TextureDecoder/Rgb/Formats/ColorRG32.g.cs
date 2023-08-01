//This code is source generated. Do not edit manually.

using AssetRipper.TextureDecoder.Attributes;

namespace AssetRipper.TextureDecoder.Rgb.Formats
{
	[RgbaAttribute(RedChannel = true, GreenChannel = true, BlueChannel = false, AlphaChannel = false, FullyUtilizedChannels = true)]
	public partial struct ColorRG32 : IColor<ushort>
	{
		public ushort R { get; set; }
		
		public ushort G { get; set; }
		
		public readonly ushort B 
		{
			get => ushort.MinValue;
			set { }
		}
		
		public readonly ushort A 
		{
			get => ushort.MaxValue;
			set { }
		}
		
		public readonly void GetChannels(out ushort r, out ushort g, out ushort b, out ushort a)
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
		}
		
		static bool IColor<ushort>.HasRedChannel => true;
		static bool IColor<ushort>.HasGreenChannel => true;
		static bool IColor<ushort>.HasBlueChannel => false;
		static bool IColor<ushort>.HasAlphaChannel => false;
	}
}
