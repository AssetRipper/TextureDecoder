// Auto-generated code. Do not modify manually.

namespace AssetRipper.TextureDecoder.Rgb;

static partial class NumericConversion
{
	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	public static TTo Convert<TFrom, TTo>(TFrom value) where TFrom : unmanaged where TTo : unmanaged
	{
		if (typeof(TFrom) == typeof(TTo))
		{
			return Unsafe.BitCast<TFrom, TTo>(value);
		}
		else if (typeof(TFrom) == typeof(sbyte))
		{
			return ConvertSByte<TTo>(Unsafe.BitCast<TFrom, sbyte>(value));
		}
		else if (typeof(TFrom) == typeof(byte))
		{
			return ConvertByte<TTo>(Unsafe.BitCast<TFrom, byte>(value));
		}
		else if (typeof(TFrom) == typeof(short))
		{
			return ConvertInt16<TTo>(Unsafe.BitCast<TFrom, short>(value));
		}
		else if (typeof(TFrom) == typeof(ushort))
		{
			return ConvertUInt16<TTo>(Unsafe.BitCast<TFrom, ushort>(value));
		}
		else if (typeof(TFrom) == typeof(int))
		{
			return ConvertInt32<TTo>(Unsafe.BitCast<TFrom, int>(value));
		}
		else if (typeof(TFrom) == typeof(uint))
		{
			return ConvertUInt32<TTo>(Unsafe.BitCast<TFrom, uint>(value));
		}
		else if (typeof(TFrom) == typeof(nint))
		{
			return ConvertIntPtr<TTo>(Unsafe.BitCast<TFrom, nint>(value));
		}
		else if (typeof(TFrom) == typeof(nuint))
		{
			return ConvertUIntPtr<TTo>(Unsafe.BitCast<TFrom, nuint>(value));
		}
		else if (typeof(TFrom) == typeof(long))
		{
			return ConvertInt64<TTo>(Unsafe.BitCast<TFrom, long>(value));
		}
		else if (typeof(TFrom) == typeof(ulong))
		{
			return ConvertUInt64<TTo>(Unsafe.BitCast<TFrom, ulong>(value));
		}
		else if (typeof(TFrom) == typeof(Int128))
		{
			return ConvertInt128<TTo>(Unsafe.BitCast<TFrom, Int128>(value));
		}
		else if (typeof(TFrom) == typeof(UInt128))
		{
			return ConvertUInt128<TTo>(Unsafe.BitCast<TFrom, UInt128>(value));
		}
		else if (typeof(TFrom) == typeof(Half))
		{
			return ConvertHalf<TTo>(Unsafe.BitCast<TFrom, Half>(value));
		}
		else if (typeof(TFrom) == typeof(float))
		{
			return ConvertSingle<TTo>(Unsafe.BitCast<TFrom, float>(value));
		}
		else if (typeof(TFrom) == typeof(NFloat))
		{
			return ConvertNFloat<TTo>(Unsafe.BitCast<TFrom, NFloat>(value));
		}
		else if (typeof(TFrom) == typeof(double))
		{
			return ConvertDouble<TTo>(Unsafe.BitCast<TFrom, double>(value));
		}
		else if (typeof(TFrom) == typeof(decimal))
		{
			return ConvertDecimal<TTo>(Unsafe.BitCast<TFrom, decimal>(value));
		}
		else
		{
			return ThrowOrReturnDefault<TTo>();
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	private static TTo ConvertSByte<TTo>(sbyte value) where TTo : unmanaged
	{
		return ConvertByte<TTo>(ChangeSign(value));
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	private static TTo ConvertByte<TTo>(byte value) where TTo : unmanaged
	{
		if (typeof(TTo) == typeof(sbyte))
		{
			sbyte converted = ChangeSign(ConvertByte<byte>(value));
			return Unsafe.BitCast<sbyte, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(byte))
		{
			return Unsafe.BitCast<byte, TTo>(value);
		}
		else if (typeof(TTo) == typeof(short))
		{
			short converted = ChangeSign(ConvertByte<ushort>(value));
			return Unsafe.BitCast<short, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(ushort))
		{
			// See https://github.com/AssetRipper/TextureDecoder/issues/19
			unchecked
			{
				ushort converted = (ushort)(((uint)value << 8) | value);
				return Unsafe.BitCast<ushort, TTo>(converted);
			}
		}
		else if (typeof(TTo) == typeof(int))
		{
			int converted = ChangeSign(ConvertByte<uint>(value));
			return Unsafe.BitCast<int, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(uint))
		{
			// See https://github.com/AssetRipper/TextureDecoder/issues/19
			unchecked
			{
				uint converted = (uint)(((uint)value << 24) | ((uint)value << 16) | ((uint)value << 8) | value);
				return Unsafe.BitCast<uint, TTo>(converted);
			}
		}
		else if (typeof(TTo) == typeof(nint))
		{
			if (IntPtr.Size == sizeof(int))
			{
				nint converted = (nint)ConvertByte<int>(value);
				return Unsafe.BitCast<nint, TTo>(converted);
			}
			else
			{
				nint converted = (nint)ConvertByte<long>(value);
				return Unsafe.BitCast<nint, TTo>(converted);
			}
		}
		else if (typeof(TTo) == typeof(nuint))
		{
			if (IntPtr.Size == sizeof(int))
			{
				nuint converted = (nuint)ConvertByte<uint>(value);
				return Unsafe.BitCast<nuint, TTo>(converted);
			}
			else
			{
				nuint converted = (nuint)ConvertByte<ulong>(value);
				return Unsafe.BitCast<nuint, TTo>(converted);
			}
		}
		else if (typeof(TTo) == typeof(long))
		{
			long converted = ChangeSign(ConvertByte<ulong>(value));
			return Unsafe.BitCast<long, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(ulong))
		{
			// See https://github.com/AssetRipper/TextureDecoder/issues/19
			unchecked
			{
				ulong converted = (ulong)(((ulong)value << 56) | ((ulong)value << 48) | ((ulong)value << 40) | ((ulong)value << 32) | ((ulong)value << 24) | ((ulong)value << 16) | ((ulong)value << 8) | value);
				return Unsafe.BitCast<ulong, TTo>(converted);
			}
		}
		else if (typeof(TTo) == typeof(Int128))
		{
			Int128 converted = ChangeSign(ConvertByte<UInt128>(value));
			return Unsafe.BitCast<Int128, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(UInt128))
		{
			// See https://github.com/AssetRipper/TextureDecoder/issues/19
			unchecked
			{
				UInt128 converted = (UInt128)(((UInt128)value << 120) | ((UInt128)value << 112) | ((UInt128)value << 104) | ((UInt128)value << 96) | ((UInt128)value << 88) | ((UInt128)value << 80) | ((UInt128)value << 72) | ((UInt128)value << 64) | ((UInt128)value << 56) | ((UInt128)value << 48) | ((UInt128)value << 40) | ((UInt128)value << 32) | ((UInt128)value << 24) | ((UInt128)value << 16) | ((UInt128)value << 8) | value);
				return Unsafe.BitCast<UInt128, TTo>(converted);
			}
		}
		else if (typeof(TTo) == typeof(Half))
		{
			// There isn't enough precision to convert from byte to Half, so we convert to float first.
			float x = ConvertByte<float>(value);
			Half converted = (Half)x;
			return Unsafe.BitCast<Half, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(float))
		{
			float converted = (float)value / (float)byte.MaxValue;
			return Unsafe.BitCast<float, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(NFloat))
		{
			if (IntPtr.Size == sizeof(int))
			{
				NFloat converted = (NFloat)ConvertByte<float>(value);
				return Unsafe.BitCast<NFloat, TTo>(converted);
			}
			else
			{
				NFloat converted = (NFloat)ConvertByte<double>(value);
				return Unsafe.BitCast<NFloat, TTo>(converted);
			}
		}
		else if (typeof(TTo) == typeof(double))
		{
			double converted = (double)value / (double)byte.MaxValue;
			return Unsafe.BitCast<double, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(decimal))
		{
			decimal converted = (decimal)value / (decimal)byte.MaxValue;
			return Unsafe.BitCast<decimal, TTo>(converted);
		}
		else
		{
			return ThrowOrReturnDefault<TTo>();
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	private static TTo ConvertInt16<TTo>(short value) where TTo : unmanaged
	{
		return ConvertUInt16<TTo>(ChangeSign(value));
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	private static TTo ConvertUInt16<TTo>(ushort value) where TTo : unmanaged
	{
		if (typeof(TTo) == typeof(sbyte))
		{
			sbyte converted = ChangeSign(ConvertUInt16<byte>(value));
			return Unsafe.BitCast<sbyte, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(byte))
		{
			// See https://github.com/AssetRipper/TextureDecoder/issues/19
			// This is a special case where we already know an optimal algorithm.
			uint x = (value * 255u + 32895u) >> 16;
			byte converted = unchecked((byte)x);
			return Unsafe.BitCast<byte, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(short))
		{
			short converted = ChangeSign(ConvertUInt16<ushort>(value));
			return Unsafe.BitCast<short, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(ushort))
		{
			return Unsafe.BitCast<ushort, TTo>(value);
		}
		else if (typeof(TTo) == typeof(int))
		{
			int converted = ChangeSign(ConvertUInt16<uint>(value));
			return Unsafe.BitCast<int, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(uint))
		{
			// See https://github.com/AssetRipper/TextureDecoder/issues/19
			unchecked
			{
				uint converted = (uint)(((uint)value << 16) | value);
				return Unsafe.BitCast<uint, TTo>(converted);
			}
		}
		else if (typeof(TTo) == typeof(nint))
		{
			if (IntPtr.Size == sizeof(int))
			{
				nint converted = (nint)ConvertUInt16<int>(value);
				return Unsafe.BitCast<nint, TTo>(converted);
			}
			else
			{
				nint converted = (nint)ConvertUInt16<long>(value);
				return Unsafe.BitCast<nint, TTo>(converted);
			}
		}
		else if (typeof(TTo) == typeof(nuint))
		{
			if (IntPtr.Size == sizeof(int))
			{
				nuint converted = (nuint)ConvertUInt16<uint>(value);
				return Unsafe.BitCast<nuint, TTo>(converted);
			}
			else
			{
				nuint converted = (nuint)ConvertUInt16<ulong>(value);
				return Unsafe.BitCast<nuint, TTo>(converted);
			}
		}
		else if (typeof(TTo) == typeof(long))
		{
			long converted = ChangeSign(ConvertUInt16<ulong>(value));
			return Unsafe.BitCast<long, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(ulong))
		{
			// See https://github.com/AssetRipper/TextureDecoder/issues/19
			unchecked
			{
				ulong converted = (ulong)(((ulong)value << 48) | ((ulong)value << 32) | ((ulong)value << 16) | value);
				return Unsafe.BitCast<ulong, TTo>(converted);
			}
		}
		else if (typeof(TTo) == typeof(Int128))
		{
			Int128 converted = ChangeSign(ConvertUInt16<UInt128>(value));
			return Unsafe.BitCast<Int128, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(UInt128))
		{
			// See https://github.com/AssetRipper/TextureDecoder/issues/19
			unchecked
			{
				UInt128 converted = (UInt128)(((UInt128)value << 112) | ((UInt128)value << 96) | ((UInt128)value << 80) | ((UInt128)value << 64) | ((UInt128)value << 48) | ((UInt128)value << 32) | ((UInt128)value << 16) | value);
				return Unsafe.BitCast<UInt128, TTo>(converted);
			}
		}
		else if (typeof(TTo) == typeof(Half))
		{
			// There isn't enough precision to convert from ushort to Half, so we convert to float first.
			float x = ConvertUInt16<float>(value);
			Half converted = (Half)x;
			return Unsafe.BitCast<Half, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(float))
		{
			float converted = (float)value / (float)ushort.MaxValue;
			return Unsafe.BitCast<float, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(NFloat))
		{
			if (IntPtr.Size == sizeof(int))
			{
				NFloat converted = (NFloat)ConvertUInt16<float>(value);
				return Unsafe.BitCast<NFloat, TTo>(converted);
			}
			else
			{
				NFloat converted = (NFloat)ConvertUInt16<double>(value);
				return Unsafe.BitCast<NFloat, TTo>(converted);
			}
		}
		else if (typeof(TTo) == typeof(double))
		{
			double converted = (double)value / (double)ushort.MaxValue;
			return Unsafe.BitCast<double, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(decimal))
		{
			decimal converted = (decimal)value / (decimal)ushort.MaxValue;
			return Unsafe.BitCast<decimal, TTo>(converted);
		}
		else
		{
			return ThrowOrReturnDefault<TTo>();
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	private static TTo ConvertInt32<TTo>(int value) where TTo : unmanaged
	{
		return ConvertUInt32<TTo>(ChangeSign(value));
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	private static TTo ConvertUInt32<TTo>(uint value) where TTo : unmanaged
	{
		if (typeof(TTo) == typeof(sbyte))
		{
			sbyte converted = ChangeSign(ConvertUInt32<byte>(value));
			return Unsafe.BitCast<sbyte, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(byte))
		{
			// See https://github.com/AssetRipper/TextureDecoder/issues/19
			double interpolated = (double)value / (double)uint.MaxValue;
			double exact = interpolated * (double)byte.MaxValue;
			byte converted = (byte)double.Round(exact, MidpointRounding.AwayFromZero);
			return Unsafe.BitCast<byte, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(short))
		{
			short converted = ChangeSign(ConvertUInt32<ushort>(value));
			return Unsafe.BitCast<short, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(ushort))
		{
			// See https://github.com/AssetRipper/TextureDecoder/issues/19
			double interpolated = (double)value / (double)uint.MaxValue;
			double exact = interpolated * (double)ushort.MaxValue;
			ushort converted = (ushort)double.Round(exact, MidpointRounding.AwayFromZero);
			return Unsafe.BitCast<ushort, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(int))
		{
			int converted = ChangeSign(ConvertUInt32<uint>(value));
			return Unsafe.BitCast<int, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(uint))
		{
			return Unsafe.BitCast<uint, TTo>(value);
		}
		else if (typeof(TTo) == typeof(nint))
		{
			if (IntPtr.Size == sizeof(int))
			{
				nint converted = (nint)ConvertUInt32<int>(value);
				return Unsafe.BitCast<nint, TTo>(converted);
			}
			else
			{
				nint converted = (nint)ConvertUInt32<long>(value);
				return Unsafe.BitCast<nint, TTo>(converted);
			}
		}
		else if (typeof(TTo) == typeof(nuint))
		{
			if (IntPtr.Size == sizeof(int))
			{
				nuint converted = (nuint)ConvertUInt32<uint>(value);
				return Unsafe.BitCast<nuint, TTo>(converted);
			}
			else
			{
				nuint converted = (nuint)ConvertUInt32<ulong>(value);
				return Unsafe.BitCast<nuint, TTo>(converted);
			}
		}
		else if (typeof(TTo) == typeof(long))
		{
			long converted = ChangeSign(ConvertUInt32<ulong>(value));
			return Unsafe.BitCast<long, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(ulong))
		{
			// See https://github.com/AssetRipper/TextureDecoder/issues/19
			unchecked
			{
				ulong converted = (ulong)(((ulong)value << 32) | value);
				return Unsafe.BitCast<ulong, TTo>(converted);
			}
		}
		else if (typeof(TTo) == typeof(Int128))
		{
			Int128 converted = ChangeSign(ConvertUInt32<UInt128>(value));
			return Unsafe.BitCast<Int128, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(UInt128))
		{
			// See https://github.com/AssetRipper/TextureDecoder/issues/19
			unchecked
			{
				UInt128 converted = (UInt128)(((UInt128)value << 96) | ((UInt128)value << 64) | ((UInt128)value << 32) | value);
				return Unsafe.BitCast<UInt128, TTo>(converted);
			}
		}
		else if (typeof(TTo) == typeof(Half))
		{
			// There isn't enough precision to convert from uint to Half, so we convert to double first.
			double x = ConvertUInt32<double>(value);
			Half converted = (Half)x;
			return Unsafe.BitCast<Half, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(float))
		{
			// There isn't enough precision to convert from uint to float, so we convert to double first.
			double x = ConvertUInt32<double>(value);
			float converted = (float)x;
			return Unsafe.BitCast<float, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(NFloat))
		{
			if (IntPtr.Size == sizeof(int))
			{
				NFloat converted = (NFloat)ConvertUInt32<float>(value);
				return Unsafe.BitCast<NFloat, TTo>(converted);
			}
			else
			{
				NFloat converted = (NFloat)ConvertUInt32<double>(value);
				return Unsafe.BitCast<NFloat, TTo>(converted);
			}
		}
		else if (typeof(TTo) == typeof(double))
		{
			double converted = (double)value / (double)uint.MaxValue;
			return Unsafe.BitCast<double, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(decimal))
		{
			decimal converted = (decimal)value / (decimal)uint.MaxValue;
			return Unsafe.BitCast<decimal, TTo>(converted);
		}
		else
		{
			return ThrowOrReturnDefault<TTo>();
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	private static TTo ConvertIntPtr<TTo>(nint value) where TTo : unmanaged
	{
		if (IntPtr.Size == sizeof(int))
		{
			return ConvertInt32<TTo>((int)value);
		}
		else
		{
			return ConvertInt64<TTo>((long)value);
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	private static TTo ConvertUIntPtr<TTo>(nuint value) where TTo : unmanaged
	{
		if (IntPtr.Size == sizeof(int))
		{
			return ConvertUInt32<TTo>((uint)value);
		}
		else
		{
			return ConvertUInt64<TTo>((ulong)value);
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	private static TTo ConvertInt64<TTo>(long value) where TTo : unmanaged
	{
		return ConvertUInt64<TTo>(ChangeSign(value));
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	private static TTo ConvertUInt64<TTo>(ulong value) where TTo : unmanaged
	{
		if (typeof(TTo) == typeof(sbyte))
		{
			sbyte converted = ChangeSign(ConvertUInt64<byte>(value));
			return Unsafe.BitCast<sbyte, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(byte))
		{
			// See https://github.com/AssetRipper/TextureDecoder/issues/19
			// There are more accurate ways to map UInt64 onto Byte, but this is the simplest.
			unchecked
			{
				byte converted = (byte)((ulong)value >> 56);
				return Unsafe.BitCast<byte, TTo>(converted);
			}
		}
		else if (typeof(TTo) == typeof(short))
		{
			short converted = ChangeSign(ConvertUInt64<ushort>(value));
			return Unsafe.BitCast<short, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(ushort))
		{
			// See https://github.com/AssetRipper/TextureDecoder/issues/19
			// There are more accurate ways to map UInt64 onto UInt16, but this is the simplest.
			unchecked
			{
				ushort converted = (ushort)((ulong)value >> 48);
				return Unsafe.BitCast<ushort, TTo>(converted);
			}
		}
		else if (typeof(TTo) == typeof(int))
		{
			int converted = ChangeSign(ConvertUInt64<uint>(value));
			return Unsafe.BitCast<int, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(uint))
		{
			// See https://github.com/AssetRipper/TextureDecoder/issues/19
			// There are more accurate ways to map UInt64 onto UInt32, but this is the simplest.
			unchecked
			{
				uint converted = (uint)((ulong)value >> 32);
				return Unsafe.BitCast<uint, TTo>(converted);
			}
		}
		else if (typeof(TTo) == typeof(nint))
		{
			if (IntPtr.Size == sizeof(int))
			{
				nint converted = (nint)ConvertUInt64<int>(value);
				return Unsafe.BitCast<nint, TTo>(converted);
			}
			else
			{
				nint converted = (nint)ConvertUInt64<long>(value);
				return Unsafe.BitCast<nint, TTo>(converted);
			}
		}
		else if (typeof(TTo) == typeof(nuint))
		{
			if (IntPtr.Size == sizeof(int))
			{
				nuint converted = (nuint)ConvertUInt64<uint>(value);
				return Unsafe.BitCast<nuint, TTo>(converted);
			}
			else
			{
				nuint converted = (nuint)ConvertUInt64<ulong>(value);
				return Unsafe.BitCast<nuint, TTo>(converted);
			}
		}
		else if (typeof(TTo) == typeof(long))
		{
			long converted = ChangeSign(ConvertUInt64<ulong>(value));
			return Unsafe.BitCast<long, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(ulong))
		{
			return Unsafe.BitCast<ulong, TTo>(value);
		}
		else if (typeof(TTo) == typeof(Int128))
		{
			Int128 converted = ChangeSign(ConvertUInt64<UInt128>(value));
			return Unsafe.BitCast<Int128, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(UInt128))
		{
			// See https://github.com/AssetRipper/TextureDecoder/issues/19
			unchecked
			{
				UInt128 converted = (UInt128)(((UInt128)value << 64) | value);
				return Unsafe.BitCast<UInt128, TTo>(converted);
			}
		}
		else if (typeof(TTo) == typeof(Half))
		{
			// There isn't enough precision to convert from ulong to Half, so we convert to double first.
			double x = ConvertUInt64<double>(value);
			Half converted = (Half)x;
			return Unsafe.BitCast<Half, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(float))
		{
			// There isn't enough precision to convert from ulong to float, so we convert to double first.
			double x = ConvertUInt64<double>(value);
			float converted = (float)x;
			return Unsafe.BitCast<float, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(NFloat))
		{
			if (IntPtr.Size == sizeof(int))
			{
				NFloat converted = (NFloat)ConvertUInt64<float>(value);
				return Unsafe.BitCast<NFloat, TTo>(converted);
			}
			else
			{
				NFloat converted = (NFloat)ConvertUInt64<double>(value);
				return Unsafe.BitCast<NFloat, TTo>(converted);
			}
		}
		else if (typeof(TTo) == typeof(double))
		{
			double converted = (double)value / (double)ulong.MaxValue;
			return Unsafe.BitCast<double, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(decimal))
		{
			decimal converted = (decimal)value / (decimal)ulong.MaxValue;
			return Unsafe.BitCast<decimal, TTo>(converted);
		}
		else
		{
			return ThrowOrReturnDefault<TTo>();
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	private static TTo ConvertInt128<TTo>(Int128 value) where TTo : unmanaged
	{
		return ConvertUInt128<TTo>(ChangeSign(value));
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	private static TTo ConvertUInt128<TTo>(UInt128 value) where TTo : unmanaged
	{
		if (typeof(TTo) == typeof(sbyte))
		{
			sbyte converted = ChangeSign(ConvertUInt128<byte>(value));
			return Unsafe.BitCast<sbyte, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(byte))
		{
			// See https://github.com/AssetRipper/TextureDecoder/issues/19
			// There are more accurate ways to map UInt128 onto Byte, but this is the simplest.
			unchecked
			{
				byte converted = (byte)((UInt128)value >> 120);
				return Unsafe.BitCast<byte, TTo>(converted);
			}
		}
		else if (typeof(TTo) == typeof(short))
		{
			short converted = ChangeSign(ConvertUInt128<ushort>(value));
			return Unsafe.BitCast<short, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(ushort))
		{
			// See https://github.com/AssetRipper/TextureDecoder/issues/19
			// There are more accurate ways to map UInt128 onto UInt16, but this is the simplest.
			unchecked
			{
				ushort converted = (ushort)((UInt128)value >> 112);
				return Unsafe.BitCast<ushort, TTo>(converted);
			}
		}
		else if (typeof(TTo) == typeof(int))
		{
			int converted = ChangeSign(ConvertUInt128<uint>(value));
			return Unsafe.BitCast<int, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(uint))
		{
			// See https://github.com/AssetRipper/TextureDecoder/issues/19
			// There are more accurate ways to map UInt128 onto UInt32, but this is the simplest.
			unchecked
			{
				uint converted = (uint)((UInt128)value >> 96);
				return Unsafe.BitCast<uint, TTo>(converted);
			}
		}
		else if (typeof(TTo) == typeof(nint))
		{
			if (IntPtr.Size == sizeof(int))
			{
				nint converted = (nint)ConvertUInt128<int>(value);
				return Unsafe.BitCast<nint, TTo>(converted);
			}
			else
			{
				nint converted = (nint)ConvertUInt128<long>(value);
				return Unsafe.BitCast<nint, TTo>(converted);
			}
		}
		else if (typeof(TTo) == typeof(nuint))
		{
			if (IntPtr.Size == sizeof(int))
			{
				nuint converted = (nuint)ConvertUInt128<uint>(value);
				return Unsafe.BitCast<nuint, TTo>(converted);
			}
			else
			{
				nuint converted = (nuint)ConvertUInt128<ulong>(value);
				return Unsafe.BitCast<nuint, TTo>(converted);
			}
		}
		else if (typeof(TTo) == typeof(long))
		{
			long converted = ChangeSign(ConvertUInt128<ulong>(value));
			return Unsafe.BitCast<long, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(ulong))
		{
			// See https://github.com/AssetRipper/TextureDecoder/issues/19
			// There are more accurate ways to map UInt128 onto UInt64, but this is the simplest.
			unchecked
			{
				ulong converted = (ulong)((UInt128)value >> 64);
				return Unsafe.BitCast<ulong, TTo>(converted);
			}
		}
		else if (typeof(TTo) == typeof(Int128))
		{
			Int128 converted = ChangeSign(ConvertUInt128<UInt128>(value));
			return Unsafe.BitCast<Int128, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(UInt128))
		{
			return Unsafe.BitCast<UInt128, TTo>(value);
		}
		else if (typeof(TTo) == typeof(Half))
		{
			// There isn't enough precision to convert from UInt128 to Half, so we convert to double first.
			double x = ConvertUInt128<double>(value);
			Half converted = (Half)x;
			return Unsafe.BitCast<Half, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(float))
		{
			// There isn't enough precision to convert from UInt128 to float, so we convert to double first.
			double x = ConvertUInt128<double>(value);
			float converted = (float)x;
			return Unsafe.BitCast<float, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(NFloat))
		{
			if (IntPtr.Size == sizeof(int))
			{
				NFloat converted = (NFloat)ConvertUInt128<float>(value);
				return Unsafe.BitCast<NFloat, TTo>(converted);
			}
			else
			{
				NFloat converted = (NFloat)ConvertUInt128<double>(value);
				return Unsafe.BitCast<NFloat, TTo>(converted);
			}
		}
		else if (typeof(TTo) == typeof(double))
		{
			double converted = (double)value / (double)UInt128.MaxValue;
			return Unsafe.BitCast<double, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(decimal))
		{
			decimal converted = (decimal)value / (decimal)UInt128.MaxValue;
			return Unsafe.BitCast<decimal, TTo>(converted);
		}
		else
		{
			return ThrowOrReturnDefault<TTo>();
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	private static TTo ConvertHalf<TTo>(Half value) where TTo : unmanaged
	{
		if (typeof(TTo) == typeof(sbyte))
		{
			sbyte converted = ChangeSign(ConvertHalf<byte>(value));
			return Unsafe.BitCast<sbyte, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(byte))
		{
			// We use float because it has enough precision to convert from Half to byte.
			return ConvertSingle<TTo>((float)value);
		}
		else if (typeof(TTo) == typeof(short))
		{
			short converted = ChangeSign(ConvertHalf<ushort>(value));
			return Unsafe.BitCast<short, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(ushort))
		{
			// We use float because it has enough precision to convert from Half to ushort.
			return ConvertSingle<TTo>((float)value);
		}
		else if (typeof(TTo) == typeof(int))
		{
			int converted = ChangeSign(ConvertHalf<uint>(value));
			return Unsafe.BitCast<int, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(uint))
		{
			// We use double because it has enough precision to convert from Half to uint.
			return ConvertDouble<TTo>((double)value);
		}
		else if (typeof(TTo) == typeof(nint))
		{
			if (IntPtr.Size == sizeof(int))
			{
				nint converted = (nint)ConvertHalf<int>(value);
				return Unsafe.BitCast<nint, TTo>(converted);
			}
			else
			{
				nint converted = (nint)ConvertHalf<long>(value);
				return Unsafe.BitCast<nint, TTo>(converted);
			}
		}
		else if (typeof(TTo) == typeof(nuint))
		{
			if (IntPtr.Size == sizeof(int))
			{
				nuint converted = (nuint)ConvertHalf<uint>(value);
				return Unsafe.BitCast<nuint, TTo>(converted);
			}
			else
			{
				nuint converted = (nuint)ConvertHalf<ulong>(value);
				return Unsafe.BitCast<nuint, TTo>(converted);
			}
		}
		else if (typeof(TTo) == typeof(long))
		{
			long converted = ChangeSign(ConvertHalf<ulong>(value));
			return Unsafe.BitCast<long, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(ulong))
		{
			// We use double because it has enough precision to convert from Half to ulong.
			return ConvertDouble<TTo>((double)value);
		}
		else if (typeof(TTo) == typeof(Int128))
		{
			Int128 converted = ChangeSign(ConvertHalf<UInt128>(value));
			return Unsafe.BitCast<Int128, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(UInt128))
		{
			// We use double because it has enough precision to convert from Half to UInt128.
			return ConvertDouble<TTo>((double)value);
		}
		else if (typeof(TTo) == typeof(Half))
		{
			return Unsafe.BitCast<Half, TTo>(value);
		}
		else if (typeof(TTo) == typeof(float))
		{
			float converted = (float)value;
			return Unsafe.BitCast<float, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(NFloat))
		{
			if (IntPtr.Size == sizeof(int))
			{
				NFloat converted = (NFloat)ConvertHalf<float>(value);
				return Unsafe.BitCast<NFloat, TTo>(converted);
			}
			else
			{
				NFloat converted = (NFloat)ConvertHalf<double>(value);
				return Unsafe.BitCast<NFloat, TTo>(converted);
			}
		}
		else if (typeof(TTo) == typeof(double))
		{
			double converted = (double)value;
			return Unsafe.BitCast<double, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(decimal))
		{
			decimal converted = (decimal)value;
			return Unsafe.BitCast<decimal, TTo>(converted);
		}
		else
		{
			return ThrowOrReturnDefault<TTo>();
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	private static TTo ConvertSingle<TTo>(float value) where TTo : unmanaged
	{
		if (typeof(TTo) == typeof(sbyte))
		{
			sbyte converted = ChangeSign(ConvertSingle<byte>(value));
			return Unsafe.BitCast<sbyte, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(byte))
		{
			// x must be clamped because of rounding errors.
			float x = value * (float)byte.MaxValue;
			byte converted = (float)byte.MaxValue < x ? byte.MaxValue : (x > (float)byte.MinValue ? (byte)x : byte.MinValue);
			return Unsafe.BitCast<byte, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(short))
		{
			short converted = ChangeSign(ConvertSingle<ushort>(value));
			return Unsafe.BitCast<short, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(ushort))
		{
			// x must be clamped because of rounding errors.
			float x = value * (float)ushort.MaxValue;
			ushort converted = (float)ushort.MaxValue < x ? ushort.MaxValue : (x > (float)ushort.MinValue ? (ushort)x : ushort.MinValue);
			return Unsafe.BitCast<ushort, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(int))
		{
			int converted = ChangeSign(ConvertSingle<uint>(value));
			return Unsafe.BitCast<int, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(uint))
		{
			// We use double because it has enough precision to convert from float to uint.
			return ConvertDouble<TTo>((double)value);
		}
		else if (typeof(TTo) == typeof(nint))
		{
			if (IntPtr.Size == sizeof(int))
			{
				nint converted = (nint)ConvertSingle<int>(value);
				return Unsafe.BitCast<nint, TTo>(converted);
			}
			else
			{
				nint converted = (nint)ConvertSingle<long>(value);
				return Unsafe.BitCast<nint, TTo>(converted);
			}
		}
		else if (typeof(TTo) == typeof(nuint))
		{
			if (IntPtr.Size == sizeof(int))
			{
				nuint converted = (nuint)ConvertSingle<uint>(value);
				return Unsafe.BitCast<nuint, TTo>(converted);
			}
			else
			{
				nuint converted = (nuint)ConvertSingle<ulong>(value);
				return Unsafe.BitCast<nuint, TTo>(converted);
			}
		}
		else if (typeof(TTo) == typeof(long))
		{
			long converted = ChangeSign(ConvertSingle<ulong>(value));
			return Unsafe.BitCast<long, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(ulong))
		{
			// We use double because it has enough precision to convert from float to ulong.
			return ConvertDouble<TTo>((double)value);
		}
		else if (typeof(TTo) == typeof(Int128))
		{
			Int128 converted = ChangeSign(ConvertSingle<UInt128>(value));
			return Unsafe.BitCast<Int128, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(UInt128))
		{
			// We use double because it has enough precision to convert from float to UInt128.
			return ConvertDouble<TTo>((double)value);
		}
		else if (typeof(TTo) == typeof(Half))
		{
			Half converted = (Half)value;
			return Unsafe.BitCast<Half, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(float))
		{
			return Unsafe.BitCast<float, TTo>(value);
		}
		else if (typeof(TTo) == typeof(NFloat))
		{
			if (IntPtr.Size == sizeof(int))
			{
				NFloat converted = (NFloat)ConvertSingle<float>(value);
				return Unsafe.BitCast<NFloat, TTo>(converted);
			}
			else
			{
				NFloat converted = (NFloat)ConvertSingle<double>(value);
				return Unsafe.BitCast<NFloat, TTo>(converted);
			}
		}
		else if (typeof(TTo) == typeof(double))
		{
			double converted = (double)value;
			return Unsafe.BitCast<double, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(decimal))
		{
			decimal converted = (decimal)value;
			return Unsafe.BitCast<decimal, TTo>(converted);
		}
		else
		{
			return ThrowOrReturnDefault<TTo>();
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	private static TTo ConvertNFloat<TTo>(NFloat value) where TTo : unmanaged
	{
		if (IntPtr.Size == sizeof(int))
		{
			return ConvertSingle<TTo>((float)value);
		}
		else
		{
			return ConvertDouble<TTo>((double)value);
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	private static TTo ConvertDouble<TTo>(double value) where TTo : unmanaged
	{
		if (typeof(TTo) == typeof(sbyte))
		{
			sbyte converted = ChangeSign(ConvertDouble<byte>(value));
			return Unsafe.BitCast<sbyte, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(byte))
		{
			// x must be clamped because of rounding errors.
			double x = value * (double)byte.MaxValue;
			byte converted = (double)byte.MaxValue < x ? byte.MaxValue : (x > (double)byte.MinValue ? (byte)x : byte.MinValue);
			return Unsafe.BitCast<byte, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(short))
		{
			short converted = ChangeSign(ConvertDouble<ushort>(value));
			return Unsafe.BitCast<short, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(ushort))
		{
			// x must be clamped because of rounding errors.
			double x = value * (double)ushort.MaxValue;
			ushort converted = (double)ushort.MaxValue < x ? ushort.MaxValue : (x > (double)ushort.MinValue ? (ushort)x : ushort.MinValue);
			return Unsafe.BitCast<ushort, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(int))
		{
			int converted = ChangeSign(ConvertDouble<uint>(value));
			return Unsafe.BitCast<int, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(uint))
		{
			// x must be clamped because of rounding errors.
			double x = value * (double)uint.MaxValue;
			uint converted = (double)uint.MaxValue < x ? uint.MaxValue : (x > (double)uint.MinValue ? (uint)x : uint.MinValue);
			return Unsafe.BitCast<uint, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(nint))
		{
			if (IntPtr.Size == sizeof(int))
			{
				nint converted = (nint)ConvertDouble<int>(value);
				return Unsafe.BitCast<nint, TTo>(converted);
			}
			else
			{
				nint converted = (nint)ConvertDouble<long>(value);
				return Unsafe.BitCast<nint, TTo>(converted);
			}
		}
		else if (typeof(TTo) == typeof(nuint))
		{
			if (IntPtr.Size == sizeof(int))
			{
				nuint converted = (nuint)ConvertDouble<uint>(value);
				return Unsafe.BitCast<nuint, TTo>(converted);
			}
			else
			{
				nuint converted = (nuint)ConvertDouble<ulong>(value);
				return Unsafe.BitCast<nuint, TTo>(converted);
			}
		}
		else if (typeof(TTo) == typeof(long))
		{
			long converted = ChangeSign(ConvertDouble<ulong>(value));
			return Unsafe.BitCast<long, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(ulong))
		{
			// x must be clamped because of rounding errors.
			double x = value * (double)ulong.MaxValue;
			ulong converted = (double)ulong.MaxValue < x ? ulong.MaxValue : (x > (double)ulong.MinValue ? (ulong)x : ulong.MinValue);
			return Unsafe.BitCast<ulong, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(Int128))
		{
			Int128 converted = ChangeSign(ConvertDouble<UInt128>(value));
			return Unsafe.BitCast<Int128, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(UInt128))
		{
			// x must be clamped because of rounding errors.
			double x = value * (double)UInt128.MaxValue;
			UInt128 converted = (double)UInt128.MaxValue < x ? UInt128.MaxValue : (x > (double)UInt128.MinValue ? (UInt128)x : UInt128.MinValue);
			return Unsafe.BitCast<UInt128, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(Half))
		{
			Half converted = (Half)value;
			return Unsafe.BitCast<Half, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(float))
		{
			float converted = (float)value;
			return Unsafe.BitCast<float, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(NFloat))
		{
			if (IntPtr.Size == sizeof(int))
			{
				NFloat converted = (NFloat)ConvertDouble<float>(value);
				return Unsafe.BitCast<NFloat, TTo>(converted);
			}
			else
			{
				NFloat converted = (NFloat)ConvertDouble<double>(value);
				return Unsafe.BitCast<NFloat, TTo>(converted);
			}
		}
		else if (typeof(TTo) == typeof(double))
		{
			return Unsafe.BitCast<double, TTo>(value);
		}
		else if (typeof(TTo) == typeof(decimal))
		{
			decimal converted = (decimal)value;
			return Unsafe.BitCast<decimal, TTo>(converted);
		}
		else
		{
			return ThrowOrReturnDefault<TTo>();
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	private static TTo ConvertDecimal<TTo>(decimal value) where TTo : unmanaged
	{
		if (typeof(TTo) == typeof(sbyte))
		{
			sbyte converted = ChangeSign(ConvertDecimal<byte>(value));
			return Unsafe.BitCast<sbyte, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(byte))
		{
			// x must be clamped because of rounding errors.
			decimal x = value * (decimal)byte.MaxValue;
			byte converted = (decimal)byte.MaxValue < x ? byte.MaxValue : (x > (decimal)byte.MinValue ? (byte)x : byte.MinValue);
			return Unsafe.BitCast<byte, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(short))
		{
			short converted = ChangeSign(ConvertDecimal<ushort>(value));
			return Unsafe.BitCast<short, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(ushort))
		{
			// x must be clamped because of rounding errors.
			decimal x = value * (decimal)ushort.MaxValue;
			ushort converted = (decimal)ushort.MaxValue < x ? ushort.MaxValue : (x > (decimal)ushort.MinValue ? (ushort)x : ushort.MinValue);
			return Unsafe.BitCast<ushort, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(int))
		{
			int converted = ChangeSign(ConvertDecimal<uint>(value));
			return Unsafe.BitCast<int, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(uint))
		{
			// x must be clamped because of rounding errors.
			decimal x = value * (decimal)uint.MaxValue;
			uint converted = (decimal)uint.MaxValue < x ? uint.MaxValue : (x > (decimal)uint.MinValue ? (uint)x : uint.MinValue);
			return Unsafe.BitCast<uint, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(nint))
		{
			if (IntPtr.Size == sizeof(int))
			{
				nint converted = (nint)ConvertDecimal<int>(value);
				return Unsafe.BitCast<nint, TTo>(converted);
			}
			else
			{
				nint converted = (nint)ConvertDecimal<long>(value);
				return Unsafe.BitCast<nint, TTo>(converted);
			}
		}
		else if (typeof(TTo) == typeof(nuint))
		{
			if (IntPtr.Size == sizeof(int))
			{
				nuint converted = (nuint)ConvertDecimal<uint>(value);
				return Unsafe.BitCast<nuint, TTo>(converted);
			}
			else
			{
				nuint converted = (nuint)ConvertDecimal<ulong>(value);
				return Unsafe.BitCast<nuint, TTo>(converted);
			}
		}
		else if (typeof(TTo) == typeof(long))
		{
			long converted = ChangeSign(ConvertDecimal<ulong>(value));
			return Unsafe.BitCast<long, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(ulong))
		{
			// x must be clamped because of rounding errors.
			decimal x = value * (decimal)ulong.MaxValue;
			ulong converted = (decimal)ulong.MaxValue < x ? ulong.MaxValue : (x > (decimal)ulong.MinValue ? (ulong)x : ulong.MinValue);
			return Unsafe.BitCast<ulong, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(Int128))
		{
			Int128 converted = ChangeSign(ConvertDecimal<UInt128>(value));
			return Unsafe.BitCast<Int128, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(UInt128))
		{
			// x must be clamped because of rounding errors.
			decimal x = value * (decimal)UInt128.MaxValue;
			UInt128 converted = (decimal)UInt128.MaxValue < x ? UInt128.MaxValue : (x > (decimal)UInt128.MinValue ? (UInt128)x : UInt128.MinValue);
			return Unsafe.BitCast<UInt128, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(Half))
		{
			Half converted = (Half)value;
			return Unsafe.BitCast<Half, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(float))
		{
			float converted = (float)value;
			return Unsafe.BitCast<float, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(NFloat))
		{
			if (IntPtr.Size == sizeof(int))
			{
				NFloat converted = (NFloat)ConvertDecimal<float>(value);
				return Unsafe.BitCast<NFloat, TTo>(converted);
			}
			else
			{
				NFloat converted = (NFloat)ConvertDecimal<double>(value);
				return Unsafe.BitCast<NFloat, TTo>(converted);
			}
		}
		else if (typeof(TTo) == typeof(double))
		{
			double converted = (double)value;
			return Unsafe.BitCast<double, TTo>(converted);
		}
		else if (typeof(TTo) == typeof(decimal))
		{
			return Unsafe.BitCast<decimal, TTo>(value);
		}
		else
		{
			return ThrowOrReturnDefault<TTo>();
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	public static T GetMinimumValue<T>() where T : unmanaged
	{
		if (typeof(T) == typeof(sbyte))
		{
			sbyte value = GetMinimumValueSafe<sbyte>();
			return Unsafe.BitCast<sbyte, T>(value);
		}
		else if (typeof(T) == typeof(byte))
		{
			byte value = GetMinimumValueSafe<byte>();
			return Unsafe.BitCast<byte, T>(value);
		}
		else if (typeof(T) == typeof(short))
		{
			short value = GetMinimumValueSafe<short>();
			return Unsafe.BitCast<short, T>(value);
		}
		else if (typeof(T) == typeof(ushort))
		{
			ushort value = GetMinimumValueSafe<ushort>();
			return Unsafe.BitCast<ushort, T>(value);
		}
		else if (typeof(T) == typeof(int))
		{
			int value = GetMinimumValueSafe<int>();
			return Unsafe.BitCast<int, T>(value);
		}
		else if (typeof(T) == typeof(uint))
		{
			uint value = GetMinimumValueSafe<uint>();
			return Unsafe.BitCast<uint, T>(value);
		}
		else if (typeof(T) == typeof(nint))
		{
			nint value = GetMinimumValueSafe<nint>();
			return Unsafe.BitCast<nint, T>(value);
		}
		else if (typeof(T) == typeof(nuint))
		{
			nuint value = GetMinimumValueSafe<nuint>();
			return Unsafe.BitCast<nuint, T>(value);
		}
		else if (typeof(T) == typeof(long))
		{
			long value = GetMinimumValueSafe<long>();
			return Unsafe.BitCast<long, T>(value);
		}
		else if (typeof(T) == typeof(ulong))
		{
			ulong value = GetMinimumValueSafe<ulong>();
			return Unsafe.BitCast<ulong, T>(value);
		}
		else if (typeof(T) == typeof(Int128))
		{
			Int128 value = GetMinimumValueSafe<Int128>();
			return Unsafe.BitCast<Int128, T>(value);
		}
		else if (typeof(T) == typeof(UInt128))
		{
			UInt128 value = GetMinimumValueSafe<UInt128>();
			return Unsafe.BitCast<UInt128, T>(value);
		}
		else if (typeof(T) == typeof(Half))
		{
			Half value = GetMinimumValueSafe<Half>();
			return Unsafe.BitCast<Half, T>(value);
		}
		else if (typeof(T) == typeof(float))
		{
			float value = GetMinimumValueSafe<float>();
			return Unsafe.BitCast<float, T>(value);
		}
		else if (typeof(T) == typeof(NFloat))
		{
			NFloat value = GetMinimumValueSafe<NFloat>();
			return Unsafe.BitCast<NFloat, T>(value);
		}
		else if (typeof(T) == typeof(double))
		{
			double value = GetMinimumValueSafe<double>();
			return Unsafe.BitCast<double, T>(value);
		}
		else if (typeof(T) == typeof(decimal))
		{
			decimal value = GetMinimumValueSafe<decimal>();
			return Unsafe.BitCast<decimal, T>(value);
		}
		else
		{
			return ThrowOrReturnDefault<T>();
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	public static T GetMaximumValue<T>() where T : unmanaged
	{
		if (typeof(T) == typeof(sbyte))
		{
			sbyte value = GetMaximumValueSafe<sbyte>();
			return Unsafe.BitCast<sbyte, T>(value);
		}
		else if (typeof(T) == typeof(byte))
		{
			byte value = GetMaximumValueSafe<byte>();
			return Unsafe.BitCast<byte, T>(value);
		}
		else if (typeof(T) == typeof(short))
		{
			short value = GetMaximumValueSafe<short>();
			return Unsafe.BitCast<short, T>(value);
		}
		else if (typeof(T) == typeof(ushort))
		{
			ushort value = GetMaximumValueSafe<ushort>();
			return Unsafe.BitCast<ushort, T>(value);
		}
		else if (typeof(T) == typeof(int))
		{
			int value = GetMaximumValueSafe<int>();
			return Unsafe.BitCast<int, T>(value);
		}
		else if (typeof(T) == typeof(uint))
		{
			uint value = GetMaximumValueSafe<uint>();
			return Unsafe.BitCast<uint, T>(value);
		}
		else if (typeof(T) == typeof(nint))
		{
			nint value = GetMaximumValueSafe<nint>();
			return Unsafe.BitCast<nint, T>(value);
		}
		else if (typeof(T) == typeof(nuint))
		{
			nuint value = GetMaximumValueSafe<nuint>();
			return Unsafe.BitCast<nuint, T>(value);
		}
		else if (typeof(T) == typeof(long))
		{
			long value = GetMaximumValueSafe<long>();
			return Unsafe.BitCast<long, T>(value);
		}
		else if (typeof(T) == typeof(ulong))
		{
			ulong value = GetMaximumValueSafe<ulong>();
			return Unsafe.BitCast<ulong, T>(value);
		}
		else if (typeof(T) == typeof(Int128))
		{
			Int128 value = GetMaximumValueSafe<Int128>();
			return Unsafe.BitCast<Int128, T>(value);
		}
		else if (typeof(T) == typeof(UInt128))
		{
			UInt128 value = GetMaximumValueSafe<UInt128>();
			return Unsafe.BitCast<UInt128, T>(value);
		}
		else if (typeof(T) == typeof(Half))
		{
			Half value = GetMaximumValueSafe<Half>();
			return Unsafe.BitCast<Half, T>(value);
		}
		else if (typeof(T) == typeof(float))
		{
			float value = GetMaximumValueSafe<float>();
			return Unsafe.BitCast<float, T>(value);
		}
		else if (typeof(T) == typeof(NFloat))
		{
			NFloat value = GetMaximumValueSafe<NFloat>();
			return Unsafe.BitCast<NFloat, T>(value);
		}
		else if (typeof(T) == typeof(double))
		{
			double value = GetMaximumValueSafe<double>();
			return Unsafe.BitCast<double, T>(value);
		}
		else if (typeof(T) == typeof(decimal))
		{
			decimal value = GetMaximumValueSafe<decimal>();
			return Unsafe.BitCast<decimal, T>(value);
		}
		else
		{
			return ThrowOrReturnDefault<T>();
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	private static sbyte ChangeSign(byte value)
	{
		return ToSignedNumber<byte, sbyte>(value);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	private static byte ChangeSign(sbyte value)
	{
		return ToUnsignedNumber<sbyte, byte>(value);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	private static short ChangeSign(ushort value)
	{
		return ToSignedNumber<ushort, short>(value);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	private static ushort ChangeSign(short value)
	{
		return ToUnsignedNumber<short, ushort>(value);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	private static int ChangeSign(uint value)
	{
		return ToSignedNumber<uint, int>(value);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	private static uint ChangeSign(int value)
	{
		return ToUnsignedNumber<int, uint>(value);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	private static long ChangeSign(ulong value)
	{
		return ToSignedNumber<ulong, long>(value);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	private static ulong ChangeSign(long value)
	{
		return ToUnsignedNumber<long, ulong>(value);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	private static Int128 ChangeSign(UInt128 value)
	{
		return ToSignedNumber<UInt128, Int128>(value);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	private static UInt128 ChangeSign(Int128 value)
	{
		return ToUnsignedNumber<Int128, UInt128>(value);
	}

}
