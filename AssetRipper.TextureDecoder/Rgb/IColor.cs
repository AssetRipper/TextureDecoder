namespace AssetRipper.TextureDecoder.Rgb
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
	/// <item><see cref="int"/></item>
	/// <item><see cref="uint"/></item>
	/// <item><see cref="long"/></item>
	/// <item><see cref="ulong"/></item>
	/// <item><see cref="Int128"/></item>
	/// <item><see cref="UInt128"/></item>
	/// <item><see cref="Half"/></item>
	/// <item><see cref="float"/></item>
	/// <item><see cref="double"/></item>
	/// <item><see cref="decimal"/></item>
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

		/// <summary>
		/// Does the format have a red channel?
		/// </summary>
		static abstract bool HasRedChannel { get; }
		/// <summary>
		/// Does the format have a green channel?
		/// </summary>
		static abstract bool HasGreenChannel { get; }
		/// <summary>
		/// Does the format have a blue channel?
		/// </summary>
		static abstract bool HasBlueChannel { get; }
		/// <summary>
		/// Does the format have an alpha channel?
		/// </summary>
		static abstract bool HasAlphaChannel { get; }
	}

	public static class ColorExtensions
	{
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
}
