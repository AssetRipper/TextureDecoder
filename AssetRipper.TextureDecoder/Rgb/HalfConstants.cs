namespace AssetRipper.TextureDecoder.Rgb;
internal static class HalfConstants
{
#if !NET7_0_OR_GREATER
	private static readonly Half one = (Half)1;
#endif

	public static Half One
	{
		[MethodImpl(OptimizationConstants.AggressiveInliningAndOptimization)]
		get
		{
#if !NET7_0_OR_GREATER
			return one;
#else
			return Half.One;
#endif
		}
	}
}
