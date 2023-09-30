// Auto-generated code. Do not modify manually.

namespace AssetRipper.TextureDecoder.Rgb;

static partial class NumericConversion
{
	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	public static TTo Convert<TFrom, TTo>(TFrom value) where TFrom : unmanaged where TTo : unmanaged
	{
		if (typeof(TFrom) == typeof(TTo))
		{
			return Unsafe.As<TFrom, TTo>(ref value);
		}
		else if (typeof(TFrom) == typeof(sbyte))
		{
			return ConvertSByte<TTo>(Unsafe.As<TFrom, sbyte>(ref value));
		}
		else if (typeof(TFrom) == typeof(byte))
		{
			return ConvertByte<TTo>(Unsafe.As<TFrom, byte>(ref value));
		}
		else if (typeof(TFrom) == typeof(short))
		{
			return ConvertInt16<TTo>(Unsafe.As<TFrom, short>(ref value));
		}
		else if (typeof(TFrom) == typeof(ushort))
		{
			return ConvertUInt16<TTo>(Unsafe.As<TFrom, ushort>(ref value));
		}
		else if (typeof(TFrom) == typeof(int))
		{
			return ConvertInt32<TTo>(Unsafe.As<TFrom, int>(ref value));
		}
		else if (typeof(TFrom) == typeof(uint))
		{
			return ConvertUInt32<TTo>(Unsafe.As<TFrom, uint>(ref value));
		}
		else if (typeof(TFrom) == typeof(long))
		{
			return ConvertInt64<TTo>(Unsafe.As<TFrom, long>(ref value));
		}
		else if (typeof(TFrom) == typeof(ulong))
		{
			return ConvertUInt64<TTo>(Unsafe.As<TFrom, ulong>(ref value));
		}
		else if (typeof(TFrom) == typeof(Int128))
		{
			return ConvertInt128<TTo>(Unsafe.As<TFrom, Int128>(ref value));
		}
		else if (typeof(TFrom) == typeof(UInt128))
		{
			return ConvertUInt128<TTo>(Unsafe.As<TFrom, UInt128>(ref value));
		}
		else if (typeof(TFrom) == typeof(Half))
		{
			return ConvertHalf<TTo>(Unsafe.As<TFrom, Half>(ref value));
		}
		else if (typeof(TFrom) == typeof(float))
		{
			return ConvertSingle<TTo>(Unsafe.As<TFrom, float>(ref value));
		}
		else if (typeof(TFrom) == typeof(double))
		{
			return ConvertDouble<TTo>(Unsafe.As<TFrom, double>(ref value));
		}
		else if (typeof(TFrom) == typeof(decimal))
		{
			return ConvertDecimal<TTo>(Unsafe.As<TFrom, decimal>(ref value));
		}
		else
		{
			return ThrowOrReturnDefault<TTo>();
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	private static TTo ConvertSByte<TTo>(sbyte value) where TTo : unmanaged
	{
		byte unsigned = ChangeSign(value);
		return ConvertByte<TTo>(unsigned);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	private static TTo ConvertByte<TTo>(byte value) where TTo : unmanaged
	{
		if (typeof(TTo) == typeof(sbyte))
		{
			sbyte converted = ChangeSign(ConvertByte<byte>(value));
			return Unsafe.As<sbyte, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(byte))
		{
			return Unsafe.As<byte, TTo>(ref value);
		}
		else if (typeof(TTo) == typeof(short))
		{
			short converted = ChangeSign(ConvertByte<ushort>(value));
			return Unsafe.As<short, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(ushort))
		{
			// See https://github.com/AssetRipper/TextureDecoder/issues/19
			unchecked
			{
				ushort converted = (ushort)(((uint)value << 8) | value);
				return Unsafe.As<ushort, TTo>(ref converted);
			}
		}
		else if (typeof(TTo) == typeof(int))
		{
			int converted = ChangeSign(ConvertByte<uint>(value));
			return Unsafe.As<int, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(uint))
		{
			// See https://github.com/AssetRipper/TextureDecoder/issues/19
			unchecked
			{
				uint converted = (uint)(((uint)value << 24) | ((uint)value << 16) | ((uint)value << 8) | value);
				return Unsafe.As<uint, TTo>(ref converted);
			}
		}
		else if (typeof(TTo) == typeof(long))
		{
			long converted = ChangeSign(ConvertByte<ulong>(value));
			return Unsafe.As<long, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(ulong))
		{
			// See https://github.com/AssetRipper/TextureDecoder/issues/19
			unchecked
			{
				ulong converted = (ulong)(((ulong)value << 56) | ((ulong)value << 48) | ((ulong)value << 40) | ((ulong)value << 32) | ((ulong)value << 24) | ((ulong)value << 16) | ((ulong)value << 8) | value);
				return Unsafe.As<ulong, TTo>(ref converted);
			}
		}
		else if (typeof(TTo) == typeof(Int128))
		{
			Int128 converted = ChangeSign(ConvertByte<UInt128>(value));
			return Unsafe.As<Int128, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(UInt128))
		{
			// See https://github.com/AssetRipper/TextureDecoder/issues/19
			unchecked
			{
				UInt128 converted = (UInt128)(((UInt128)value << 120) | ((UInt128)value << 112) | ((UInt128)value << 104) | ((UInt128)value << 96) | ((UInt128)value << 88) | ((UInt128)value << 80) | ((UInt128)value << 72) | ((UInt128)value << 64) | ((UInt128)value << 56) | ((UInt128)value << 48) | ((UInt128)value << 40) | ((UInt128)value << 32) | ((UInt128)value << 24) | ((UInt128)value << 16) | ((UInt128)value << 8) | value);
				return Unsafe.As<UInt128, TTo>(ref converted);
			}
		}
		else if (typeof(TTo) == typeof(Half))
		{
			// There isn't enough precision to convert from anything bigger than byte to Half, so we convert to float first.
			float x = ConvertByte<float>(value);
			Half converted = (Half)x;
			return Unsafe.As<Half, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(float))
		{
			float converted = (float)value / (float)byte.MaxValue;
			return Unsafe.As<float, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(double))
		{
			double converted = (double)value / (double)byte.MaxValue;
			return Unsafe.As<double, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(decimal))
		{
			decimal converted = (decimal)value / (decimal)byte.MaxValue;
			return Unsafe.As<decimal, TTo>(ref converted);
		}
		else
		{
			return ThrowOrReturnDefault<TTo>();
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	private static TTo ConvertInt16<TTo>(short value) where TTo : unmanaged
	{
		ushort unsigned = ChangeSign(value);
		return ConvertUInt16<TTo>(unsigned);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	private static TTo ConvertUInt16<TTo>(ushort value) where TTo : unmanaged
	{
		if (typeof(TTo) == typeof(sbyte))
		{
			sbyte converted = ChangeSign(ConvertUInt16<byte>(value));
			return Unsafe.As<sbyte, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(byte))
		{
			// See https://github.com/AssetRipper/TextureDecoder/issues/19
			// This is a special case where we already know an optimal algorithm.
			uint x = (value * 255u + 32895u) >> 16;
			byte converted = unchecked((byte)x);
			return Unsafe.As<byte, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(short))
		{
			short converted = ChangeSign(ConvertUInt16<ushort>(value));
			return Unsafe.As<short, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(ushort))
		{
			return Unsafe.As<ushort, TTo>(ref value);
		}
		else if (typeof(TTo) == typeof(int))
		{
			int converted = ChangeSign(ConvertUInt16<uint>(value));
			return Unsafe.As<int, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(uint))
		{
			// See https://github.com/AssetRipper/TextureDecoder/issues/19
			unchecked
			{
				uint converted = (uint)(((uint)value << 16) | value);
				return Unsafe.As<uint, TTo>(ref converted);
			}
		}
		else if (typeof(TTo) == typeof(long))
		{
			long converted = ChangeSign(ConvertUInt16<ulong>(value));
			return Unsafe.As<long, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(ulong))
		{
			// See https://github.com/AssetRipper/TextureDecoder/issues/19
			unchecked
			{
				ulong converted = (ulong)(((ulong)value << 48) | ((ulong)value << 32) | ((ulong)value << 16) | value);
				return Unsafe.As<ulong, TTo>(ref converted);
			}
		}
		else if (typeof(TTo) == typeof(Int128))
		{
			Int128 converted = ChangeSign(ConvertUInt16<UInt128>(value));
			return Unsafe.As<Int128, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(UInt128))
		{
			// See https://github.com/AssetRipper/TextureDecoder/issues/19
			unchecked
			{
				UInt128 converted = (UInt128)(((UInt128)value << 112) | ((UInt128)value << 96) | ((UInt128)value << 80) | ((UInt128)value << 64) | ((UInt128)value << 48) | ((UInt128)value << 32) | ((UInt128)value << 16) | value);
				return Unsafe.As<UInt128, TTo>(ref converted);
			}
		}
		else if (typeof(TTo) == typeof(Half))
		{
			// There isn't enough precision to convert from anything bigger than byte to Half, so we convert to float first.
			float x = ConvertUInt16<float>(value);
			Half converted = (Half)x;
			return Unsafe.As<Half, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(float))
		{
			float converted = (float)value / (float)ushort.MaxValue;
			return Unsafe.As<float, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(double))
		{
			double converted = (double)value / (double)ushort.MaxValue;
			return Unsafe.As<double, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(decimal))
		{
			decimal converted = (decimal)value / (decimal)ushort.MaxValue;
			return Unsafe.As<decimal, TTo>(ref converted);
		}
		else
		{
			return ThrowOrReturnDefault<TTo>();
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	private static TTo ConvertInt32<TTo>(int value) where TTo : unmanaged
	{
		uint unsigned = ChangeSign(value);
		return ConvertUInt32<TTo>(unsigned);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	private static TTo ConvertUInt32<TTo>(uint value) where TTo : unmanaged
	{
		if (typeof(TTo) == typeof(sbyte))
		{
			sbyte converted = ChangeSign(ConvertUInt32<byte>(value));
			return Unsafe.As<sbyte, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(byte))
		{
			// See https://github.com/AssetRipper/TextureDecoder/issues/19
			// There are more accurate ways to map UInt32 onto Byte, but this is the simplest.
			unchecked
			{
				byte converted = (byte)((uint)value >> 24);
				return Unsafe.As<byte, TTo>(ref converted);
			}
		}
		else if (typeof(TTo) == typeof(short))
		{
			short converted = ChangeSign(ConvertUInt32<ushort>(value));
			return Unsafe.As<short, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(ushort))
		{
			// See https://github.com/AssetRipper/TextureDecoder/issues/19
			// There are more accurate ways to map UInt32 onto UInt16, but this is the simplest.
			unchecked
			{
				ushort converted = (ushort)((uint)value >> 16);
				return Unsafe.As<ushort, TTo>(ref converted);
			}
		}
		else if (typeof(TTo) == typeof(int))
		{
			int converted = ChangeSign(ConvertUInt32<uint>(value));
			return Unsafe.As<int, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(uint))
		{
			return Unsafe.As<uint, TTo>(ref value);
		}
		else if (typeof(TTo) == typeof(long))
		{
			long converted = ChangeSign(ConvertUInt32<ulong>(value));
			return Unsafe.As<long, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(ulong))
		{
			// See https://github.com/AssetRipper/TextureDecoder/issues/19
			unchecked
			{
				ulong converted = (ulong)(((ulong)value << 32) | value);
				return Unsafe.As<ulong, TTo>(ref converted);
			}
		}
		else if (typeof(TTo) == typeof(Int128))
		{
			Int128 converted = ChangeSign(ConvertUInt32<UInt128>(value));
			return Unsafe.As<Int128, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(UInt128))
		{
			// See https://github.com/AssetRipper/TextureDecoder/issues/19
			unchecked
			{
				UInt128 converted = (UInt128)(((UInt128)value << 96) | ((UInt128)value << 64) | ((UInt128)value << 32) | value);
				return Unsafe.As<UInt128, TTo>(ref converted);
			}
		}
		else if (typeof(TTo) == typeof(Half))
		{
			// There isn't enough precision to convert from anything bigger than byte to Half, so we convert to float first.
			float x = ConvertUInt32<float>(value);
			Half converted = (Half)x;
			return Unsafe.As<Half, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(float))
		{
			float converted = (float)value / (float)uint.MaxValue;
			return Unsafe.As<float, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(double))
		{
			double converted = (double)value / (double)uint.MaxValue;
			return Unsafe.As<double, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(decimal))
		{
			decimal converted = (decimal)value / (decimal)uint.MaxValue;
			return Unsafe.As<decimal, TTo>(ref converted);
		}
		else
		{
			return ThrowOrReturnDefault<TTo>();
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	private static TTo ConvertInt64<TTo>(long value) where TTo : unmanaged
	{
		ulong unsigned = ChangeSign(value);
		return ConvertUInt64<TTo>(unsigned);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	private static TTo ConvertUInt64<TTo>(ulong value) where TTo : unmanaged
	{
		if (typeof(TTo) == typeof(sbyte))
		{
			sbyte converted = ChangeSign(ConvertUInt64<byte>(value));
			return Unsafe.As<sbyte, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(byte))
		{
			// See https://github.com/AssetRipper/TextureDecoder/issues/19
			// There are more accurate ways to map UInt64 onto Byte, but this is the simplest.
			unchecked
			{
				byte converted = (byte)((ulong)value >> 56);
				return Unsafe.As<byte, TTo>(ref converted);
			}
		}
		else if (typeof(TTo) == typeof(short))
		{
			short converted = ChangeSign(ConvertUInt64<ushort>(value));
			return Unsafe.As<short, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(ushort))
		{
			// See https://github.com/AssetRipper/TextureDecoder/issues/19
			// There are more accurate ways to map UInt64 onto UInt16, but this is the simplest.
			unchecked
			{
				ushort converted = (ushort)((ulong)value >> 48);
				return Unsafe.As<ushort, TTo>(ref converted);
			}
		}
		else if (typeof(TTo) == typeof(int))
		{
			int converted = ChangeSign(ConvertUInt64<uint>(value));
			return Unsafe.As<int, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(uint))
		{
			// See https://github.com/AssetRipper/TextureDecoder/issues/19
			// There are more accurate ways to map UInt64 onto UInt32, but this is the simplest.
			unchecked
			{
				uint converted = (uint)((ulong)value >> 32);
				return Unsafe.As<uint, TTo>(ref converted);
			}
		}
		else if (typeof(TTo) == typeof(long))
		{
			long converted = ChangeSign(ConvertUInt64<ulong>(value));
			return Unsafe.As<long, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(ulong))
		{
			return Unsafe.As<ulong, TTo>(ref value);
		}
		else if (typeof(TTo) == typeof(Int128))
		{
			Int128 converted = ChangeSign(ConvertUInt64<UInt128>(value));
			return Unsafe.As<Int128, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(UInt128))
		{
			// See https://github.com/AssetRipper/TextureDecoder/issues/19
			unchecked
			{
				UInt128 converted = (UInt128)(((UInt128)value << 64) | value);
				return Unsafe.As<UInt128, TTo>(ref converted);
			}
		}
		else if (typeof(TTo) == typeof(Half))
		{
			// There isn't enough precision to convert from anything bigger than byte to Half, so we convert to float first.
			float x = ConvertUInt64<float>(value);
			Half converted = (Half)x;
			return Unsafe.As<Half, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(float))
		{
			float converted = (float)value / (float)ulong.MaxValue;
			return Unsafe.As<float, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(double))
		{
			double converted = (double)value / (double)ulong.MaxValue;
			return Unsafe.As<double, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(decimal))
		{
			decimal converted = (decimal)value / (decimal)ulong.MaxValue;
			return Unsafe.As<decimal, TTo>(ref converted);
		}
		else
		{
			return ThrowOrReturnDefault<TTo>();
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	private static TTo ConvertInt128<TTo>(Int128 value) where TTo : unmanaged
	{
		UInt128 unsigned = ChangeSign(value);
		return ConvertUInt128<TTo>(unsigned);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	private static TTo ConvertUInt128<TTo>(UInt128 value) where TTo : unmanaged
	{
		if (typeof(TTo) == typeof(sbyte))
		{
			sbyte converted = ChangeSign(ConvertUInt128<byte>(value));
			return Unsafe.As<sbyte, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(byte))
		{
			// See https://github.com/AssetRipper/TextureDecoder/issues/19
			// There are more accurate ways to map UInt128 onto Byte, but this is the simplest.
			unchecked
			{
				byte converted = (byte)((UInt128)value >> 120);
				return Unsafe.As<byte, TTo>(ref converted);
			}
		}
		else if (typeof(TTo) == typeof(short))
		{
			short converted = ChangeSign(ConvertUInt128<ushort>(value));
			return Unsafe.As<short, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(ushort))
		{
			// See https://github.com/AssetRipper/TextureDecoder/issues/19
			// There are more accurate ways to map UInt128 onto UInt16, but this is the simplest.
			unchecked
			{
				ushort converted = (ushort)((UInt128)value >> 112);
				return Unsafe.As<ushort, TTo>(ref converted);
			}
		}
		else if (typeof(TTo) == typeof(int))
		{
			int converted = ChangeSign(ConvertUInt128<uint>(value));
			return Unsafe.As<int, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(uint))
		{
			// See https://github.com/AssetRipper/TextureDecoder/issues/19
			// There are more accurate ways to map UInt128 onto UInt32, but this is the simplest.
			unchecked
			{
				uint converted = (uint)((UInt128)value >> 96);
				return Unsafe.As<uint, TTo>(ref converted);
			}
		}
		else if (typeof(TTo) == typeof(long))
		{
			long converted = ChangeSign(ConvertUInt128<ulong>(value));
			return Unsafe.As<long, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(ulong))
		{
			// See https://github.com/AssetRipper/TextureDecoder/issues/19
			// There are more accurate ways to map UInt128 onto UInt64, but this is the simplest.
			unchecked
			{
				ulong converted = (ulong)((UInt128)value >> 64);
				return Unsafe.As<ulong, TTo>(ref converted);
			}
		}
		else if (typeof(TTo) == typeof(Int128))
		{
			Int128 converted = ChangeSign(ConvertUInt128<UInt128>(value));
			return Unsafe.As<Int128, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(UInt128))
		{
			return Unsafe.As<UInt128, TTo>(ref value);
		}
		else if (typeof(TTo) == typeof(Half))
		{
			// There isn't enough precision to convert from anything bigger than byte to Half, so we convert to float first.
			float x = ConvertUInt128<float>(value);
			Half converted = (Half)x;
			return Unsafe.As<Half, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(float))
		{
			float converted = (float)value / (float)UInt128.MaxValue;
			return Unsafe.As<float, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(double))
		{
			double converted = (double)value / (double)UInt128.MaxValue;
			return Unsafe.As<double, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(decimal))
		{
			decimal converted = (decimal)value / (decimal)UInt128.MaxValue;
			return Unsafe.As<decimal, TTo>(ref converted);
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
			return Unsafe.As<sbyte, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(byte))
		{
			// We use float because it has enough precision to convert from Half to any integer type.
			return ConvertSingle<TTo>((float)value);
		}
		else if (typeof(TTo) == typeof(short))
		{
			short converted = ChangeSign(ConvertHalf<ushort>(value));
			return Unsafe.As<short, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(ushort))
		{
			// We use float because it has enough precision to convert from Half to any integer type.
			return ConvertSingle<TTo>((float)value);
		}
		else if (typeof(TTo) == typeof(int))
		{
			int converted = ChangeSign(ConvertHalf<uint>(value));
			return Unsafe.As<int, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(uint))
		{
			// We use float because it has enough precision to convert from Half to any integer type.
			return ConvertSingle<TTo>((float)value);
		}
		else if (typeof(TTo) == typeof(long))
		{
			long converted = ChangeSign(ConvertHalf<ulong>(value));
			return Unsafe.As<long, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(ulong))
		{
			// We use float because it has enough precision to convert from Half to any integer type.
			return ConvertSingle<TTo>((float)value);
		}
		else if (typeof(TTo) == typeof(Int128))
		{
			Int128 converted = ChangeSign(ConvertHalf<UInt128>(value));
			return Unsafe.As<Int128, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(UInt128))
		{
			// We use float because it has enough precision to convert from Half to any integer type.
			return ConvertSingle<TTo>((float)value);
		}
		else if (typeof(TTo) == typeof(Half))
		{
			return Unsafe.As<Half, TTo>(ref value);
		}
		else if (typeof(TTo) == typeof(float))
		{
			float converted = (float)value;
			return Unsafe.As<float, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(double))
		{
			double converted = (double)value;
			return Unsafe.As<double, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(decimal))
		{
			decimal converted = (decimal)value;
			return Unsafe.As<decimal, TTo>(ref converted);
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
			return Unsafe.As<sbyte, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(byte))
		{
			// x must be clamped because of rounding errors.
			float x = value * (float)byte.MaxValue;
			byte converted = (float)byte.MaxValue < x ? byte.MaxValue : (x > (float)byte.MinValue ? (byte)x : byte.MinValue);
			return Unsafe.As<byte, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(short))
		{
			short converted = ChangeSign(ConvertSingle<ushort>(value));
			return Unsafe.As<short, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(ushort))
		{
			// x must be clamped because of rounding errors.
			float x = value * (float)ushort.MaxValue;
			ushort converted = (float)ushort.MaxValue < x ? ushort.MaxValue : (x > (float)ushort.MinValue ? (ushort)x : ushort.MinValue);
			return Unsafe.As<ushort, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(int))
		{
			int converted = ChangeSign(ConvertSingle<uint>(value));
			return Unsafe.As<int, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(uint))
		{
			// x must be clamped because of rounding errors.
			float x = value * (float)uint.MaxValue;
			uint converted = (float)uint.MaxValue < x ? uint.MaxValue : (x > (float)uint.MinValue ? (uint)x : uint.MinValue);
			return Unsafe.As<uint, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(long))
		{
			long converted = ChangeSign(ConvertSingle<ulong>(value));
			return Unsafe.As<long, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(ulong))
		{
			// x must be clamped because of rounding errors.
			float x = value * (float)ulong.MaxValue;
			ulong converted = (float)ulong.MaxValue < x ? ulong.MaxValue : (x > (float)ulong.MinValue ? (ulong)x : ulong.MinValue);
			return Unsafe.As<ulong, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(Int128))
		{
			Int128 converted = ChangeSign(ConvertSingle<UInt128>(value));
			return Unsafe.As<Int128, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(UInt128))
		{
			// x must be clamped because of rounding errors.
			float x = value * (float)UInt128.MaxValue;
			UInt128 converted = (float)UInt128.MaxValue < x ? UInt128.MaxValue : (x > (float)UInt128.MinValue ? (UInt128)x : UInt128.MinValue);
			return Unsafe.As<UInt128, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(Half))
		{
			Half converted = (Half)value;
			return Unsafe.As<Half, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(float))
		{
			return Unsafe.As<float, TTo>(ref value);
		}
		else if (typeof(TTo) == typeof(double))
		{
			double converted = (double)value;
			return Unsafe.As<double, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(decimal))
		{
			decimal converted = (decimal)value;
			return Unsafe.As<decimal, TTo>(ref converted);
		}
		else
		{
			return ThrowOrReturnDefault<TTo>();
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	private static TTo ConvertDouble<TTo>(double value) where TTo : unmanaged
	{
		if (typeof(TTo) == typeof(sbyte))
		{
			sbyte converted = ChangeSign(ConvertDouble<byte>(value));
			return Unsafe.As<sbyte, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(byte))
		{
			// x must be clamped because of rounding errors.
			double x = value * (double)byte.MaxValue;
			byte converted = (double)byte.MaxValue < x ? byte.MaxValue : (x > (double)byte.MinValue ? (byte)x : byte.MinValue);
			return Unsafe.As<byte, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(short))
		{
			short converted = ChangeSign(ConvertDouble<ushort>(value));
			return Unsafe.As<short, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(ushort))
		{
			// x must be clamped because of rounding errors.
			double x = value * (double)ushort.MaxValue;
			ushort converted = (double)ushort.MaxValue < x ? ushort.MaxValue : (x > (double)ushort.MinValue ? (ushort)x : ushort.MinValue);
			return Unsafe.As<ushort, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(int))
		{
			int converted = ChangeSign(ConvertDouble<uint>(value));
			return Unsafe.As<int, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(uint))
		{
			// x must be clamped because of rounding errors.
			double x = value * (double)uint.MaxValue;
			uint converted = (double)uint.MaxValue < x ? uint.MaxValue : (x > (double)uint.MinValue ? (uint)x : uint.MinValue);
			return Unsafe.As<uint, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(long))
		{
			long converted = ChangeSign(ConvertDouble<ulong>(value));
			return Unsafe.As<long, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(ulong))
		{
			// x must be clamped because of rounding errors.
			double x = value * (double)ulong.MaxValue;
			ulong converted = (double)ulong.MaxValue < x ? ulong.MaxValue : (x > (double)ulong.MinValue ? (ulong)x : ulong.MinValue);
			return Unsafe.As<ulong, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(Int128))
		{
			Int128 converted = ChangeSign(ConvertDouble<UInt128>(value));
			return Unsafe.As<Int128, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(UInt128))
		{
			// x must be clamped because of rounding errors.
			double x = value * (double)UInt128.MaxValue;
			UInt128 converted = (double)UInt128.MaxValue < x ? UInt128.MaxValue : (x > (double)UInt128.MinValue ? (UInt128)x : UInt128.MinValue);
			return Unsafe.As<UInt128, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(Half))
		{
			Half converted = (Half)value;
			return Unsafe.As<Half, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(float))
		{
			float converted = (float)value;
			return Unsafe.As<float, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(double))
		{
			return Unsafe.As<double, TTo>(ref value);
		}
		else if (typeof(TTo) == typeof(decimal))
		{
			decimal converted = (decimal)value;
			return Unsafe.As<decimal, TTo>(ref converted);
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
			return Unsafe.As<sbyte, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(byte))
		{
			// x must be clamped because of rounding errors.
			decimal x = value * (decimal)byte.MaxValue;
			byte converted = (decimal)byte.MaxValue < x ? byte.MaxValue : (x > (decimal)byte.MinValue ? (byte)x : byte.MinValue);
			return Unsafe.As<byte, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(short))
		{
			short converted = ChangeSign(ConvertDecimal<ushort>(value));
			return Unsafe.As<short, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(ushort))
		{
			// x must be clamped because of rounding errors.
			decimal x = value * (decimal)ushort.MaxValue;
			ushort converted = (decimal)ushort.MaxValue < x ? ushort.MaxValue : (x > (decimal)ushort.MinValue ? (ushort)x : ushort.MinValue);
			return Unsafe.As<ushort, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(int))
		{
			int converted = ChangeSign(ConvertDecimal<uint>(value));
			return Unsafe.As<int, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(uint))
		{
			// x must be clamped because of rounding errors.
			decimal x = value * (decimal)uint.MaxValue;
			uint converted = (decimal)uint.MaxValue < x ? uint.MaxValue : (x > (decimal)uint.MinValue ? (uint)x : uint.MinValue);
			return Unsafe.As<uint, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(long))
		{
			long converted = ChangeSign(ConvertDecimal<ulong>(value));
			return Unsafe.As<long, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(ulong))
		{
			// x must be clamped because of rounding errors.
			decimal x = value * (decimal)ulong.MaxValue;
			ulong converted = (decimal)ulong.MaxValue < x ? ulong.MaxValue : (x > (decimal)ulong.MinValue ? (ulong)x : ulong.MinValue);
			return Unsafe.As<ulong, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(Int128))
		{
			Int128 converted = ChangeSign(ConvertDecimal<UInt128>(value));
			return Unsafe.As<Int128, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(UInt128))
		{
			// x must be clamped because of rounding errors.
			decimal x = value * (decimal)UInt128.MaxValue;
			UInt128 converted = (decimal)UInt128.MaxValue < x ? UInt128.MaxValue : (x > (decimal)UInt128.MinValue ? (UInt128)x : UInt128.MinValue);
			return Unsafe.As<UInt128, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(Half))
		{
			Half converted = (Half)value;
			return Unsafe.As<Half, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(float))
		{
			float converted = (float)value;
			return Unsafe.As<float, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(double))
		{
			double converted = (double)value;
			return Unsafe.As<double, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(decimal))
		{
			return Unsafe.As<decimal, TTo>(ref value);
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
			return Unsafe.As<sbyte, T>(ref value);
		}
		else if (typeof(T) == typeof(byte))
		{
			byte value = GetMinimumValueSafe<byte>();
			return Unsafe.As<byte, T>(ref value);
		}
		else if (typeof(T) == typeof(short))
		{
			short value = GetMinimumValueSafe<short>();
			return Unsafe.As<short, T>(ref value);
		}
		else if (typeof(T) == typeof(ushort))
		{
			ushort value = GetMinimumValueSafe<ushort>();
			return Unsafe.As<ushort, T>(ref value);
		}
		else if (typeof(T) == typeof(int))
		{
			int value = GetMinimumValueSafe<int>();
			return Unsafe.As<int, T>(ref value);
		}
		else if (typeof(T) == typeof(uint))
		{
			uint value = GetMinimumValueSafe<uint>();
			return Unsafe.As<uint, T>(ref value);
		}
		else if (typeof(T) == typeof(long))
		{
			long value = GetMinimumValueSafe<long>();
			return Unsafe.As<long, T>(ref value);
		}
		else if (typeof(T) == typeof(ulong))
		{
			ulong value = GetMinimumValueSafe<ulong>();
			return Unsafe.As<ulong, T>(ref value);
		}
		else if (typeof(T) == typeof(Int128))
		{
			Int128 value = GetMinimumValueSafe<Int128>();
			return Unsafe.As<Int128, T>(ref value);
		}
		else if (typeof(T) == typeof(UInt128))
		{
			UInt128 value = GetMinimumValueSafe<UInt128>();
			return Unsafe.As<UInt128, T>(ref value);
		}
		else if (typeof(T) == typeof(Half))
		{
			Half value = GetMinimumValueSafe<Half>();
			return Unsafe.As<Half, T>(ref value);
		}
		else if (typeof(T) == typeof(float))
		{
			float value = GetMinimumValueSafe<float>();
			return Unsafe.As<float, T>(ref value);
		}
		else if (typeof(T) == typeof(double))
		{
			double value = GetMinimumValueSafe<double>();
			return Unsafe.As<double, T>(ref value);
		}
		else if (typeof(T) == typeof(decimal))
		{
			decimal value = GetMinimumValueSafe<decimal>();
			return Unsafe.As<decimal, T>(ref value);
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
			return Unsafe.As<sbyte, T>(ref value);
		}
		else if (typeof(T) == typeof(byte))
		{
			byte value = GetMaximumValueSafe<byte>();
			return Unsafe.As<byte, T>(ref value);
		}
		else if (typeof(T) == typeof(short))
		{
			short value = GetMaximumValueSafe<short>();
			return Unsafe.As<short, T>(ref value);
		}
		else if (typeof(T) == typeof(ushort))
		{
			ushort value = GetMaximumValueSafe<ushort>();
			return Unsafe.As<ushort, T>(ref value);
		}
		else if (typeof(T) == typeof(int))
		{
			int value = GetMaximumValueSafe<int>();
			return Unsafe.As<int, T>(ref value);
		}
		else if (typeof(T) == typeof(uint))
		{
			uint value = GetMaximumValueSafe<uint>();
			return Unsafe.As<uint, T>(ref value);
		}
		else if (typeof(T) == typeof(long))
		{
			long value = GetMaximumValueSafe<long>();
			return Unsafe.As<long, T>(ref value);
		}
		else if (typeof(T) == typeof(ulong))
		{
			ulong value = GetMaximumValueSafe<ulong>();
			return Unsafe.As<ulong, T>(ref value);
		}
		else if (typeof(T) == typeof(Int128))
		{
			Int128 value = GetMaximumValueSafe<Int128>();
			return Unsafe.As<Int128, T>(ref value);
		}
		else if (typeof(T) == typeof(UInt128))
		{
			UInt128 value = GetMaximumValueSafe<UInt128>();
			return Unsafe.As<UInt128, T>(ref value);
		}
		else if (typeof(T) == typeof(Half))
		{
			Half value = GetMaximumValueSafe<Half>();
			return Unsafe.As<Half, T>(ref value);
		}
		else if (typeof(T) == typeof(float))
		{
			float value = GetMaximumValueSafe<float>();
			return Unsafe.As<float, T>(ref value);
		}
		else if (typeof(T) == typeof(double))
		{
			double value = GetMaximumValueSafe<double>();
			return Unsafe.As<double, T>(ref value);
		}
		else if (typeof(T) == typeof(decimal))
		{
			decimal value = GetMaximumValueSafe<decimal>();
			return Unsafe.As<decimal, T>(ref value);
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
