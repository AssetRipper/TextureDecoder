using AssetRipper.TextureDecoder.Rgb;
using AssetRipper.TextureDecoder.Rgb.Formats;

namespace AssetRipper.TextureDecoder.Yuy2
{
	public static class Yuy2Decoder
	{
		/// <summary>
		/// Decompress a YUY2 image
		/// </summary>
		/// <param name="input">Input buffer containing the compressed image.</param>
		/// <param name="width">Pixel width of the image.</param>
		/// <param name="height">Pixel height of the image.</param>
		/// <param name="output">An output buffer of size 4 * width * height.</param>
		/// <returns>Number of bytes read from <paramref name="input"/></returns>
		public static int DecompressYUY2(ReadOnlySpan<byte> input, int width, int height, out byte[] output)
		{
			return DecompressYUY2<ColorBGRA32, byte>(input, width, height, out output);
		}

		/// <summary>
		/// Decompress a YUY2 image
		/// </summary>
		/// <param name="input">Input buffer containing the compressed image.</param>
		/// <param name="width">Pixel width of the image.</param>
		/// <param name="height">Pixel height of the image.</param>
		/// <param name="output">An output buffer. Must be at least 4 * width * height.</param>
		/// <returns>Number of bytes read from <paramref name="input"/></returns>
		public static int DecompressYUY2(ReadOnlySpan<byte> input, int width, int height, Span<byte> output)
		{
			return DecompressYUY2<ColorBGRA32, byte>(input, width, height, output);
		}

		/// <summary>
		/// Decompress a YUY2 image
		/// </summary>
		/// <typeparam name="TOutputColor">The <see cref="IColor{T}"/> type used for each pixel.</typeparam>
		/// <typeparam name="TOutputChannelValue">The channel type used in <typeparamref name="TOutputColor"/>.</typeparam>
		/// <param name="input">Input buffer containing the compressed image.</param>
		/// <param name="width">Pixel width of the image.</param>
		/// <param name="height">Pixel height of the image.</param>
		/// <param name="output">An output buffer. Must be at least width * height * pixelSize.</param>
		/// <returns>Number of bytes read from <paramref name="input"/></returns>
		public static int DecompressYUY2<TOutputColor, TOutputChannelValue>(ReadOnlySpan<byte> input, int width, int height, out byte[] output)
			where TOutputChannelValue : unmanaged
			where TOutputColor : unmanaged, IColor<TOutputChannelValue>
		{
			output = new byte[width * height * Unsafe.SizeOf<TOutputColor>()];
			return DecompressYUY2<TOutputColor, TOutputChannelValue>(input, width, height, output);
		}

		/// <summary>
		/// Decompress a YUY2 image
		/// </summary>
		/// <typeparam name="TOutputColor">The <see cref="IColor{T}"/> type used for each pixel.</typeparam>
		/// <typeparam name="TOutputChannelValue">The channel type used in <typeparamref name="TOutputColor"/>.</typeparam>
		/// <param name="input">Input buffer containing the compressed image.</param>
		/// <param name="width">Pixel width of the image.</param>
		/// <param name="height">Pixel height of the image.</param>
		/// <param name="output">An output buffer. Must be at least width * height * pixelSize.</param>
		/// <returns>Number of bytes read from <paramref name="input"/></returns>
		public static int DecompressYUY2<TOutputColor, TOutputChannelValue>(ReadOnlySpan<byte> input, int width, int height, Span<byte> output)
			where TOutputChannelValue : unmanaged
			where TOutputColor : unmanaged, IColor<TOutputChannelValue>
		{
			return DecompressYUY2<TOutputColor, TOutputChannelValue>(input, width, height, MemoryMarshal.Cast<byte, TOutputColor>(output));
		}

		/// <summary>
		/// Decompress a YUY2 image
		/// </summary>
		/// <typeparam name="TOutputColor">The <see cref="IColor{T}"/> type used for each pixel.</typeparam>
		/// <typeparam name="TOutputChannelValue">The channel type used in <typeparamref name="TOutputColor"/>.</typeparam>
		/// <param name="input">Input buffer containing the compressed image.</param>
		/// <param name="width">Pixel width of the image.</param>
		/// <param name="height">Pixel height of the image.</param>
		/// <param name="output">An output buffer. Must be at least width * height.</param>
		/// <returns>Number of bytes read from <paramref name="input"/></returns>
		public static int DecompressYUY2<TOutputColor, TOutputChannelValue>(ReadOnlySpan<byte> input, int width, int height, Span<TOutputColor> output)
			where TOutputChannelValue : unmanaged
			where TOutputColor : unmanaged, IColor<TOutputChannelValue>
		{
			ThrowHelper.ThrowIfNotEnoughSpace(output.Length, width * height);

			int p = 0;
			int o = 0;
			int halfWidth = width / 2;
			for (int j = 0; j < height; j++)
			{
				for (int i = 0; i < halfWidth; ++i)
				{
					int y0 = input[p++];
					int u0 = input[p++];
					int y1 = input[p++];
					int v0 = input[p++];
					int c = y0 - 16;
					int d = u0 - 128;
					int e = v0 - 128;
					byte b0 = ClampByte((298 * c + 516 * d + 128) >> 8);            // blue
					byte g0 = ClampByte((298 * c - 100 * d - 208 * e + 128) >> 8);  // green
					byte r0 = ClampByte((298 * c + 409 * e + 128) >> 8);            // red
					output[o++].SetConvertedChannels<TOutputColor, TOutputChannelValue, byte>(r0, g0, b0);
					c = y1 - 16;
					byte b1 = ClampByte((298 * c + 516 * d + 128) >> 8);            // blue
					byte g1 = ClampByte((298 * c - 100 * d - 208 * e + 128) >> 8);  // green
					byte r1 = ClampByte((298 * c + 409 * e + 128) >> 8);            // red
					output[o++].SetConvertedChannels<TOutputColor, TOutputChannelValue, byte>(r1, g1, b1);
				}
			}

			return p;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		private static byte ClampByte(int x)
		{
			return (byte)(byte.MaxValue < x ? byte.MaxValue : (x > byte.MinValue ? x : byte.MinValue));
		}
	}
}
