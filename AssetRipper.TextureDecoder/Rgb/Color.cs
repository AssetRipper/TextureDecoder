using AssetRipper.TextureDecoder.Rgb.Formats;

namespace AssetRipper.TextureDecoder.Rgb;

public static class Color
{
	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	internal static void SetBlack<TThis, TThisChannel>(this ref TThis color)
		where TThisChannel : unmanaged
		where TThis : unmanaged, IColor<TThisChannel>
	{
		color.SetChannels(
			NumericConversion.GetMinimumValue<TThisChannel>(),
			NumericConversion.GetMinimumValue<TThisChannel>(),
			NumericConversion.GetMinimumValue<TThisChannel>(),
			NumericConversion.GetMaximumValue<TThisChannel>());
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	internal static void SetWhite<TThis, TThisChannel>(this ref TThis color)
		where TThisChannel : unmanaged
		where TThis : unmanaged, IColor<TThisChannel>
	{
		color.SetChannels(
			NumericConversion.GetMaximumValue<TThisChannel>(),
			NumericConversion.GetMaximumValue<TThisChannel>(),
			NumericConversion.GetMaximumValue<TThisChannel>(),
			NumericConversion.GetMaximumValue<TThisChannel>());
	}

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
	internal static void SetConvertedChannels<TThis, TThisChannel, TSourceChannel>(this ref TThis color, ColorRGB<TSourceChannel> rgb, TSourceChannel a)
		where TThisChannel : unmanaged
		where TSourceChannel : unmanaged, INumberBase<TSourceChannel>, IMinMaxValue<TSourceChannel>
		where TThis : unmanaged, IColor<TThisChannel>
	{
		color.SetChannels(
			NumericConversion.Convert<TSourceChannel, TThisChannel>(rgb.R),
			NumericConversion.Convert<TSourceChannel, TThisChannel>(rgb.G),
			NumericConversion.Convert<TSourceChannel, TThisChannel>(rgb.B),
			NumericConversion.Convert<TSourceChannel, TThisChannel>(a));
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	public static int GetChannelCount<T>() where T : IColor
	{
		return (T.HasRedChannel ? 1 : 0) + (T.HasGreenChannel ? 1 : 0) + (T.HasBlueChannel ? 1 : 0) + (T.HasAlphaChannel ? 1 : 0);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	public static TTarget Convert<TThis, TThisChannelValue, TTarget, TTargetChannelValue>(this TThis color)
		where TThisChannelValue : unmanaged
		where TTargetChannelValue : unmanaged
		where TThis : unmanaged, IColor<TThisChannelValue>
		where TTarget : unmanaged, IColor<TTargetChannelValue>
	{
		if (typeof(TThis) == typeof(TTarget))
		{
			return Unsafe.BitCast<TThis, TTarget>(color);
		}
		else
		{
			TTarget destination = default;
			color.GetChannels(out TThisChannelValue r, out TThisChannelValue g, out TThisChannelValue b, out TThisChannelValue a);
			destination.SetConvertedChannels<TTarget, TTargetChannelValue, TThisChannelValue>(r, g, b, a);
			return destination;
		}
	}

	internal static TColor SRgbToLinear<TColor, TChannel>(this TColor color)
		where TChannel : unmanaged
		where TColor : unmanaged, IColor<TChannel>
	{
		color.GetChannels(out TChannel r, out TChannel g, out TChannel b, out TChannel a);
		color.SetChannels(
			SRgb.ToLinear(r),
			SRgb.ToLinear(g),
			SRgb.ToLinear(b),
			a);
		return color;
	}

	internal static TColor LinearToSRgb<TColor, TChannel>(this TColor color)
		where TChannel : unmanaged
		where TColor : unmanaged, IColor<TChannel>
	{
		color.GetChannels(out TChannel r, out TChannel g, out TChannel b, out TChannel a);
		color.SetChannels(
			SRgb.FromLinear(r),
			SRgb.FromLinear(g),
			SRgb.FromLinear(b),
			a);
		return color;
	}
}
