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
	}
}
