namespace AssetRipper.TextureDecoder.Rgb.Formats
{
	public partial struct ColorARGB16 : IColor<byte>
	{
		private byte gb;
		private byte ar;

		public byte B
		{
			get => unchecked((byte)((uint)gb << 4));
			set => gb = unchecked((byte)(((uint)value >> 4) | (gb & 0xF0u)));
		}
		public byte G
		{
			get => (byte)(gb & 0xF0);
			set => gb = (byte)((value & 0xF0u) | (gb & 0x0Fu));
		}
		public byte R
		{
			get => unchecked((byte)(ar << 4));
			set => ar = unchecked((byte)(((uint)value >> 4) | (ar & 0xF0u)));
		}
		public byte A
		{
			get => (byte)(ar & 0xF0);
			set => ar = (byte)((value & 0xF0u) | (ar & 0x0Fu));
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
