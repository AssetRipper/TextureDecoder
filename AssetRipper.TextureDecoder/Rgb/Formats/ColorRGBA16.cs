namespace AssetRipper.TextureDecoder.Rgb.Formats
{
	public partial struct ColorRGBA16 : IColor<byte>
	{
		private byte ba;
		private byte rg;

		public byte B
		{
			get => (byte)(ba & 0xF0u);
			set => ba = (byte)((value & 0xF0u) | (ba & 0x0Fu));
		}
		public byte G
		{
			get => unchecked((byte)(rg << 4));
			set => rg = unchecked((byte)(((uint)value >> 4) | (rg & 0xF0u)));
		}
		public byte R
		{
			get => (byte)(rg & 0xF0u);
			set => rg = (byte)((value & 0xF0u) | (rg & 0x0Fu));
		}
		public byte A
		{
			get => unchecked((byte)(ba << 4));
			set => ba = unchecked((byte)(((uint)value >> 4) | (ba & 0xF0u)));
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
