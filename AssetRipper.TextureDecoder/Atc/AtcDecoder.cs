using AssetRipper.TextureDecoder.Rgb;
using System.Buffers.Binary;

namespace AssetRipper.TextureDecoder.Atc
{
	public static class AtcDecoder
	{
		public static int DecompressAtcRgb4<TOutputColor, TOutputChannelValue>(ReadOnlySpan<byte> input, int width, int height, out byte[] output)
			where TOutputChannelValue : unmanaged
			where TOutputColor : unmanaged, IColor<TOutputChannelValue>
		{
			output = new byte[width * height * Unsafe.SizeOf<TOutputColor>()];
			return DecompressAtcRgb4<TOutputColor, TOutputChannelValue>(input, width, height, output);
		}

		public static int DecompressAtcRgb4<TOutputColor, TOutputChannelValue>(ReadOnlySpan<byte> input, int width, int height, Span<byte> output)
			where TOutputChannelValue : unmanaged
			where TOutputColor : unmanaged, IColor<TOutputChannelValue>
		{
			ThrowHelper.ThrowIfNotLittleEndian();
			return DecompressAtcRgb4<TOutputColor, TOutputChannelValue>(input, width, height, MemoryMarshal.Cast<byte, TOutputColor>(output));
		}

		public static int DecompressAtcRgb4<TOutputColor, TOutputChannelValue>(ReadOnlySpan<byte> input, int width, int height, Span<TOutputColor> output)
			where TOutputChannelValue : unmanaged
			where TOutputColor : unmanaged, IColor<TOutputChannelValue>
		{
			ThrowHelper.ThrowIfNotEnoughSpace(output.Length, width * height);
			int bcw = (width + 3) / 4;
			int bch = (height + 3) / 4;
			int clen_last = (width + 3) % 4 + 1;
			Span<TOutputColor> buf = stackalloc TOutputColor[16];
			int inputOffset = 0;
			for (int t = 0; t < bch; t++)
			{
				for (int s = 0; s < bcw; s++, inputOffset += 8)
				{
					DecodeAtcRgb4Block<TOutputColor, TOutputChannelValue>(input.Slice(inputOffset, 8), buf);
					int clen = s < bcw - 1 ? 4 : clen_last;
					for (int i = 0, y = t * 4; i < 4 && y < height; i++, y++)
					{
						int outputOffset = t * 4 * width + s * 4 + i * width;
						for (int j = 0; j < clen; j++)
						{
							output[outputOffset + j] = buf[j + 4 * i];
						}
					}
				}
			}
			return inputOffset;
		}

		public static int DecompressAtcRgba8<TOutputColor, TOutputChannelValue>(ReadOnlySpan<byte> input, int width, int height, out byte[] output)
			where TOutputChannelValue : unmanaged
			where TOutputColor : unmanaged, IColor<TOutputChannelValue>
		{
			output = new byte[width * height * Unsafe.SizeOf<TOutputColor>()];
			return DecompressAtcRgba8<TOutputColor, TOutputChannelValue>(input, width, height, output);
		}

		public static int DecompressAtcRgba8<TOutputColor, TOutputChannelValue>(ReadOnlySpan<byte> input, int width, int height, Span<byte> output)
			where TOutputChannelValue : unmanaged
			where TOutputColor : unmanaged, IColor<TOutputChannelValue>
		{
			ThrowHelper.ThrowIfNotLittleEndian();
			return DecompressAtcRgba8<TOutputColor, TOutputChannelValue>(input, width, height, MemoryMarshal.Cast<byte, TOutputColor>(output));
		}

		public static int DecompressAtcRgba8<TOutputColor, TOutputChannelValue>(ReadOnlySpan<byte> input, int width, int height, Span<TOutputColor> output)
			where TOutputChannelValue : unmanaged
			where TOutputColor : unmanaged, IColor<TOutputChannelValue>
		{
			ThrowHelper.ThrowIfNotEnoughSpace(output.Length, width * height);
			int bcw = (width + 3) / 4;
			int bch = (height + 3) / 4;
			int clen_last = (width + 3) % 4 + 1;
			Span<TOutputColor> buf = stackalloc TOutputColor[16];
			int inputOffset = 0;
			for (int t = 0; t < bch; t++)
			{
				for (int s = 0; s < bcw; s++, inputOffset += 16)
				{
					DecodeAtcRgba8Block<TOutputColor, TOutputChannelValue>(input.Slice(inputOffset, 16), buf);
					int clen = s < bcw - 1 ? 4 : clen_last;
					for (int i = 0, y = t * 4; i < 4 && y < height; i++, y++)
					{
						int outputOffset = t * 4 * width + s * 4 + i * width;
						for (int j = 0; j < clen; j++)
						{
							output[outputOffset + j] = buf[j + 4 * i];
						}
					}
				}
			}
			return inputOffset;
		}

		private static void DecodeAtcRgb4Block<TOutputColor, TOutputChannelValue>(ReadOnlySpan<byte> input, Span<TOutputColor> output)
			where TOutputChannelValue : unmanaged
			where TOutputColor : unmanaged, IColor<TOutputChannelValue>
		{
			Span<int> colors = stackalloc int[16];
			int c0 = input.ReadAtOffset<ushort>(0);
			int c1 = input.ReadAtOffset<ushort>(2);
			uint cindex = input.ReadAtOffset<uint>(4);

			DecodeColors(colors, c0, c1);

			for (int i = 0; i < 4 * 4; i++)
			{
				int cidx = unchecked((int)(cindex & 3));
				int cb = colors[cidx * 4 + 0];
				int cg = colors[cidx * 4 + 1];
				int cr = colors[cidx * 4 + 2];
				output[i].SetConvertedChannels<TOutputColor, TOutputChannelValue, byte>((byte)cr, (byte)cg, (byte)cb, byte.MaxValue);
				cindex >>= 2;
			}
		}

		private static void DecodeAtcRgba8Block<TOutputColor, TOutputChannelValue>(ReadOnlySpan<byte> input, Span<TOutputColor> output)
			where TOutputChannelValue : unmanaged
			where TOutputColor : unmanaged, IColor<TOutputChannelValue>
		{
			Span<int> alphas = stackalloc int[16];
			ulong avalue = BinaryPrimitives.ReadUInt64LittleEndian(input);
			int a0 = unchecked((int)(avalue >> 0) & 0xFF);
			int a1 = unchecked((int)(avalue >> 8) & 0xFF);
			ulong aindex = avalue >> 16;

			Span<int> colors = stackalloc int[16];
			int c0 = input.ReadAtOffset<ushort>(8);
			int c1 = input.ReadAtOffset<ushort>(10);
			uint cindex = input.ReadAtOffset<uint>(12);

			DecodeColors(colors, c0, c1);
			DecodeAlphas(alphas, a0, a1);

			for (int i = 0; i < 4 * 4; i++)
			{
				int cidx = unchecked((int)cindex & 3);
				int cb = colors[cidx * 4 + 0];
				int cg = colors[cidx * 4 + 1];
				int cr = colors[cidx * 4 + 2];
				int aidx = unchecked((int)aindex & 7);
				int ca = alphas[aidx];
				output[i].SetConvertedChannels<TOutputColor, TOutputChannelValue, byte>((byte)cr, (byte)cg, (byte)cb, (byte)ca);
				cindex >>= 2;
				aindex >>= 3;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		private static T ReadAtOffset<T>(this ReadOnlySpan<byte> input, int offset) where T : unmanaged
		{
			ThrowHelper.ThrowIfNotLittleEndian();
			return MemoryMarshal.Read<T>(input.Slice(offset));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		private static void DecodeColors(Span<int> colors, int c0, int c1)
		{
			if ((c0 & 0x8000) == 0)
			{
				colors[0] = Extend((c0 >> 0) & 0x1F, 5, 8);
				colors[1] = Extend((c0 >> 5) & 0x1F, 5, 8);
				colors[2] = Extend((c0 >> 10) & 0x1F, 5, 8);

				colors[12] = Extend((c1 >> 0) & 0x1F, 5, 8);
				colors[13] = Extend((c1 >> 5) & 0x3f, 6, 8);
				colors[14] = Extend((c1 >> 11) & 0x1F, 5, 8);

				colors[4] = (5 * colors[0] + 3 * colors[12]) / 8;
				colors[5] = (5 * colors[1] + 3 * colors[13]) / 8;
				colors[6] = (5 * colors[2] + 3 * colors[14]) / 8;

				colors[8] = (3 * colors[0] + 5 * colors[12]) / 8;
				colors[9] = (3 * colors[1] + 5 * colors[13]) / 8;
				colors[10] = (3 * colors[2] + 5 * colors[14]) / 8;
			}
			else
			{
				colors[0] = 0;
				colors[1] = 0;
				colors[2] = 0;

				colors[8] = Extend((c0 >> 0) & 0x1F, 5, 8);
				colors[9] = Extend((c0 >> 5) & 0x1F, 5, 8);
				colors[10] = Extend((c0 >> 10) & 0x1F, 5, 8);

				colors[12] = Extend((c1 >> 0) & 0x1F, 5, 8);
				colors[13] = Extend((c1 >> 5) & 0x3F, 6, 8);
				colors[14] = Extend((c1 >> 11) & 0x1F, 5, 8);

				colors[4] = int.Max(0, colors[8] - colors[12] / 4);
				colors[5] = int.Max(0, colors[9] - colors[13] / 4);
				colors[6] = int.Max(0, colors[10] - colors[14] / 4);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		private static void DecodeAlphas(Span<int> alphas, int a0, int a1)
		{
			alphas[0] = a0;
			alphas[1] = a1;
			if (a0 > a1)
			{
				alphas[2] = (a0 * 6 + a1 * 1) / 7;
				alphas[3] = (a0 * 5 + a1 * 2) / 7;
				alphas[4] = (a0 * 4 + a1 * 3) / 7;
				alphas[5] = (a0 * 3 + a1 * 4) / 7;
				alphas[6] = (a0 * 2 + a1 * 5) / 7;
				alphas[7] = (a0 * 1 + a1 * 6) / 7;
			}
			else
			{
				alphas[2] = (a0 * 4 + a1 * 1) / 5;
				alphas[3] = (a0 * 3 + a1 * 2) / 5;
				alphas[4] = (a0 * 2 + a1 * 3) / 5;
				alphas[5] = (a0 * 1 + a1 * 4) / 5;
				alphas[6] = 0;
				alphas[7] = 255;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		private static int Extend(int value, int from, int to)
		{
			// bit-pattern replicating scaling (can at most double the bits)
			return (value << (to - from)) | (value >> (from * 2 - to));
		}
	}
}
