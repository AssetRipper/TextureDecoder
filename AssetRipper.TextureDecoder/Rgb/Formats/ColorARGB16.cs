namespace AssetRipper.TextureDecoder.Rgb.Formats
{
	public partial struct ColorARGB16 : IColor<byte>
	{
		private UInt4Pair gb;
		private UInt4Pair ar;

		public byte B
		{
			readonly get => gb.LowValue;
			set => gb.LowValue = value;
		}

		public byte G
		{
			readonly get => gb.HighValue;
			set => gb.HighValue = value;
		}

		public byte R
		{
			readonly get => ar.LowValue;
			set => ar.LowValue = value;
		}

		public byte A
		{
			readonly get => ar.HighValue;
			set => ar.HighValue = value;
		}

		public readonly void GetChannels(out byte r, out byte g, out byte b, out byte a)
		{
			DefaultColorMethods.GetChannels(this, out r, out g, out b, out a);
		}

		public void SetChannels(byte r, byte g, byte b, byte a)
		{
			DefaultColorMethods.SetChannels(ref this, r, g, b, a);
		}
	}
}
