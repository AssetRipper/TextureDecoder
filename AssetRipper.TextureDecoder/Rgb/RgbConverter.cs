using System.Buffers.Binary;
using System.Runtime.InteropServices;

namespace AssetRipper.TextureDecoder.Rgb
{
	public static class RgbConverter
	{
		[MethodImpl(OptimizationConstants.AggressiveInliningAndOptimization)]
		public static int A8ToBGRA32(ReadOnlySpan<byte> input, int width, int height, out byte[] output)
		{
			output = new byte[width * height * 4];
			return A8ToBGRA32(input, width, height, output);
		}

		[MethodImpl(OptimizationConstants.AggressiveInliningAndOptimization)]
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

		[MethodImpl(OptimizationConstants.AggressiveInliningAndOptimization)]
		public static int ARGB16ToBGRA32(ReadOnlySpan<byte> input, int width, int height, out byte[] output)
		{
			output = new byte[width * height * 4];
			return ARGB16ToBGRA32(input, width, height, output);
		}

		[MethodImpl(OptimizationConstants.AggressiveInliningAndOptimization)]
		public static int ARGB16ToBGRA32(ReadOnlySpan<byte> input, int width, int height, Span<byte> output)
		{
			int io = 0;
			int oo = 0;
			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < height; j++)
				{
					output[oo + 0] = unchecked((byte)(input[io + 0] << 4));	// b
					output[oo + 1] = (byte)(input[io + 0] & 0xF0);			// g	
					output[oo + 2] = unchecked((byte)(input[io + 1] << 4));	// r
					output[oo + 3] = (byte)(input[io + 1] & 0xF0);			// a
					io += 2;
					oo += 4;
				}
			}
			return io;
		}

		[MethodImpl(OptimizationConstants.AggressiveInliningAndOptimization)]
		public static int RGB24ToBGRA32(ReadOnlySpan<byte> input, int width, int height, out byte[] output)
		{
			output = new byte[width * height * 4];
			return RGB24ToBGRA32(input, width, height, output);
		}

		[MethodImpl(OptimizationConstants.AggressiveInliningAndOptimization)]
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

		[MethodImpl(OptimizationConstants.AggressiveInliningAndOptimization)]
		public static int RGBA32ToBGRA32(ReadOnlySpan<byte> input, int width, int height, out byte[] output)
		{
			output = new byte[width * height * 4];
			return RGBA32ToBGRA32(input, width, height, output);
		}

		[MethodImpl(OptimizationConstants.AggressiveInliningAndOptimization)]
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

		[MethodImpl(OptimizationConstants.AggressiveInliningAndOptimization)]
		public static int ARGB32ToBGRA32(ReadOnlySpan<byte> input, int width, int height, out byte[] output)
		{
			output = new byte[width * height * 4];
			return ARGB32ToBGRA32(input, width, height, output);
		}

		[MethodImpl(OptimizationConstants.AggressiveInliningAndOptimization)]
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

		[MethodImpl(OptimizationConstants.AggressiveInliningAndOptimization)]
		public static int RGB16ToBGRA32(ReadOnlySpan<byte> input, int width, int height, out byte[] output)
		{
			output = new byte[width * height * 4];
			return RGB16ToBGRA32(input, width, height, output);
		}

		[MethodImpl(OptimizationConstants.AggressiveInliningAndOptimization)]
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

		[MethodImpl(OptimizationConstants.AggressiveInliningAndOptimization)]
		public static int R16ToBGRA32(ReadOnlySpan<byte> input, int width, int height, out byte[] output)
		{
			output = new byte[width * height * 4];
			return R16ToBGRA32(input, width, height, output);
		}

		[MethodImpl(OptimizationConstants.AggressiveInliningAndOptimization)]
		public static int R16ToBGRA32(ReadOnlySpan<byte> input, int width, int height, Span<byte> output)
		{
			int io = 0;
			int oo = 0;
			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < height; j++)
				{
					output[oo + 0] = 0;				// b
					output[oo + 1] = 0;				// g
					output[oo + 2] = input[io + 1]; // r
					output[oo + 3] = 255;			// a
					io += 2;
					oo += 4;
				}
			}
			return io;
		}

		[MethodImpl(OptimizationConstants.AggressiveInliningAndOptimization)]
		public static int RGBA16ToBGRA32(ReadOnlySpan<byte> input, int width, int height, out byte[] output)
		{
			output = new byte[width * height * 4];
			return RGBA16ToBGRA32(input, width, height, output);
		}

		[MethodImpl(OptimizationConstants.AggressiveInliningAndOptimization)]
		public static int RGBA16ToBGRA32(ReadOnlySpan<byte> input, int width, int height, Span<byte> output)
		{
			int io = 0;
			int oo = 0;
			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < height; j++)
				{
					output[oo + 0] = (byte)(input[io + 0] & 0xF0);			// b
					output[oo + 1] = unchecked((byte)(input[io + 1] << 4));	// g	
					output[oo + 2] = (byte)(input[io + 1] & 0xF0);			// r
					output[oo + 3] = unchecked((byte)(input[io + 0] << 4));	// a
					io += 2;
					oo += 4;
				}
			}
			return io;
		}

		[MethodImpl(OptimizationConstants.AggressiveInliningAndOptimization)]
		public static int RG16ToBGRA32(ReadOnlySpan<byte> input, int width, int height, out byte[] output)
		{
			output = new byte[width * height * 4];
			return RG16ToBGRA32(input, width, height, output);
		}

		[MethodImpl(OptimizationConstants.AggressiveInliningAndOptimization)]
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

		[MethodImpl(OptimizationConstants.AggressiveInliningAndOptimization)]
		public static int R8ToBGRA32(ReadOnlySpan<byte> input, int width, int height, out byte[] output)
		{
			output = new byte[width * height * 4];
			return R8ToBGRA32(input, width, height, output);
		}

		[MethodImpl(OptimizationConstants.AggressiveInliningAndOptimization)]
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

		[MethodImpl(OptimizationConstants.AggressiveInliningAndOptimization)]
		public static int RHalfToBGRA32(ReadOnlySpan<byte> input, int width, int height, out byte[] output)
		{
			output = new byte[width * height * 4];
			return RHalfToBGRA32(input, width, height, output);
		}

		[MethodImpl(OptimizationConstants.AggressiveInliningAndOptimization)]
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

		[MethodImpl(OptimizationConstants.AggressiveInliningAndOptimization)]
		public static int RGHalfToBGRA32(ReadOnlySpan<byte> input, int width, int height, out byte[] output)
		{
			output = new byte[width * height * 4];
			return RGHalfToBGRA32(input, width, height, output);
		}

		[MethodImpl(OptimizationConstants.AggressiveInliningAndOptimization)]
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

		[MethodImpl(OptimizationConstants.AggressiveInliningAndOptimization)]
		public static int RGBAHalfToBGRA32(ReadOnlySpan<byte> input, int width, int height, out byte[] output)
		{
			output = new byte[width * height * 4];
			return RGBAHalfToBGRA32(input, width, height, output);
		}

		[MethodImpl(OptimizationConstants.AggressiveInliningAndOptimization)]
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

		[MethodImpl(OptimizationConstants.AggressiveInliningAndOptimization)]
		public static int RFloatToBGRA32(ReadOnlySpan<byte> input, int width, int height, out byte[] output)
		{
			output = new byte[width * height * 4];
			return RFloatToBGRA32(input, width, height, output);
		}

		[MethodImpl(OptimizationConstants.AggressiveInliningAndOptimization)]
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

		[MethodImpl(OptimizationConstants.AggressiveInliningAndOptimization)]
		public static int RGFloatToBGRA32(ReadOnlySpan<byte> input, int width, int height, out byte[] output)
		{
			output = new byte[width * height * 4];
			return RGFloatToBGRA32(input, width, height, output);
		}

		[MethodImpl(OptimizationConstants.AggressiveInliningAndOptimization)]
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

		[MethodImpl(OptimizationConstants.AggressiveInliningAndOptimization)]
		public static int RGBAFloatToBGRA32(ReadOnlySpan<byte> input, int width, int height, out byte[] output)
		{
			output = new byte[width * height * 4];
			return RGBAFloatToBGRA32(input, width, height, output);
		}

		[MethodImpl(OptimizationConstants.AggressiveInliningAndOptimization)]
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

		[MethodImpl(OptimizationConstants.AggressiveInliningAndOptimization)]
		public static int RGB9e5FloatToBGRA32(ReadOnlySpan<byte> input, int width, int height, out byte[] output)
		{
			output = new byte[width * height * 4];
			return RGB9e5FloatToBGRA32(input, width, height, output);
		}

		[MethodImpl(OptimizationConstants.AggressiveInliningAndOptimization)]
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

		[MethodImpl(OptimizationConstants.AggressiveInliningAndOptimization)]
		public static int RG32ToBGRA32(ReadOnlySpan<byte> input, int width, int height, out byte[] output)
		{
			output = new byte[width * height * 4];
			return RG32ToBGRA32(input, width, height, output);
		}

		[MethodImpl(OptimizationConstants.AggressiveInliningAndOptimization)]
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

		[MethodImpl(OptimizationConstants.AggressiveInliningAndOptimization)]
		public static int RGB48ToBGRA32(ReadOnlySpan<byte> input, int width, int height, out byte[] output)
		{
			output = new byte[width * height * 4];
			return RGB48ToBGRA32(input, width, height, output);
		}

		[MethodImpl(OptimizationConstants.AggressiveInliningAndOptimization)]
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

		[MethodImpl(OptimizationConstants.AggressiveInliningAndOptimization)]
		public static int RGBA64ToBGRA32(ReadOnlySpan<byte> input, int width, int height, out byte[] output)
		{
			output = new byte[width * height * 4];
			return RGBA64ToBGRA32(input, width, height, output);
		}

		[MethodImpl(OptimizationConstants.AggressiveInliningAndOptimization)]
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

		[MethodImpl(OptimizationConstants.AggressiveInliningAndOptimization)]
		public static int Convert<TSource, TSourceArg, TDestination, TDestinationArg>(ReadOnlySpan<byte> input, int width, int height, out byte[] output)
			where TSourceArg : unmanaged
			where TSource : unmanaged, IColor<TSourceArg>
			where TDestinationArg : unmanaged
			where TDestination : unmanaged, IColor<TDestinationArg>
		{
			output = new byte[width * height * Unsafe.SizeOf<TDestination>()];
			return Convert<TSource, TSourceArg, TDestination, TDestinationArg>(input, width, height, output);
		}

		[MethodImpl(OptimizationConstants.AggressiveInliningAndOptimization)]
		public static int Convert<TSource, TSourceArg, TDestination, TDestinationArg>(ReadOnlySpan<byte> input, int width, int height, Span<byte> output)
			where TSourceArg : unmanaged
			where TSource : unmanaged, IColor<TSourceArg>
			where TDestinationArg : unmanaged
			where TDestination : unmanaged, IColor<TDestinationArg>
		{
			ReadOnlySpan<TSource> sourceSpan = MemoryMarshal.Cast<byte, TSource>(input).Slice(0, width * height);
			Span<TDestination> destinationSpan = MemoryMarshal.Cast<byte, TDestination>(output).Slice(0, width * height);
			Convert<TSource, TSourceArg, TDestination, TDestinationArg>(sourceSpan, destinationSpan);
			return width * height * Unsafe.SizeOf<TSource>();
		}

		[MethodImpl(OptimizationConstants.AggressiveInliningAndOptimization)]
		public static void Convert<TSource, TSourceArg, TDestination, TDestinationArg>(ReadOnlySpan<TSource> sourceSpan, Span<TDestination> destinationSpan)
			where TSourceArg : unmanaged
			where TSource : unmanaged, IColor<TSourceArg>
			where TDestinationArg : unmanaged
			where TDestination : unmanaged, IColor<TDestinationArg>
		{
			for (int i = 0; i < sourceSpan.Length; i++)
			{
				destinationSpan[i] = sourceSpan[i].Convert<TSource, TSourceArg, TDestination, TDestinationArg>();
			}
		}

		[MethodImpl(OptimizationConstants.AggressiveInliningAndOptimization)]
		private static float ToHalf(ReadOnlySpan<byte> input, int offset)
		{
#if NET6_0_OR_GREATER
			return (float)BinaryPrimitives.ReadHalfLittleEndian(input.Slice(offset, 2));
#else
			ushort bits = BinaryPrimitives.ReadUInt16LittleEndian(input.Slice(offset, 2));
			return (float)Unsafe.As<ushort, Half>(ref bits);
#endif
		}

		[MethodImpl(OptimizationConstants.AggressiveInliningAndOptimization)]
		private static float ToSingle(ReadOnlySpan<byte> input, int offset)
		{
#if NET5_0_OR_GREATER
			return BinaryPrimitives.ReadSingleLittleEndian(input.Slice(offset, 4));
#else
			uint bits = BinaryPrimitives.ReadUInt32LittleEndian(input.Slice(offset, 4));
			return Unsafe.As<uint, float>(ref bits);
#endif
		}

		[MethodImpl(OptimizationConstants.AggressiveInliningAndOptimization)]
		private static byte ClampByte(float x)
		{
			return byte.MaxValue < x ? byte.MaxValue : (x > byte.MinValue ? (byte)x : byte.MinValue);
		}

		[MethodImpl(OptimizationConstants.AggressiveInliningAndOptimization)]
		private static byte ClampByte(double x)
		{
			return byte.MaxValue < x ? byte.MaxValue : (x > byte.MinValue ? (byte)x : byte.MinValue);
		}
	}
}
