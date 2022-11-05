//Auto-generated code. Do not modify.
namespace AssetRipper.TextureDecoder.Rgb.Formats
{
	public partial struct ColorRGBA32Signed : IColor<sbyte>
	{
		public sbyte R { get; set; }
		
		public sbyte G { get; set; }
		
		public sbyte B { get; set; }
		
		public sbyte A { get; set; }
		
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
