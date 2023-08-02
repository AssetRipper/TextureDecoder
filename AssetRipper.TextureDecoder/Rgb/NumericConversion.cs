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
}
