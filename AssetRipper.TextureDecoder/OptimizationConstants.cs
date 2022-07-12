namespace AssetRipper.TextureDecoder
{
	internal static class OptimizationConstants
	{
#if NETCOREAPP
		internal const MethodImplOptions AggressiveInliningAndOptimization = MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization;
#else
		internal const MethodImplOptions AggressiveInliningAndOptimization = MethodImplOptions.AggressiveInlining;
#endif
	}
}
