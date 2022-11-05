namespace AssetRipper.TextureDecoder.Rgb.Formats
{
	public partial struct ColorRGBA16 : IColor<byte>
	{
		private UInt4Pair ba;
		private UInt4Pair rg;

		public byte R
		{
			get => rg.HighValue;
			set => rg.HighValue = value;
		}

		public byte G
		{
			get => rg.LowValue;
			set => rg.LowValue = value;
		}

		public byte B
		{
			get => ba.HighValue;
			set => ba.HighValue = value;
		}

		public byte A
		{
			get => ba.LowValue;
			set => ba.LowValue = value;
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
