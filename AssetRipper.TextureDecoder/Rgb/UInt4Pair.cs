namespace AssetRipper.TextureDecoder.Rgb
{
	/// <summary>
	/// An 8-bit struct containing a pair of 4-bit unsigned integers.
	/// </summary>
	/// <remarks>
	/// Each of these integers is in the range [0 - 240], with increments of 16.
	/// TODO: Should this be increments of 17? That would give a range of [0 - 255].
	/// </remarks>
	internal struct UInt4Pair
	{
		private byte bits;

		/// <summary>
		/// The value stored in the high bits.
		/// </summary>
		public byte HighValue
		{
			get => (byte)(bits & 0xF0);
			set => bits = (byte)((value & 0xF0u) | (bits & 0x0Fu));
		}

		/// <summary>
		/// The value stored in the low bits.
		/// </summary>
		public byte LowValue
		{
			get => unchecked((byte)((uint)bits << 4));
			set => bits = unchecked((byte)(((uint)value >> 4) | (bits & 0xF0u)));
		}
	}
}