using AssetRipper.TextureDecoder.Rgb;
using AssetRipper.TextureDecoder.Rgb.Formats;

namespace AssetRipper.TextureDecoder.Bc;

public static class Bc1
{
	public static int Decompress(ReadOnlySpan<byte> input, int width, int height, out byte[] output)
	{
		output = new byte[width * height * Unsafe.SizeOf<ColorBGRA32>()];
		return Decompress(input, width, height, output);
	}

	public static int Decompress(ReadOnlySpan<byte> input, int width, int height, Span<byte> output)
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
}