namespace AssetRipper.TextureDecoder.Rgb.Formats
{
	/// <summary>
	/// Also called RGB 565
	/// </summary>
	public partial struct ColorRGB16 : IColor<byte>
	{
		private ushort bits;

		/// <summary>
		/// 5 bits
		/// </summary>
		public byte R
		{
			readonly get => (byte)(((uint)bits >> 8) & 0xF8);
			set
			{
				bits = (ushort)((((uint)value << 8) & 0xF800u) | (bits & ~0xF800u));
			}
		}

		/// <summary>
		/// 6 bits
		/// </summary>
		public byte G
		{
			readonly get => (byte)(((uint)bits >> 3) & 0xFC);
			set
			{
				bits = (ushort)((((uint)value << 3) & 0x07E0u) | (bits & ~0x07E0u));
			}
		}

		/// <summary>
		/// 5 bits
		/// </summary>
		public byte B
		{
			readonly get => (byte)(((uint)bits << 3) & 0xF8);
			set
			{
				bits = (ushort)((((uint)value >> 3) & 0x001Fu) | (bits & ~0x001Fu));
			}
		}

		public readonly byte A
		{
			get => byte.MaxValue;
			set { }
		}

		public readonly void GetChannels(out byte r, out byte g, out byte b, out byte a)
		{
			DefaultColorMethods.GetChannels(this, out r, out g, out b, out a);
		}

		public void SetChannels(byte r, byte g, byte b, byte a)
		{
			bits = (ushort)(((r & 0xF8u) << 8) | ((g & 0xFCu) << 3) | ((b & 0xF8u) >> 3));
		}
	}
}
