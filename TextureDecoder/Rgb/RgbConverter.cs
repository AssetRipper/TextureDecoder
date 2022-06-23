using System;
using System.Buffers.Binary;
using System.Runtime.CompilerServices;

namespace AssetRipper.TextureDecoder.Rgb
{
	/// <summary>
	/// 
	/// </summary>
	public static class RgbConverter
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="input"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <returns></returns>
		public static byte[] A8ToBGRA32(ReadOnlySpan<byte> input, int width, int height)
		{
			byte[] output = new byte[width * height * 4];
			A8ToBGRA32(input, width, height, output);
			return output;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="input"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <param name="output"></param>
		public static void A8ToBGRA32(ReadOnlySpan<byte> input, int width, int height, Span<byte> output)
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
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="input"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <returns></returns>
		public static byte[] ARGB16ToBGRA32(ReadOnlySpan<byte> input, int width, int height)
		{
			byte[] output = new byte[width * height * 4];
			ARGB16ToBGRA32(input, width, height, output);
			return output;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="input"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <param name="output"></param>
		public static void ARGB16ToBGRA32(ReadOnlySpan<byte> input, int width, int height, Span<byte> output)
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
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="input"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <returns></returns>
		public static byte[] RGB24ToBGRA32(ReadOnlySpan<byte> input, int width, int height)
		{
			byte[] output = new byte[width * height * 4];
			RGB24ToBGRA32(input, width, height, output);
			return output;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="input"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <param name="output"></param>
		public static void RGB24ToBGRA32(ReadOnlySpan<byte> input, int width, int height, Span<byte> output)
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
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="input"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <returns></returns>
		public static byte[] RGBA32ToBGRA32(ReadOnlySpan<byte> input, int width, int height)
		{
			byte[] output = new byte[width * height * 4];
			RGBA32ToBGRA32(input, width, height, output);
			return output;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="input"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <param name="output"></param>
		public static void RGBA32ToBGRA32(ReadOnlySpan<byte> input, int width, int height, Span<byte> output)
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
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="input"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <returns></returns>
		public static byte[] ARGB32ToBGRA32(ReadOnlySpan<byte> input, int width, int height)
		{
			byte[] output = new byte[width * height * 4];
			ARGB32ToBGRA32(input, width, height, output);
			return output;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="input"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <param name="output"></param>
		public static void ARGB32ToBGRA32(ReadOnlySpan<byte> input, int width, int height, Span<byte> output)
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
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="input"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <returns></returns>
		public static byte[] RGB16ToBGRA32(ReadOnlySpan<byte> input, int width, int height)
		{
			byte[] output = new byte[width * height * 4];
			RGB16ToBGRA32(input, width, height, output);
			return output;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="input"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <param name="output"></param>
		public static void RGB16ToBGRA32(ReadOnlySpan<byte> input, int width, int height, Span<byte> output)
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
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="input"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <returns></returns>
		public static byte[] R16ToBGRA32(ReadOnlySpan<byte> input, int width, int height)
		{
			byte[] output = new byte[width * height * 4];
			R16ToBGRA32(input, width, height, output);
			return output;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="input"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <param name="output"></param>
		public static void R16ToBGRA32(ReadOnlySpan<byte> input, int width, int height, Span<byte> output)
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
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="input"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <returns></returns>
		public static byte[] RGBA16ToBGRA32(ReadOnlySpan<byte> input, int width, int height)
		{
			byte[] output = new byte[width * height * 4];
			RGBA16ToBGRA32(input, width, height, output);
			return output;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="input"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <param name="output"></param>
		public static void RGBA16ToBGRA32(ReadOnlySpan<byte> input, int width, int height, Span<byte> output)
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
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="input"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <returns></returns>
		public static byte[] RG16ToBGRA32(ReadOnlySpan<byte> input, int width, int height)
		{
			byte[] output = new byte[width * height * 4];
			RG16ToBGRA32(input, width, height, output);
			return output;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="input"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <param name="output"></param>
		public static void RG16ToBGRA32(ReadOnlySpan<byte> input, int width, int height, Span<byte> output)
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
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="input"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <returns></returns>
		public static byte[] R8ToBGRA32(ReadOnlySpan<byte> input, int width, int height)
		{
			byte[] output = new byte[width * height * 4];
			R8ToBGRA32(input, width, height, output);
			return output;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="input"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <param name="output"></param>
		public static void R8ToBGRA32(ReadOnlySpan<byte> input, int width, int height, Span<byte> output)
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
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="input"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <returns></returns>
		public static byte[] RHalfToBGRA32(ReadOnlySpan<byte> input, int width, int height)
		{
			byte[] output = new byte[width * height * 4];
			RHalfToBGRA32(input, width, height, output);
			return output;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="input"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <param name="output"></param>
		public static void RHalfToBGRA32(ReadOnlySpan<byte> input, int width, int height, Span<byte> output)
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
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="input"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <returns></returns>
		public static byte[] RGHalfToBGRA32(ReadOnlySpan<byte> input, int width, int height)
		{
			byte[] output = new byte[width * height * 4];
			RGHalfToBGRA32(input, width, height, output);
			return output;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="input"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <param name="output"></param>
		public static void RGHalfToBGRA32(ReadOnlySpan<byte> input, int width, int height, Span<byte> output)
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
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="input"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <returns></returns>
		public static byte[] RGBAHalfToBGRA32(ReadOnlySpan<byte> input, int width, int height)
		{
			byte[] output = new byte[width * height * 4];
			RGBAHalfToBGRA32(input, width, height, output);
			return output;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="input"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <param name="output"></param>
		public static void RGBAHalfToBGRA32(ReadOnlySpan<byte> input, int width, int height, Span<byte> output)
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
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="input"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <returns></returns>
		public static byte[] RFloatToBGRA32(ReadOnlySpan<byte> input, int width, int height)
		{
			byte[] output = new byte[width * height * 4];
			RFloatToBGRA32(input, width, height, output);
			return output;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="input"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <param name="output"></param>
		public static void RFloatToBGRA32(ReadOnlySpan<byte> input, int width, int height, Span<byte> output)
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
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="input"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <returns></returns>
		public static byte[] RGFloatToBGRA32(ReadOnlySpan<byte> input, int width, int height)
		{
			byte[] output = new byte[width * height * 4];
			RGFloatToBGRA32(input, width, height, output);
			return output;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="input"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <param name="output"></param>
		public static void RGFloatToBGRA32(ReadOnlySpan<byte> input, int width, int height, Span<byte> output)
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
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="input"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <returns></returns>
		public static byte[] RGBAFloatToBGRA32(ReadOnlySpan<byte> input, int width, int height)
		{
			byte[] output = new byte[width * height * 4];
			RGBAFloatToBGRA32(input, width, height, output);
			return output;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="input"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <param name="output"></param>
		public static void RGBAFloatToBGRA32(ReadOnlySpan<byte> input, int width, int height, Span<byte> output)
		{
			int io = 0;
			int oo = 0;
			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < height; j++)
				{
					byte r = ClampByte(ToSingle(input, io + 0));
					byte g = ClampByte(ToSingle(input, io + 4));
					byte b = ClampByte(ToSingle(input, io + 8));
					byte a = ClampByte(ToSingle(input, io + 12));
					output[oo + 0] = b;				// b
					output[oo + 1] = g;				// g
					output[oo + 2] = r;				// r
					output[oo + 3] = a;				// a
					io += 16;
					oo += 4;
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="input"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <returns></returns>
		public static byte[] RGB9e5FloatToBGRA32(ReadOnlySpan<byte> input, int width, int height)
		{
			byte[] output = new byte[width * height * 4];
			RGB9e5FloatToBGRA32(input, width, height, output);
			return output;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="input"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <param name="output"></param>
		public static void RGB9e5FloatToBGRA32(ReadOnlySpan<byte> input, int width, int height, Span<byte> output)
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
		}

		private static float ToHalf(ReadOnlySpan<byte> input, int offset)
		{
#if NET6_0_OR_GREATER
			return (float)BinaryPrimitives.ReadHalfLittleEndian(input.Slice(offset, 2));
#else
			ushort bits = BinaryPrimitives.ReadUInt16LittleEndian(input.Slice(offset, 2));
			return (float)Unsafe.As<ushort, Half>(ref bits);
#endif
		}

		private static float ToSingle(ReadOnlySpan<byte> input, int offset)
		{
#if NET5_0_OR_GREATER
			return BinaryPrimitives.ReadSingleLittleEndian(input.Slice(offset, 4));
#else
			uint bits = BinaryPrimitives.ReadUInt32LittleEndian(input.Slice(offset, 4));
			return Unsafe.As<uint, float>(ref bits);
#endif
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static byte ClampByte(float x)
		{
			return byte.MaxValue < x ? byte.MaxValue : (x > byte.MinValue ? (byte)x : byte.MinValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static byte ClampByte(double x)
		{
			return byte.MaxValue < x ? byte.MaxValue : (x > byte.MinValue ? (byte)x : byte.MinValue);
		}
	}
}
