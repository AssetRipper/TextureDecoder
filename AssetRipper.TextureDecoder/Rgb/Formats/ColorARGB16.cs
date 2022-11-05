namespace AssetRipper.TextureDecoder.Rgb.Formats
{
	public partial struct ColorARGB16 : IColor<byte>
	{
		private UInt4Pair gb;
		private UInt4Pair ar;

		public byte B
		{
			get => gb.LowValue;
			set => gb.LowValue = value;
		}

		public byte G
		{
			get => gb.HighValue;
			set => gb.HighValue = value;
		}

		public byte R
		{
			get => ar.LowValue;
			set => ar.LowValue = value;
		}

		public byte A
		{
			get => ar.HighValue;
			set => ar.HighValue = value;
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
