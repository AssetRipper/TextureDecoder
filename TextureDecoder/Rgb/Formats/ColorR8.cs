namespace AssetRipper.TextureDecoder.Rgb.Formats
{
	public struct ColorR8 : IColor<byte>
	{
		public byte R { get; set; }
		public byte G
		{
			get => byte.MinValue;
			set { }
		}
		public byte B
		{
			get => byte.MinValue;
			set { }
		}
		public byte A
		{
			get => byte.MaxValue;
			set { }
		}

		public void GetChannels(out byte r, out byte g, out byte b, out byte a)
		{
			DefaultColorMethods.GetChannels(this, out r, out g, out b, out a);
		}

		public void SetChannels(byte r, byte g, byte b, byte a)
		{
			DefaultColorMethods.SetChannels(ref this, r, g, b, a);
		}
	}
}
