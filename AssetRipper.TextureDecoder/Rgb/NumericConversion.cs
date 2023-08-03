using System.Numerics;

namespace AssetRipper.TextureDecoder.Rgb;
public static partial class NumericConversion
{
	public static T GetMinimumValue<T>() where T : unmanaged, INumberBase<T>, IMinMaxValue<T>
	{
		return typeof(T) == typeof(Half) || typeof(T) == typeof(float) || typeof(T) == typeof(double) || typeof(T) == typeof(decimal)
			? T.Zero
			: T.MinValue;
	}

	public static T GetMaximumValue<T>() where T : unmanaged, INumberBase<T>, IMinMaxValue<T>
	{
		return typeof(T) == typeof(Half) || typeof(T) == typeof(float) || typeof(T) == typeof(double) || typeof(T) == typeof(decimal)
			? T.One
			: T.MaxValue;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	private static T ThrowOrReturnDefault<T>() where T : struct
	{
#if DEBUG
		throw new InvalidCastException();
#else
		return default; //exceptions prevent inlining
#endif
	}

	private static TTo ToSignedNumber<TFrom, TTo>(TFrom value)
		where TFrom : unmanaged, IUnsignedNumber<TFrom>, IShiftOperators<TFrom, int, TFrom>, IBitwiseOperators<TFrom, TFrom, TFrom>
		where TTo : unmanaged, ISignedNumber<TTo>
	{
		if (Unsafe.SizeOf<TFrom>() != Unsafe.SizeOf<TTo>())
		{
			return ThrowOrReturnDefault<TTo>();
		}

		TFrom SignBit = TFrom.One << (Unsafe.SizeOf<TFrom>() * 8 - 1);
		TFrom converted = (SignBit ^ value);
		return Unsafe.As<TFrom, TTo>(ref converted);
	}

	private static TTo ToUnsignedNumber<TFrom, TTo>(TFrom value)
		where TFrom : unmanaged, ISignedNumber<TFrom>
		where TTo : unmanaged, IUnsignedNumber<TTo>, IShiftOperators<TTo, int, TTo>, IBitwiseOperators<TTo, TTo, TTo>
	{
		if (Unsafe.SizeOf<TFrom>() != Unsafe.SizeOf<TTo>())
		{
			return ThrowOrReturnDefault<TTo>();
		}

		TTo SignBit = TTo.One << (Unsafe.SizeOf<TTo>() * 8 - 1);
		return SignBit ^ Unsafe.As<TFrom, TTo>(ref value);
	}

	private static void Test()
	{
		uint unum = default;
		int num = default;

		int converted = ToSignedNumber<uint, int>(unum);
	}
}
