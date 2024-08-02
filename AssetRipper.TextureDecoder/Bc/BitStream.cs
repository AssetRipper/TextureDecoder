namespace AssetRipper.TextureDecoder.Bc;

internal ref struct BitStream
{
	private ulong low;
	private ulong high;

	public BitStream(ulong low, ulong high)
	{
		this.low = low;
		this.high = high;
	}
	
	public uint ReadBits(int numBits)
	{
		uint mask = (1u << numBits) - 1u;
		// Read the low N bits
		uint bits = unchecked((uint)(this.low & mask));

		this.low >>= numBits;
		// Put the low N bits of "high" into the high 64-N bits of "low".
		this.low |= (this.high & mask) << ((sizeof(ulong) * 8) - numBits);
		this.high >>= numBits;

		return bits;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	public uint ReadBit()
	{
		return this.ReadBits(1);
	}

	/// <summary>
	/// Reversed bits pulling, used in BC6H decoding
	/// </summary>
	/// <param name="numBits"></param>
	/// <returns></returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	public uint ReadBitsReversed(int numBits)
	{
		uint bits = this.ReadBits(numBits);
		// Reverse the bits.
		return bits.ReverseBits() >> (32 - numBits);
	}
}
