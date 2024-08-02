using System.Buffers.Binary;

namespace AssetRipper.TextureDecoder.Bc;

internal ref struct BitStream
{
	private UInt128 value;

	private readonly uint BottomBits
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		get => unchecked((uint)this.value);
	}

	public BitStream(UInt128 value)
	{
		this.value = value;
	}

	public BitStream(ReadOnlySpan<byte> span) : this(BinaryPrimitives.ReadUInt128LittleEndian(span))
	{
	}

	public BitStream(ulong low, ulong high)
	{
		value = new UInt128(high, low);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	public readonly uint PeakBits(int numBits)
	{
		uint mask = (1u << numBits) - 1u;
		uint bits = BottomBits & mask;
		return bits;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	public uint ReadBits(int numBits)
	{
		uint bits = PeakBits(numBits);
		this.value >>= numBits;
		return bits;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	public uint ReadBit()
	{
		uint result = BottomBits & 1u;
		this.value >>= 1;
		return result;
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
