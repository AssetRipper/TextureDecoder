using AssetRipper.TextureDecoder.Rgb;
using AssetRipper.TextureDecoder.Rgb.Formats;

namespace AssetRipper.TextureDecoder.Bc
{
	public static class BcDecoder
	{
		public static int DecompressBC1(ReadOnlySpan<byte> input, int width, int height, out byte[] output)
		{
			output = new byte[width * height * Unsafe.SizeOf<ColorBGRA32>()];
			return DecompressBC1(input, width, height, output);
		}

		public static int DecompressBC1(ReadOnlySpan<byte> input, int width, int height, Span<byte> output)
		{
			int inputOffset = 0;
			for (int i = 0; i < height; i += 4)
			{
				for (int j = 0; j < width; j += 4)
				{
					int outputOffset = ((i * width) + j) * Unsafe.SizeOf<ColorRGBA32>();
					BcHelpers.DecompressBc1(input.Slice(inputOffset, DefineConstants.BCDEC_BC1_BLOCK_SIZE), output.Slice(outputOffset), width * 4);
					inputOffset += DefineConstants.BCDEC_BC1_BLOCK_SIZE;
				}
			}
			RgbConverter.Convert<ColorRGBA32, byte, ColorBGRA32, byte>(output, width, height, output);
			return inputOffset;
		}

		public static int DecompressBC2(ReadOnlySpan<byte> input, int width, int height, out byte[] output)
		{
			output = new byte[width * height * Unsafe.SizeOf<ColorBGRA32>()];
			return DecompressBC2(input, width, height, output);
		}

		public static int DecompressBC2(ReadOnlySpan<byte> input, int width, int height, Span<byte> output)
		{
			int inputOffset = 0;
			for (int i = 0; i < height; i += 4)
			{
				for (int j = 0; j < width; j += 4)
				{
					int outputOffset = ((i * width) + j) * Unsafe.SizeOf<ColorRGBA32>();
					BcHelpers.DecompressBc2(input.Slice(inputOffset), output.Slice(outputOffset), width * 4);
					inputOffset += DefineConstants.BCDEC_BC2_BLOCK_SIZE;
				}
			}
			RgbConverter.Convert<ColorRGBA32, byte, ColorBGRA32, byte>(output, width, height, output);
			return inputOffset;
		}

		public static int DecompressBC3(ReadOnlySpan<byte> input, int width, int height, out byte[] output)
		{
			output = new byte[width * height * Unsafe.SizeOf<ColorBGRA32>()];
			return DecompressBC3(input, width, height, output);
		}

		public static int DecompressBC3(ReadOnlySpan<byte> input, int width, int height, Span<byte> output)
		{
			int inputOffset = 0;
			for (int i = 0; i < height; i += 4)
			{
				for (int j = 0; j < width; j += 4)
				{
					int outputOffset = ((i * width) + j) * Unsafe.SizeOf<ColorRGBA32>();
					BcHelpers.DecompressBc3(input.Slice(inputOffset), output.Slice(outputOffset), width * 4);
					inputOffset += DefineConstants.BCDEC_BC3_BLOCK_SIZE;
				}
			}
			RgbConverter.Convert<ColorRGBA32, byte, ColorBGRA32, byte>(output, width, height, output);
			return inputOffset;
		}

		public static int DecompressBC4(ReadOnlySpan<byte> input, int width, int height, out byte[] output)
		{
			output = new byte[width * height * Unsafe.SizeOf<ColorBGRA32>()];
			return DecompressBC4(input, width, height, output);
		}

		public static int DecompressBC4(ReadOnlySpan<byte> input, int width, int height, Span<byte> output)
		{
			int inputOffset = 0;
			Span<byte> buffer = new byte[width * height * Unsafe.SizeOf<ColorR8>()];
			for (int i = 0; i < height; i += 4)
			{
				for (int j = 0; j < width; j += 4)
				{
					int bufferOffset = ((i * width) + j) * Unsafe.SizeOf<ColorR8>();
					BcHelpers.DecompressBc4(input.Slice(inputOffset), buffer.Slice(bufferOffset), width);
					inputOffset += DefineConstants.BCDEC_BC4_BLOCK_SIZE;
				}
			}
			RgbConverter.Convert<ColorR8, byte, ColorBGRA32, byte>(buffer, width, height, output);
			return inputOffset;
		}

		public static int DecompressBC5(ReadOnlySpan<byte> input, int width, int height, out byte[] output)
		{
			output = new byte[width * height * Unsafe.SizeOf<ColorBGRA32>()];
			return DecompressBC5(input, width, height, output);
		}

		public static int DecompressBC5(ReadOnlySpan<byte> input, int width, int height, Span<byte> output)
		{
			int inputOffset = 0;
			Span<byte> buffer = new byte[width * height * Unsafe.SizeOf<ColorRG16>()];
			for (int i = 0; i < height; i += 4)
			{
				for (int j = 0; j < width; j += 4)
				{
					int bufferOffset = ((i * width) + j) * Unsafe.SizeOf<ColorRG16>();
					BcHelpers.DecompressBc5(input.Slice(inputOffset), buffer.Slice(bufferOffset), width * 2);
					inputOffset += DefineConstants.BCDEC_BC5_BLOCK_SIZE;
				}
			}
			RgbConverter.Convert<ColorRG16, byte, ColorBGRA32, byte>(buffer, width, height, output);
			return inputOffset;
		}

		public static int DecompressBC6H(ReadOnlySpan<byte> input, int width, int height, bool isSigned, out byte[] output)
		{
			output = new byte[width * height * Unsafe.SizeOf<ColorBGRA32>()];
			return DecompressBC6H(input, width, height, isSigned, output);
		}

		public unsafe static int DecompressBC6H(ReadOnlySpan<byte> input, int width, int height, bool isSigned, Span<byte> output)
		{
			int bytesRead;
			byte[] buffer = new byte[width * height * Unsafe.SizeOf<ColorRGBSingle>()];
			fixed (byte* bufferPtr = buffer)
			{
				bytesRead = DecompressBC6H(input, width, height, isSigned, bufferPtr);
			}
			RgbConverter.Convert<ColorRGBSingle, float, ColorBGRA32, byte>(buffer, width, height, output);
			return bytesRead;
		}

		private unsafe static int DecompressBC6H(ReadOnlySpan<byte> input, int width, int height, bool isSigned, byte* output)
		{
			int inputOffset = 0;
			for (int i = 0; i < height; i += 4)
			{
				for (int j = 0; j < width; j += 4)
				{
					int outputOffset = ((i * width) + j) * Unsafe.SizeOf<ColorRGBSingle>();
					BcHelpers.DecompressBc6h_Float(
						input.Slice(inputOffset, DefineConstants.BCDEC_BC6H_BLOCK_SIZE),
						output + outputOffset,
						width * 3,
						isSigned);
					inputOffset += DefineConstants.BCDEC_BC6H_BLOCK_SIZE;
				}
			}
			return inputOffset;
		}

		public static int DecompressBC7(ReadOnlySpan<byte> input, int width, int height, out byte[] output)
		{
			output = new byte[width * height * Unsafe.SizeOf<ColorBGRA32>()];
			return DecompressBC7(input, width, height, output);
		}

		public unsafe static int DecompressBC7(ReadOnlySpan<byte> input, int width, int height, Span<byte> output)
		{
			int inputOffset = 0;
			for (int i = 0; i < height; i += 4)
			{
				for (int j = 0; j < width; j += 4)
				{
					int outputOffset = ((i * width) + j) * Unsafe.SizeOf<ColorRGBA32>();
					BcHelpers.DecompressBc7(
						input.Slice(inputOffset, DefineConstants.BCDEC_BC7_BLOCK_SIZE), 
						output.Slice(outputOffset),
						width * 4);
					inputOffset += DefineConstants.BCDEC_BC7_BLOCK_SIZE;
				}
			}
			RgbConverter.Convert<ColorRGBA32, byte, ColorBGRA32, byte>(output, width, height, output);
			return inputOffset;
		}
	}
}
