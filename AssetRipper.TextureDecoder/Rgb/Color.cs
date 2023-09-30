namespace AssetRipper.TextureDecoder.Rgb;

public static class Color
{
	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	internal static void SetConvertedChannels<TThis, TThisChannel, TSourceChannel>(this ref TThis color, TSourceChannel r, TSourceChannel g, TSourceChannel b)
		where TThisChannel : unmanaged
		where TSourceChannel : unmanaged
		where TThis : unmanaged, IColor<TThisChannel>
	{
		color.SetChannels(
			NumericConversion.Convert<TSourceChannel, TThisChannel>(r),
			NumericConversion.Convert<TSourceChannel, TThisChannel>(g),
			NumericConversion.Convert<TSourceChannel, TThisChannel>(b),
			NumericConversion.GetMaximumValue<TThisChannel>());
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	internal static void SetConvertedChannels<TThis, TThisChannel, TSourceChannel>(this ref TThis color, TSourceChannel r, TSourceChannel g, TSourceChannel b, TSourceChannel a)
		where TThisChannel : unmanaged
		where TSourceChannel : unmanaged
		where TThis : unmanaged, IColor<TThisChannel>
	{
		color.SetChannels(
			NumericConversion.Convert<TSourceChannel, TThisChannel>(r),
			NumericConversion.Convert<TSourceChannel, TThisChannel>(g),
			NumericConversion.Convert<TSourceChannel, TThisChannel>(b),
			NumericConversion.Convert<TSourceChannel, TThisChannel>(a));
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	public static int GetChannelCount<T>() where T : IColorBase
	{
		return (T.HasRedChannel ? 1 : 0) + (T.HasGreenChannel ? 1 : 0) + (T.HasBlueChannel ? 1 : 0) + (T.HasAlphaChannel ? 1 : 0);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	public static TTarget Convert<TThis, TThisChannel, TTarget, TTargetChannel>(this TThis color)
		where TThisChannel : unmanaged
		where TTargetChannel : unmanaged
		where TThis : unmanaged, IColor<TThisChannel>
		where TTarget : unmanaged, IColor<TTargetChannel>
	{
		if (typeof(TThis) == typeof(TTarget))
		{
			return Unsafe.As<TThis, TTarget>(ref color);
		}
		else
		{
			TTarget destination = default;
			color.GetChannels(out TThisChannel r, out TThisChannel g, out TThisChannel b, out TThisChannel a);
			destination.SetConvertedChannels<TTarget, TTargetChannel, TThisChannel>(r, g, b, a);
			return destination;
		}
	}
}
