namespace AssetRipper.TextureDecoder.Bc;

internal struct BitStream
{
	public ulong low;
	public ulong high;
	
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

	public uint ReadBit()
	{
		return this.ReadBits(1);
	}

	/// <summary>
	/// Reversed bits pulling, used in BC6H decoding
	/// </summary>
	/// <param name="numBits"></param>
	/// <returns></returns>
	public uint ReadBitsReversed(int numBits)
	{
		uint bits = this.ReadBits(numBits);
		// Reverse the bits.
		uint result = 0;
		while (numBits-- != 0)
		{
			result <<= 1;
			result |= bits & 1;
			bits >>= 1;
		}
		return result;
	}
}
