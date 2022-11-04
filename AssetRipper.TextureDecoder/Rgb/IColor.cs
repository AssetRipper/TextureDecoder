﻿namespace AssetRipper.TextureDecoder.Rgb
{
	/// <summary>
	/// An <see langword="interface"/> for handling color formats with up to 4 channels.
	/// </summary>
	/// <remarks>
	/// When used as a generic type constraint, the methods and properties get devirtualized by the JIT compiler.
	/// This prevents boxing when the implementing type is a <see langword="struct"/>.
	/// </remarks>
	/// <typeparam name="T">
	/// Supported types are:
	/// <list type="bullet">
	/// <item><see cref="sbyte"/></item>
	/// <item><see cref="byte"/></item>
	/// <item><see cref="short"/></item>
	/// <item><see cref="ushort"/></item>
	/// <item><see cref="Half"/></item>
	/// <item><see cref="float"/></item>
	/// <item><see cref="double"/></item>
	/// </list>
	/// </typeparam>
	public interface IColor<T> where T : unmanaged
	{
		/// <summary>
		/// The red channel
		/// </summary>
		T R { get; set; }
		/// <summary>
		/// The green channel
		/// </summary>
		T G { get; set; }
		/// <summary>
		/// The blue channel
		/// </summary>
		T B { get; set; }
		/// <summary>
		/// The alpha channel
		/// </summary>
		T A { get; set; }

		void GetChannels(out T r, out T g, out T b, out T a);
		void SetChannels(T r, T g, T b, T a);
	}

	internal static class ColorExtensions
	{
		[MethodImpl(OptimizationConstants.AggressiveInliningAndOptimization)]
		internal static void SetConvertedChannels<TThis, TThisArg, TSourceArg>(this ref TThis color, TSourceArg r, TSourceArg g, TSourceArg b, TSourceArg a)
			where TThisArg : unmanaged
			where TSourceArg : unmanaged
			where TThis : unmanaged, IColor<TThisArg>
		{
			color.SetChannels(
					ConversionUtilities.ConvertValue<TSourceArg, TThisArg>(r),
					ConversionUtilities.ConvertValue<TSourceArg, TThisArg>(g),
					ConversionUtilities.ConvertValue<TSourceArg, TThisArg>(b),
					ConversionUtilities.ConvertValue<TSourceArg, TThisArg>(a));
		}
	}
}
