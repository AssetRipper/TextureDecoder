using System.Runtime.InteropServices;

namespace AssetRipper.TextureDecoder.Rgb;
public static partial class NumericConversion
{
	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	internal static T GetMinimumValueSafe<T>() where T : unmanaged, INumberBase<T>, IMinMaxValue<T>
	{
		return typeof(T) == typeof(Half) || typeof(T) == typeof(float) || typeof(T) == typeof(NFloat) || typeof(T) == typeof(double) || typeof(T) == typeof(decimal)
			? T.Zero
			: T.MinValue;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	internal static T GetMaximumValueSafe<T>() where T : unmanaged, INumberBase<T>, IMinMaxValue<T>
	{
		return typeof(T) == typeof(Half) || typeof(T) == typeof(float) || typeof(T) == typeof(NFloat) || typeof(T) == typeof(double) || typeof(T) == typeof(decimal)
			? T.One
			: T.MaxValue;
	}

#if DEBUG
	[DoesNotReturn]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	private static T ThrowOrReturnDefault<T>() where T : struct
	{
#if DEBUG
		throw new InvalidCastException();
#else
		return default; //exceptions prevent inlining
#endif
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
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

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
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
}
