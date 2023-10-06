using AssetRipper.TextureDecoder.Rgb;
using AssetRipper.TextureDecoder.Rgb.Formats;
using System.Buffers.Binary;
using System.Runtime.InteropServices;

namespace AssetRipper.TextureDecoder.Dxt
{
	public static class DxtDecoder
	{
		/// <summary>
		/// Decompress a DXT1 image
		/// </summary>
		/// <param name="input">Input buffer containing the compressed image.</param>
		/// <param name="width">Pixel width of the image.</param>
		/// <param name="height">Pixel height of the image.</param>
		/// <param name="output">An output buffer of size 4 * width * height.</param>
		/// <returns>Number of bytes read from <paramref name="input"/></returns>
		public static int DecompressDXT1(ReadOnlySpan<byte> input, int width, int height, out byte[] output)
		{
			return DecompressDXT1<ColorBGRA32, byte>(input, width, height, out output);
		}

		/// <summary>
		/// Decompress a DXT1 image
		/// </summary>
		/// <param name="input">Input buffer containing the compressed image.</param>
		/// <param name="width">Pixel width of the image.</param>
		/// <param name="height">Pixel height of the image.</param>
		/// <param name="output">An output buffer. Must be at least 4 * width * height.</param>
		/// <returns>Number of bytes read from <paramref name="input"/></returns>
		public static int DecompressDXT1(ReadOnlySpan<byte> input, int width, int height, Span<byte> output)
		{
			return DecompressDXT1<ColorBGRA32, byte>(input, width, height, output);
		}

		/// <summary>
		/// Decompress a DXT1 image
		/// </summary>
		/// <typeparam name="TOutputColor">The <see cref="IColor{T}"/> type used for each pixel.</typeparam>
		/// <typeparam name="TOutputChannel">The channel type used in <typeparamref name="TOutputColor"/>.</typeparam>
		/// <param name="input">Input buffer containing the compressed image.</param>
		/// <param name="width">Pixel width of the image.</param>
		/// <param name="height">Pixel height of the image.</param>
		/// <param name="output">An output buffer. Must be at least width * height * pixelSize.</param>
		/// <returns>Number of bytes read from <paramref name="input"/></returns>
		public static int DecompressDXT1<TOutputColor, TOutputChannel>(ReadOnlySpan<byte> input, int width, int height, out byte[] output)
			where TOutputChannel : unmanaged
			where TOutputColor : unmanaged, IColor<TOutputChannel>
		{
			output = new byte[width * height * Unsafe.SizeOf<TOutputColor>()];
			return DecompressDXT1<TOutputColor, TOutputChannel>(input, width, height, output);
		}

		/// <summary>
		/// Decompress a DXT1 image
		/// </summary>
		/// <typeparam name="TOutputColor">The <see cref="IColor{T}"/> type used for each pixel.</typeparam>
		/// <typeparam name="TOutputChannel">The channel type used in <typeparamref name="TOutputColor"/>.</typeparam>
		/// <param name="input">Input buffer containing the compressed image.</param>
		/// <param name="width">Pixel width of the image.</param>
		/// <param name="height">Pixel height of the image.</param>
		/// <param name="output">An output buffer. Must be at least width * height * pixelSize.</param>
		/// <returns>Number of bytes read from <paramref name="input"/></returns>
		public static int DecompressDXT1<TOutputColor, TOutputChannel>(ReadOnlySpan<byte> input, int width, int height, Span<byte> output)
			where TOutputChannel : unmanaged
			where TOutputColor : unmanaged, IColor<TOutputChannel>
		{
			return DecompressDXT1<TOutputColor, TOutputChannel>(input, width, height, MemoryMarshal.Cast<byte, TOutputColor>(output));
		}

		/// <summary>
		/// Decompress a DXT1 image
		/// </summary>
		/// <typeparam name="TOutputColor">The <see cref="IColor{T}"/> type used for each pixel.</typeparam>
		/// <typeparam name="TOutputChannel">The channel type used in <typeparamref name="TOutputColor"/>.</typeparam>
		/// <param name="input">Input buffer containing the compressed image.</param>
		/// <param name="width">Pixel width of the image.</param>
		/// <param name="height">Pixel height of the image.</param>
		/// <param name="output">An output buffer. Must be at least width * height * pixelSize.</param>
		/// <returns>Number of bytes read from <paramref name="input"/></returns>
		public static int DecompressDXT1<TOutputColor, TOutputChannel>(ReadOnlySpan<byte> input, int width, int height, Span<TOutputColor> output)
			where TOutputChannel : unmanaged
			where TOutputColor : unmanaged, IColor<TOutputChannel>
		{
			ThrowHelper.ThrowIfNotEnoughSpace(output.Length, width * height);

			int offset = 0;
			int bcw = (width + 3) / 4;
			int bch = (height + 3) / 4;
			int clen_last = (width + 3) % 4 + 1;
			Span<TOutputColor> buffer = stackalloc TOutputColor[16];
			Span<TOutputColor> colors = stackalloc TOutputColor[4];
			for (int t = 0; t < bch; t++)
			{
				for (int s = 0; s < bcw; s++, offset += 8)
				{
					int q0 = input[offset + 0] | input[offset + 1] << 8;
					int q1 = input[offset + 2] | input[offset + 3] << 8;
					Rgb565(q0, out byte r0, out byte g0, out byte b0);
					Rgb565(q1, out byte r1, out byte g1, out byte b1);
					colors[0].SetConvertedChannels<TOutputColor, TOutputChannel, byte>(r0, g0, b0);
					colors[1].SetConvertedChannels<TOutputColor, TOutputChannel, byte>(r1, g1, b1);
					if (q0 > q1)
					{
						colors[2].SetConvertedChannels<TOutputColor, TOutputChannel, byte>((byte)((r0 * 2 + r1) / 3), (byte)((g0 * 2 + g1) / 3), (byte)((b0 * 2 + b1) / 3));
						colors[3].SetConvertedChannels<TOutputColor, TOutputChannel, byte>((byte)((r0 + r1 * 2) / 3), (byte)((g0 + g1 * 2) / 3), (byte)((b0 + b1 * 2) / 3));
					}
					else
					{
						colors[2].SetConvertedChannels<TOutputColor, TOutputChannel, byte>((byte)((r0 + r1) / 2), (byte)((g0 + g1) / 2), (byte)((b0 + b1) / 2));
						colors[3].SetBlack<TOutputColor, TOutputChannel>();
					}

					uint d = ToUInt32(input, offset + 4);
					for (int i = 0; i < 16; i++, d >>= 2)
					{
						buffer[i] = colors[unchecked((int)(d & 3))];
					}

					int clen = s < bcw - 1 ? 4 : clen_last;
					for (int i = 0, y = t * 4; i < 4 && y < height; i++, y++)
					{
						BlockCopy(buffer, i * 4, output, y * width + s * 4, clen);
					}
				}
			}

			return offset;
		}

		/// <summary>
		/// Decompress a DXT3 image
		/// </summary>
		/// <param name="input">Input buffer containing the compressed image.</param>
		/// <param name="width">Pixel width of the image.</param>
		/// <param name="height">Pixel height of the image.</param>
		/// <param name="output">An output buffer of size 4 * width * height.</param>
		/// <returns>Number of bytes read from <paramref name="input"/></returns>
		public static int DecompressDXT3(ReadOnlySpan<byte> input, int width, int height, out byte[] output)
		{
			output = new byte[width * height * sizeof(uint)];
			return DecompressDXT3(input, width, height, output);
		}

		/// <summary>
		/// Decompress a DXT3 image
		/// </summary>
		/// <param name="input">Input buffer containing the compressed image.</param>
		/// <param name="width">Pixel width of the image.</param>
		/// <param name="height">Pixel height of the image.</param>
		/// <param name="output">An output buffer. Must be at least 4 * width * height.</param>
		/// <returns>Number of bytes read from <paramref name="input"/></returns>
		public static int DecompressDXT3(ReadOnlySpan<byte> input, int width, int height, Span<byte> output)
		{
			ThrowHelper.ThrowIfNotEnoughSpace(output, width, height);

			int offset = 0;
			int bcw = (width + 3) / 4;
			int bch = (height + 3) / 4;
			int clen_last = (width + 3) % 4 + 1;
			uint[] buffer = new uint[16];
			int[] colors = new int[4];
			int[] alphas = new int[16];
			for (int t = 0; t < bch; t++)
			{
				for (int s = 0; s < bcw; s++, offset += 16)
				{
					for (int i = 0; i < 4; i++)
					{
						int alpha = input[offset + i * 2] | input[offset + i * 2 + 1] << 8;
						alphas[i * 4 + 0] = (((alpha >> 0) & 0xF) * 0x11) << 24;
						alphas[i * 4 + 1] = (((alpha >> 4) & 0xF) * 0x11) << 24;
						alphas[i * 4 + 2] = (((alpha >> 8) & 0xF) * 0x11) << 24;
						alphas[i * 4 + 3] = (((alpha >> 12) & 0xF) * 0x11) << 24;
					}

					int q0 = input[offset + 8] | input[offset + 9] << 8;
					int q1 = input[offset + 10] | input[offset + 11] << 8;
					Rgb565(q0, out byte r0, out byte g0, out byte b0);
					Rgb565(q1, out byte r1, out byte g1, out byte b1);
					colors[0] = Color(r0, g0, b0, 0);
					colors[1] = Color(r1, g1, b1, 0);
					if (q0 > q1)
					{
						colors[2] = Color((r0 * 2 + r1) / 3, (g0 * 2 + g1) / 3, (b0 * 2 + b1) / 3, 0);
						colors[3] = Color((r0 + r1 * 2) / 3, (g0 + g1 * 2) / 3, (b0 + b1 * 2) / 3, 0);
					}
					else
					{
						colors[2] = Color((r0 + r1) / 2, (g0 + g1) / 2, (b0 + b1) / 2, 0);
					}

					uint d = ToUInt32(input, offset + 12);
					for (int i = 0; i < 16; i++, d >>= 2)
					{
						buffer[i] = unchecked((uint)(colors[d & 3] | alphas[i]));
					}

					int clen = (s < bcw - 1 ? 4 : clen_last) * 4;
					for (int i = 0, y = t * 4; i < 4 && y < height; i++, y++)
					{
						ReadOnlySpan<byte> bufferSpan = MemoryMarshal.Cast<uint, byte>(new ReadOnlySpan<uint>(buffer));
						BlockCopy(bufferSpan, i * 4 * 4, output, (y * width + s * 4) * 4, clen);
					}
				}
			}

			return offset;
		}

		/// <summary>
		/// Decompress a DXT5 image
		/// </summary>
		/// <param name="input">Input buffer containing the compressed image.</param>
		/// <param name="width">Pixel width of the image.</param>
		/// <param name="height">Pixel height of the image.</param>
		/// <param name="output">An output buffer of size 4 * width * height.</param>
		/// <returns>Number of bytes read from <paramref name="input"/></returns>
		public static int DecompressDXT5(ReadOnlySpan<byte> input, int width, int height, out byte[] output)
		{
			output = new byte[width * height * sizeof(uint)];
			return DecompressDXT5(input, width, height, output);
		}

		/// <summary>
		/// Decompress a DXT5 image
		/// </summary>
		/// <param name="input">Input buffer containing the compressed image.</param>
		/// <param name="width">Pixel width of the image.</param>
		/// <param name="height">Pixel height of the image.</param>
		/// <param name="output">An output buffer. Must be at least 4 * width * height.</param>
		/// <returns>Number of bytes read from <paramref name="input"/></returns>
		public static int DecompressDXT5(ReadOnlySpan<byte> input, int width, int height, Span<byte> output)
		{
			ThrowHelper.ThrowIfNotEnoughSpace(output, width, height);

			int offset = 0;
			int bcw = (width + 3) / 4;
			int bch = (height + 3) / 4;
			int clen_last = (width + 3) % 4 + 1;
			uint[] buffer = new uint[16];
			int[] colors = new int[4];
			int[] alphas = new int[8];
			for (int t = 0; t < bch; t++)
			{
				for (int s = 0; s < bcw; s++, offset += 16)
				{
					alphas[0] = input[offset + 0];
					alphas[1] = input[offset + 1];
					if (alphas[0] > alphas[1])
					{
						alphas[2] = (alphas[0] * 6 + alphas[1]) / 7;
						alphas[3] = (alphas[0] * 5 + alphas[1] * 2) / 7;
						alphas[4] = (alphas[0] * 4 + alphas[1] * 3) / 7;
						alphas[5] = (alphas[0] * 3 + alphas[1] * 4) / 7;
						alphas[6] = (alphas[0] * 2 + alphas[1] * 5) / 7;
						alphas[7] = (alphas[0] + alphas[1] * 6) / 7;
					}
					else
					{
						alphas[2] = (alphas[0] * 4 + alphas[1]) / 5;
						alphas[3] = (alphas[0] * 3 + alphas[1] * 2) / 5;
						alphas[4] = (alphas[0] * 2 + alphas[1] * 3) / 5;
						alphas[5] = (alphas[0] + alphas[1] * 4) / 5;
						alphas[7] = 255;
					}
					for (int i = 0; i < 8; i++)
					{
						alphas[i] <<= 24;
					}

					int q0 = input[offset + 8] | input[offset + 9] << 8;
					int q1 = input[offset + 10] | input[offset + 11] << 8;
					Rgb565(q0, out byte r0, out byte g0, out byte b0);
					Rgb565(q1, out byte r1, out byte g1, out byte b1);
					colors[0] = Color(r0, g0, b0, 0);
					colors[1] = Color(r1, g1, b1, 0);
					if (q0 > q1)
					{
						colors[2] = Color((r0 * 2 + r1) / 3, (g0 * 2 + g1) / 3, (b0 * 2 + b1) / 3, 0);
						colors[3] = Color((r0 + r1 * 2) / 3, (g0 + g1 * 2) / 3, (b0 + b1 * 2) / 3, 0);
					}
					else
					{
						colors[2] = Color((r0 + r1) / 2, (g0 + g1) / 2, (b0 + b1) / 2, 0);
					}

					ulong da = ToUInt64(input, offset) >> 16;
					uint dc = ToUInt32(input, offset + 12);
					for (int i = 0; i < 16; i++, da >>= 3, dc >>= 2)
					{
						buffer[i] = unchecked((uint)(alphas[da & 7] | colors[dc & 3]));
					}

					int clen = (s < bcw - 1 ? 4 : clen_last) * 4;
					for (int i = 0, y = t * 4; i < 4 && y < height; i++, y++)
					{
						ReadOnlySpan<byte> bufferSpan = MemoryMarshal.Cast<uint, byte>(new ReadOnlySpan<uint>(buffer));
						BlockCopy(bufferSpan, i * 4 * 4, output, (y * width + s * 4) * 4, clen);
					}
				}
			}
			
			return offset;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		private static void Rgb565(int c, out byte r, out byte g, out byte b)
		{
			r = unchecked((byte)((c & 0xf800) >> 8));
			g = unchecked((byte)((c & 0x07e0) >> 3));
			b = unchecked((byte)((c & 0x001f) << 3));
			r |= (byte)(r >> 5);
			g |= (byte)(g >> 6);
			b |= (byte)(b >> 5);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		private static int Color(int r, int g, int b, int a)
		{
			return (byte)r << 16 | (byte)g << 8 | (byte)b | (byte)a << 24;
		}

		/// <summary>
		/// Based on <see cref="Buffer.BlockCopy(Array, int, Array, int, int)"/>
		/// </summary>
		/// <param name="source">The source buffer.</param>
		/// <param name="sourceOffset">The zero-based offset into source.</param>
		/// <param name="destination">The destination buffer.</param>
		/// <param name="destinationOffset">The zero-based offset into destination.</param>
		/// <param name="count">The number of items to copy.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		private static void BlockCopy<T>(ReadOnlySpan<T> source, int sourceOffset, Span<T> destination, int destinationOffset, int count)
		{
			source.Slice(sourceOffset, count).CopyTo(destination.Slice(destinationOffset));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		private static uint ToUInt32(ReadOnlySpan<byte> input, int offset)
		{
			return BinaryPrimitives.ReadUInt32LittleEndian(input.Slice(offset));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		private static ulong ToUInt64(ReadOnlySpan<byte> input, int offset)
		{
			return BinaryPrimitives.ReadUInt64LittleEndian(input.Slice(offset));
		}
	}
}
