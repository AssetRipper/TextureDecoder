namespace AssetRipper.TextureDecoder.Rgb
{
	/// <summary>
	/// An 8-bit struct containing a pair of 4-bit unsigned integers.
	/// </summary>
	internal struct UInt4Pair
	{
		private const uint HighBitMask = 0xF0u;
		private const uint LowBitMask = 0x0Fu;
		private byte bits;

		/// <summary>
		/// The value stored in the high bits.
		/// </summary>
		/// <remarks>
		/// Possible values: any multiple of 17 in the inclusive range [0 - 255].
		/// </remarks>
		public byte HighValue
		{
			readonly get
			{
				return (byte)((bits & HighBitMask) | (uint)bits >> 4);
			}

			set
			{
				unchecked
				{
					bits = (byte)((Convert8BitsTo4Bits(value) << 4) | (bits & LowBitMask));
				}
			}
		}

		/// <summary>
		/// The value stored in the low bits.
		/// </summary>
		/// <remarks>
		/// Possible values: any multiple of 17 in the inclusive range [0 - 255].
		/// </remarks>
		public byte LowValue
		{
			readonly get
			{
				uint relevantBits = bits & LowBitMask;
				return (byte)((relevantBits << 4) | relevantBits);
			}

			set
			{
				unchecked
				{
					bits = (byte)(Convert8BitsTo4Bits(value) | (bits & HighBitMask));
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		private static uint Convert8BitsTo4Bits(byte value)
		{
			//See https://github.com/AssetRipper/TextureDecoder/issues/19
			return (value * 15u + 135u) >> 8;
		}
	}
}