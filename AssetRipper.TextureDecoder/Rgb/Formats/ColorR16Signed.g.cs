//Auto-generated code. Do not modify.
namespace AssetRipper.TextureDecoder.Rgb.Formats
{
	public partial struct ColorR16Signed : IColor<short>
	{
		public short R { get; set; }
		
		public short G 
		{
			get => short.MinValue;
			set { }
		}
		
		public short B 
		{
			get => short.MinValue;
			set { }
		}
		
		public short A 
		{
			get => short.MaxValue;
			set { }
		}
		
		public void GetChannels(out short r, out short g, out short b, out short a)
		{
			DefaultColorMethods.GetChannels(this, out r, out g, out b, out a);
		}
		
		public void SetChannels(short r, short g, short b, short a)
		{
			DefaultColorMethods.SetChannels(ref this, r, g, b, a);
		}
	}
}
