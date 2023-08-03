namespace AssetRipper.TextureDecoder.Rgb
{
	internal static class DefaultColorMethods
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		internal static void GetChannels<T, TArg>(T color, out TArg r, out TArg g, out TArg b, out TArg a)
			where TArg : unmanaged
			where T : unmanaged, IColor<TArg>
		{
			r = color.R;
			g = color.G;
			b = color.B;
			a = color.A;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		internal static void SetChannels<T, TArg>(ref T color, TArg r, TArg g, TArg b, TArg a)
			where TArg : unmanaged
			where T : unmanaged, IColor<TArg>
		{
			color.R = r;
			color.G = g;
			color.B = b;
			color.A = a;
		}
	}
}
