namespace AssetRipper.TextureDecoder.Rgb.Formats
{
	public partial struct ColorRGBA16 : IColor<byte>
	{
		private UInt4Pair ba;
		private UInt4Pair rg;

		public byte R
		{
			readonly get => rg.HighValue;
			set => rg.HighValue = value;
		}

		public byte G
		{
			readonly get => rg.LowValue;
			set => rg.LowValue = value;
		}

		public byte B
		{
			readonly get => ba.HighValue;
			set => ba.HighValue = value;
		}

		public byte A
		{
			readonly get => ba.LowValue;
			set => ba.LowValue = value;
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
