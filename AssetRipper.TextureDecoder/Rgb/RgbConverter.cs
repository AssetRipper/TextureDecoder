using System.Buffers.Binary;
using System.Runtime.InteropServices;

namespace AssetRipper.TextureDecoder.Rgb
{
	public static class RgbConverter
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static int A8ToBGRA32(ReadOnlySpan<byte> input, int width, int height, out byte[] output)
		{
			output = new byte[width * height * 4];
			return A8ToBGRA32(input, width, height, output);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static int A8ToBGRA32(ReadOnlySpan<byte> input, int width, int height, Span<byte> output)
		{
			int io = 0;
			int oo = 0;
			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < height; j++)
				{
					output[oo + 0] = 0;			// b
					output[oo + 1] = 0;			// g
					output[oo + 2] = 0;			// r
					output[oo + 3] = input[io]; // a
					oo += 4;
					io++;
				}
			}
			return io;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static int RGB24ToBGRA32(ReadOnlySpan<byte> input, int width, int height, out byte[] output)
		{
			output = new byte[width * height * 4];
			return RGB24ToBGRA32(input, width, height, output);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static int RGB24ToBGRA32(ReadOnlySpan<byte> input, int width, int height, Span<byte> output)
		{
			int io = 0;
			int oo = 0;
			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < height; j++)
				{
					output[oo + 0] = input[io + 2];	// b
					output[oo + 1] = input[io + 1];	// g
					output[oo + 2] = input[io + 0];	// r
					output[oo + 3] = 255;           // a
					io += 3;
					oo += 4;
				}
			}
			return io;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static int RGBA32ToBGRA32(ReadOnlySpan<byte> input, int width, int height, out byte[] output)
		{
			output = new byte[width * height * 4];
			return RGBA32ToBGRA32(input, width, height, output);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static int RGBA32ToBGRA32(ReadOnlySpan<byte> input, int width, int height, Span<byte> output)
		{
			int io = 0;
			int oo = 0;
			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < height; j++)
				{
					output[oo + 0] = input[io + 2]; // b
					output[oo + 1] = input[io + 1]; // g
					output[oo + 2] = input[io + 0]; // r
					output[oo + 3] = input[io + 3]; // a
					io += 4;
					oo += 4;
				}
			}
			return io;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static int ARGB32ToBGRA32(ReadOnlySpan<byte> input, int width, int height, out byte[] output)
		{
			output = new byte[width * height * 4];
			return ARGB32ToBGRA32(input, width, height, output);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static int ARGB32ToBGRA32(ReadOnlySpan<byte> input, int width, int height, Span<byte> output)
		{
			int io = 0;
			int oo = 0;
			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < height; j++)
				{
					output[oo + 0] = input[io + 3]; // b
					output[oo + 1] = input[io + 2]; // g
					output[oo + 2] = input[io + 1]; // r
					output[oo + 3] = input[io + 0]; // a
					io += 4;
					oo += 4;
				}
			}
			return io;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static int RGB16ToBGRA32(ReadOnlySpan<byte> input, int width, int height, out byte[] output)
		{
			output = new byte[width * height * 4];
			return RGB16ToBGRA32(input, width, height, output);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static int RGB16ToBGRA32(ReadOnlySpan<byte> input, int width, int height, Span<byte> output)
		{
			int io = 0;
			int oo = 0;
			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < height; j++)
				{
					byte r = (byte)(input[io + 1] & 0xF8);
					byte g = unchecked((byte)((input[io + 1] << 5) | ((input[io + 0] & 0xE0) >> 3)));
					byte b = unchecked((byte)(input[io + 0] << 3));
					output[oo + 0] = b;		// b
					output[oo + 1] = g;		// g
					output[oo + 2] = r;		// r
					output[oo + 3] = 255;   // a
					io += 2;
					oo += 4;
				}
			}
			return io;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static int RG16ToBGRA32(ReadOnlySpan<byte> input, int width, int height, out byte[] output)
		{
			output = new byte[width * height * 4];
			return RG16ToBGRA32(input, width, height, output);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static int RG16ToBGRA32(ReadOnlySpan<byte> input, int width, int height, Span<byte> output)
		{
			int io = 0;
			int oo = 0;
			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < height; j++)
				{
					output[oo + 0] = 0;				// b
					output[oo + 1] = input[io + 1]; // g
					output[oo + 2] = input[io + 0]; // r
					output[oo + 3] = 255;			// a
					io += 2;
					oo += 4;
				}
			}
			return io;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static int R8ToBGRA32(ReadOnlySpan<byte> input, int width, int height, out byte[] output)
		{
			output = new byte[width * height * 4];
			return R8ToBGRA32(input, width, height, output);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static int R8ToBGRA32(ReadOnlySpan<byte> input, int width, int height, Span<byte> output)
		{
			int io = 0;
			int oo = 0;
			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < height; j++)
				{
					output[oo + 0] = 0;             // b
					output[oo + 1] = 0;				// g
					output[oo + 2] = input[io + 0]; // r
					output[oo + 3] = 255;           // a
					io += 1;
					oo += 4;
				}
			}
			return io;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static int RHalfToBGRA32(ReadOnlySpan<byte> input, int width, int height, out byte[] output)
		{
			output = new byte[width * height * 4];
			return RHalfToBGRA32(input, width, height, output);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static int RHalfToBGRA32(ReadOnlySpan<byte> input, int width, int height, Span<byte> output)
		{
			int io = 0;
			int oo = 0;
			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < height; j++)
				{
					byte r = ClampByte(ToHalf(input, io) * 255f);
					output[oo + 0] = 0;             // b
					output[oo + 1] = 0;             // g
					output[oo + 2] = r;				// r
					output[oo + 3] = 255;           // a
					io += 2;
					oo += 4;
				}
			}
			return io;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static int RGHalfToBGRA32(ReadOnlySpan<byte> input, int width, int height, out byte[] output)
		{
			output = new byte[width * height * 4];
			return RGHalfToBGRA32(input, width, height, output);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static int RGHalfToBGRA32(ReadOnlySpan<byte> input, int width, int height, Span<byte> output)
		{
			int io = 0;
			int oo = 0;
			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < height; j++)
				{
					byte r = ClampByte(ToHalf(input, io + 0) * 255f);
					byte g = ClampByte(ToHalf(input, io + 2) * 255f);
					output[oo + 0] = 0;             // b
					output[oo + 1] = g;             // g
					output[oo + 2] = r;             // r
					output[oo + 3] = 255;           // a
					io += 4;
					oo += 4;
				}
			}
			return io;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static int RGBAHalfToBGRA32(ReadOnlySpan<byte> input, int width, int height, out byte[] output)
		{
			output = new byte[width * height * 4];
			return RGBAHalfToBGRA32(input, width, height, output);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static int RGBAHalfToBGRA32(ReadOnlySpan<byte> input, int width, int height, Span<byte> output)
		{
			int io = 0;
			int oo = 0;
			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < height; j++)
				{
					byte r = ClampByte(ToHalf(input, io + 0) * 255f);
					byte g = ClampByte(ToHalf(input, io + 2) * 255f);
					byte b = ClampByte(ToHalf(input, io + 4) * 255f);
					byte a = ClampByte(ToHalf(input, io + 6) * 255f);
					output[oo + 0] = b;             // b
					output[oo + 1] = g;             // g
					output[oo + 2] = r;             // r
					output[oo + 3] = a;				// a
					io += 8;
					oo += 4;
				}
			}
			return io;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static int RFloatToBGRA32(ReadOnlySpan<byte> input, int width, int height, out byte[] output)
		{
			output = new byte[width * height * 4];
			return RFloatToBGRA32(input, width, height, output);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static int RFloatToBGRA32(ReadOnlySpan<byte> input, int width, int height, Span<byte> output)
		{
			int io = 0;
			int oo = 0;
			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < height; j++)
				{
					byte r = ClampByte(ToSingle(input, io) * 255f);
					output[oo + 0] = 0;				// b
					output[oo + 1] = 0;				// g
					output[oo + 2] = r;				// r
					output[oo + 3] = 255;			// a
					io += 4;
					oo += 4;
				}
			}
			return io;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static int RGFloatToBGRA32(ReadOnlySpan<byte> input, int width, int height, out byte[] output)
		{
			output = new byte[width * height * 4];
			return RGFloatToBGRA32(input, width, height, output);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static int RGFloatToBGRA32(ReadOnlySpan<byte> input, int width, int height, Span<byte> output)
		{
			int io = 0;
			int oo = 0;
			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < height; j++)
				{
					byte r = ClampByte(ToSingle(input, io + 0) * 255f);
					byte g = ClampByte(ToSingle(input, io + 4) * 255f);
					output[oo + 0] = 0;             // b
					output[oo + 1] = g;             // g
					output[oo + 2] = r;             // r
					output[oo + 3] = 255;           // a
					io += 8;
					oo += 4;
				}
			}
			return io;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static int RGBAFloatToBGRA32(ReadOnlySpan<byte> input, int width, int height, out byte[] output)
		{
			output = new byte[width * height * 4];
			return RGBAFloatToBGRA32(input, width, height, output);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static int RGBAFloatToBGRA32(ReadOnlySpan<byte> input, int width, int height, Span<byte> output)
		{
			int io = 0;
			int oo = 0;
			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < height; j++)
				{
					byte r = ClampByte(ToSingle(input, io + 0) * 255f);
					byte g = ClampByte(ToSingle(input, io + 4) * 255f);
					byte b = ClampByte(ToSingle(input, io + 8) * 255f);
					byte a = ClampByte(ToSingle(input, io + 12) * 255f);
					output[oo + 0] = b;				// b
					output[oo + 1] = g;				// g
					output[oo + 2] = r;				// r
					output[oo + 3] = a;				// a
					io += 16;
					oo += 4;
				}
			}
			return io;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static int RGB9e5FloatToBGRA32(ReadOnlySpan<byte> input, int width, int height, out byte[] output)
		{
			output = new byte[width * height * 4];
			return RGB9e5FloatToBGRA32(input, width, height, output);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static int RGB9e5FloatToBGRA32(ReadOnlySpan<byte> input, int width, int height, Span<byte> output)
		{
			int io = 0;
			int oo = 0;
			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < height; j++)
				{
					uint value = BinaryPrimitives.ReadUInt32LittleEndian(input.Slice(io, 4));
					double scale = Math.Pow(2, unchecked((int)(value >> 27) - 24));
					byte r = ClampByte((value >> 0 & 0x1FF) * scale * 255.0);
					byte g = ClampByte((value >> 9 & 0x1FF) * scale * 255.0);
					byte b = ClampByte((value >> 18 & 0x1FF) * scale * 255.0);
					output[oo + 0] = b;             // b
					output[oo + 1] = g;             // g
					output[oo + 2] = r;             // r
					output[oo + 3] = 255;           // a
					io += 4;
					oo += 4;
				}
			}
			return io;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static int RG32ToBGRA32(ReadOnlySpan<byte> input, int width, int height, out byte[] output)
		{
			output = new byte[width * height * 4];
			return RG32ToBGRA32(input, width, height, output);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static int RG32ToBGRA32(ReadOnlySpan<byte> input, int width, int height, Span<byte> output)
		{
			int io = 0;
			int oo = 0;
			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < height; j++)
				{
					output[oo + 0] = 0;             // b
					output[oo + 1] = input[io + 3]; // g
					output[oo + 2] = input[io + 1]; // r
					output[oo + 3] = 255;           // a
					io += 4;
					oo += 4;
				}
			}
			return io;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static int RGB48ToBGRA32(ReadOnlySpan<byte> input, int width, int height, out byte[] output)
		{
			output = new byte[width * height * 4];
			return RGB48ToBGRA32(input, width, height, output);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static int RGB48ToBGRA32(ReadOnlySpan<byte> input, int width, int height, Span<byte> output)
		{
			int io = 0;
			int oo = 0;
			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < height; j++)
				{
					output[oo + 0] = input[io + 5]; // b
					output[oo + 1] = input[io + 3]; // g
					output[oo + 2] = input[io + 1]; // r
					output[oo + 3] = 255;           // a
					io += 6;
					oo += 4;
				}
			}
			return io;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static int RGBA64ToBGRA32(ReadOnlySpan<byte> input, int width, int height, out byte[] output)
		{
			output = new byte[width * height * 4];
			return RGBA64ToBGRA32(input, width, height, output);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static int RGBA64ToBGRA32(ReadOnlySpan<byte> input, int width, int height, Span<byte> output)
		{
			int io = 0;
			int oo = 0;
			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < height; j++)
				{
					output[oo + 0] = input[io + 5]; // b
					output[oo + 1] = input[io + 3]; // g
					output[oo + 2] = input[io + 1]; // r
					output[oo + 3] = input[io + 7]; // a
					io += 8;
					oo += 4;
				}
			}
			return io;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static int Convert<TSourceColor, TSourceChannel, TDestinationColor, TDestinationChannel>(ReadOnlySpan<byte> input, int width, int height, out byte[] output)
			where TSourceChannel : unmanaged
			where TSourceColor : unmanaged, IColor<TSourceChannel>
			where TDestinationChannel : unmanaged
			where TDestinationColor : unmanaged, IColor<TDestinationChannel>
		{
			output = new byte[width * height * Unsafe.SizeOf<TDestinationColor>()];
			return Convert<TSourceColor, TSourceChannel, TDestinationColor, TDestinationChannel>(input, width, height, output);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static int Convert<TSourceColor, TSourceChannel, TDestinationColor, TDestinationChannel>(ReadOnlySpan<byte> input, int width, int height, Span<byte> output)
			where TSourceChannel : unmanaged
			where TSourceColor : unmanaged, IColor<TSourceChannel>
			where TDestinationChannel : unmanaged
			where TDestinationColor : unmanaged, IColor<TDestinationChannel>
		{
			ReadOnlySpan<TSourceColor> sourceSpan = MemoryMarshal.Cast<byte, TSourceColor>(input).Slice(0, width * height);
			Span<TDestinationColor> destinationSpan = MemoryMarshal.Cast<byte, TDestinationColor>(output).Slice(0, width * height);
			Convert<TSourceColor, TSourceChannel, TDestinationColor, TDestinationChannel>(sourceSpan, destinationSpan);
			return width * height * Unsafe.SizeOf<TSourceColor>();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static void Convert<TSourceColor, TSourceChannel, TDestinationColor, TDestinationChannel>(ReadOnlySpan<TSourceColor> sourceSpan, Span<TDestinationColor> destinationSpan)
			where TSourceChannel : unmanaged
			where TSourceColor : unmanaged, IColor<TSourceChannel>
			where TDestinationChannel : unmanaged
			where TDestinationColor : unmanaged, IColor<TDestinationChannel>
		{
			for (int i = 0; i < sourceSpan.Length; i++)
			{
				destinationSpan[i] = sourceSpan[i].Convert<TSourceColor, TSourceChannel, TDestinationColor, TDestinationChannel>();
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		private static float ToHalf(ReadOnlySpan<byte> input, int offset)
		{
			return (float)BinaryPrimitives.ReadHalfLittleEndian(input.Slice(offset, 2));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		private static float ToSingle(ReadOnlySpan<byte> input, int offset)
		{
			return BinaryPrimitives.ReadSingleLittleEndian(input.Slice(offset, 4));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		private static byte ClampByte(float x)
		{
			return byte.MaxValue < x ? byte.MaxValue : (x > byte.MinValue ? (byte)x : byte.MinValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		private static byte ClampByte(double x)
		{
			return byte.MaxValue < x ? byte.MaxValue : (x > byte.MinValue ? (byte)x : byte.MinValue);
		}
	}
}
