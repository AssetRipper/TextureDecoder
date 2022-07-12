namespace AssetRipper.TextureDecoder
{
	internal static class ThrowHelper
	{
		private const int BytesPerPixel = 4;
		
		internal static void ThrowIfNotEnoughSpace(Span<byte> output, int width, int height)
		{
			if (output.Length < width * height * BytesPerPixel)
			{
				throw new InvalidOperationException("Not enough space");
			}
		}

		internal static void ThrowIfNotEnoughSpace(int spaceAvailable, int spaceRequired)
		{
			if (spaceAvailable < spaceRequired)
			{
				throw new InvalidOperationException($"Not enough space. {spaceRequired} is required, but only {spaceAvailable} is available");
			}
		}
	}
}
