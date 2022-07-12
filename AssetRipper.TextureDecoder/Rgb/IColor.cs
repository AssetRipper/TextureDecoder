namespace AssetRipper.TextureDecoder.Rgb
{
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
