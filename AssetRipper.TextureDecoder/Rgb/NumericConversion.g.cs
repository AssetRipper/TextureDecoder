//This code is source generated. Do not edit manually.

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
		else if (typeof(TTo) == typeof(Half))
		{
			// There isn't enough precision to convert from anything bigger than byte to Half, so we convert to float first.
			float x = ConvertByte<float>(value);
			Half converted = (Half)x;
			return Unsafe.As<Half, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(float))
		{
			float converted = value / (float)byte.MaxValue;
			return Unsafe.As<float, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(double))
		{
			double converted = value / (double)byte.MaxValue;
			return Unsafe.As<double, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(decimal))
		{
			decimal converted = value / (decimal)byte.MaxValue;
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
		else if (typeof(TTo) == typeof(Half))
		{
			// There isn't enough precision to convert from anything bigger than byte to Half, so we convert to float first.
			float x = ConvertUInt16<float>(value);
			Half converted = (Half)x;
			return Unsafe.As<Half, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(float))
		{
			float converted = value / (float)ushort.MaxValue;
			return Unsafe.As<float, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(double))
		{
			double converted = value / (double)ushort.MaxValue;
			return Unsafe.As<double, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(decimal))
		{
			decimal converted = value / (decimal)ushort.MaxValue;
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
			// There are more accurate ways to map uint onto byte, but this is the simplest.
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
			// There are more accurate ways to map uint onto ushort, but this is the simplest.
			unchecked
			{
				ushort converted = (ushort)((uint)value >> 8);
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
		else if (typeof(TTo) == typeof(Half))
		{
			// There isn't enough precision to convert from anything bigger than byte to Half, so we convert to float first.
			float x = ConvertUInt32<float>(value);
			Half converted = (Half)x;
			return Unsafe.As<Half, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(float))
		{
			float converted = value / (float)uint.MaxValue;
			return Unsafe.As<float, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(double))
		{
			double converted = value / (double)uint.MaxValue;
			return Unsafe.As<double, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(decimal))
		{
			decimal converted = value / (decimal)uint.MaxValue;
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
			// There are more accurate ways to map ulong onto byte, but this is the simplest.
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
			// There are more accurate ways to map ulong onto ushort, but this is the simplest.
			unchecked
			{
				ushort converted = (ushort)((ulong)value >> 24);
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
			// There are more accurate ways to map ulong onto uint, but this is the simplest.
			unchecked
			{
				uint converted = (uint)((ulong)value >> 8);
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
		else if (typeof(TTo) == typeof(Half))
		{
			// There isn't enough precision to convert from anything bigger than byte to Half, so we convert to float first.
			float x = ConvertUInt64<float>(value);
			Half converted = (Half)x;
			return Unsafe.As<Half, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(float))
		{
			float converted = value / (float)ulong.MaxValue;
			return Unsafe.As<float, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(double))
		{
			double converted = value / (double)ulong.MaxValue;
			return Unsafe.As<double, TTo>(ref converted);
		}
		else if (typeof(TTo) == typeof(decimal))
		{
			decimal converted = value / (decimal)ulong.MaxValue;
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
			float x = value * byte.MaxValue;
			byte converted = byte.MaxValue < x ? byte.MaxValue : (x > byte.MinValue ? (byte)x : byte.MinValue);
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
			float x = value * ushort.MaxValue;
			ushort converted = ushort.MaxValue < x ? ushort.MaxValue : (x > ushort.MinValue ? (ushort)x : ushort.MinValue);
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
			float x = value * uint.MaxValue;
			uint converted = uint.MaxValue < x ? uint.MaxValue : (x > uint.MinValue ? (uint)x : uint.MinValue);
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
			float x = value * ulong.MaxValue;
			ulong converted = ulong.MaxValue < x ? ulong.MaxValue : (x > ulong.MinValue ? (ulong)x : ulong.MinValue);
			return Unsafe.As<ulong, TTo>(ref converted);
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
			double x = value * byte.MaxValue;
			byte converted = byte.MaxValue < x ? byte.MaxValue : (x > byte.MinValue ? (byte)x : byte.MinValue);
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
			double x = value * ushort.MaxValue;
			ushort converted = ushort.MaxValue < x ? ushort.MaxValue : (x > ushort.MinValue ? (ushort)x : ushort.MinValue);
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
			double x = value * uint.MaxValue;
			uint converted = uint.MaxValue < x ? uint.MaxValue : (x > uint.MinValue ? (uint)x : uint.MinValue);
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
			double x = value * ulong.MaxValue;
			ulong converted = ulong.MaxValue < x ? ulong.MaxValue : (x > ulong.MinValue ? (ulong)x : ulong.MinValue);
			return Unsafe.As<ulong, TTo>(ref converted);
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
			decimal x = value * byte.MaxValue;
			byte converted = byte.MaxValue < x ? byte.MaxValue : (x > byte.MinValue ? (byte)x : byte.MinValue);
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
			decimal x = value * ushort.MaxValue;
			ushort converted = ushort.MaxValue < x ? ushort.MaxValue : (x > ushort.MinValue ? (ushort)x : ushort.MinValue);
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
			decimal x = value * uint.MaxValue;
			uint converted = uint.MaxValue < x ? uint.MaxValue : (x > uint.MinValue ? (uint)x : uint.MinValue);
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
			decimal x = value * ulong.MaxValue;
			ulong converted = ulong.MaxValue < x ? ulong.MaxValue : (x > ulong.MinValue ? (ulong)x : ulong.MinValue);
			return Unsafe.As<ulong, TTo>(ref converted);
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
	private static sbyte ChangeSign(byte value)
	{
		unchecked
		{
			const uint SignBit = 0x80;
			return (sbyte)(value ^ SignBit);
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	private static byte ChangeSign(sbyte value)
	{
		unchecked
		{
			const uint SignBit = 0x80;
			return (byte)((uint)value ^ SignBit);
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	private static short ChangeSign(ushort value)
	{
		unchecked
		{
			const uint SignBit = 0x8000;
			return (short)(value ^ SignBit);
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	private static ushort ChangeSign(short value)
	{
		unchecked
		{
			const uint SignBit = 0x8000;
			return (ushort)((uint)value ^ SignBit);
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	private static int ChangeSign(uint value)
	{
		unchecked
		{
			const uint SignBit = 0x80000000;
			return (int)(value ^ SignBit);
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	private static uint ChangeSign(int value)
	{
		unchecked
		{
			const uint SignBit = 0x80000000;
			return (uint)((uint)value ^ SignBit);
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	private static long ChangeSign(ulong value)
	{
		unchecked
		{
			const ulong SignBit = 0x8000000000000000;
			return (long)(value ^ SignBit);
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	private static ulong ChangeSign(long value)
	{
		unchecked
		{
			const ulong SignBit = 0x8000000000000000;
			return (ulong)((ulong)value ^ SignBit);
		}
	}

}
